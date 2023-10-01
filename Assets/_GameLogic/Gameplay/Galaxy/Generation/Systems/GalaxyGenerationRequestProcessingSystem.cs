using _GameLogic.Extensions.GalaxyGenerator;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace _GameLogic.Gameplay.Galaxy.Generation.Systems
{
    public partial class GalaxyGenerationRequestProcessingSystem : SystemBase
    {
        private GalaxyGenerator _galaxyGenerator;
        
        protected override void OnCreate()
        {
            base.OnCreate();
            _galaxyGenerator = new GalaxyGenerator();
            RequireForUpdate<GameResourcesData>();
            RequireForUpdate<GalaxyGenerationRequest>();
        }

        protected override void OnUpdate()
        {
            var ecb = SystemAPI
                .GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>()
                .CreateCommandBuffer(World.Unmanaged);
            var gameResourcesData = SystemAPI.GetSingleton<GameResourcesData>();

            foreach (var (request, entity) in SystemAPI.Query<GalaxyGenerationRequest>().WithEntityAccess())
            {
                var galaxyEntity = ecb.Instantiate(gameResourcesData.GalaxyPrefab);
                var galaxyConfiguration = new GalaxyConfiguration("Galaxy", request.StarSystemsNumber, 4, 1.05f, 5, 0.05f);
                _galaxyGenerator.Configuration = galaxyConfiguration;
                var points = _galaxyGenerator.GenerateSimpleGalaxy();

                foreach (var point in points)
                {
                    var starSystemEntity = ecb.Instantiate(gameResourcesData.StarSystemPrefab);
                    ecb.AddComponent(starSystemEntity, new Parent
                    {
                        Value = galaxyEntity
                    });
                    ecb.SetComponent(starSystemEntity, new LocalTransform()
                    {
                        Position = new float3(point.X, 0, point.Y),
                        Scale = 1
                    });
                }
                
                ecb.DestroyEntity(entity);
            }
        }
    }
}