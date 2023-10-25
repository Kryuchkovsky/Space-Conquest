using _GameLogic.Extensions.Configs;
using _GameLogic.GalaxyGenerator;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using Unity.IL2CPP.CompilerServices;
using Unity.Mathematics;
using UnityEngine;

namespace _GameLogic.Gameplay.Galaxy.Generation.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [CreateAssetMenu(menuName = "ECS/Systems/Gameplay/Galaxy/Generation/" + nameof(GalaxyGenerationRequestProcessingSystem))]
    public class GalaxyGenerationRequestProcessingSystem : UpdateSystem
    {
        private Request<GalaxyGenerationRequest> _galaxyGenerationRequest;
        private GalaxyGenerationManager _galaxyGenerationManager;

        public override void OnAwake()
        {
            _galaxyGenerationRequest = World.GetRequest<GalaxyGenerationRequest>();
            _galaxyGenerationManager = new GalaxyGenerationManager();
        }

        public override void OnUpdate(float deltaTime)
        {
            foreach (var request in _galaxyGenerationRequest.Consume())
            {
                var catalog = ConfigManager.GetConfig<GameResourcesCatalog>();
                var galaxy = Instantiate(catalog.GalaxyPrefab);
                var galaxyConfiguration = new GalaxyConfiguration("Galaxy", request.StarSystemsNumber, 4, 1.05f, 5, 0.05f);
                _galaxyGenerationManager.Configuration = galaxyConfiguration;
                var points = _galaxyGenerationManager.GenerateSimpleGalaxy();

                foreach (var point in points)
                {
                    var position = point;
                    var starSystem = Instantiate(catalog.StarSystemPrefab, position, quaternion.identity, galaxy.transform);
                }
            }
        }
    }
}