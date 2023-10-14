using _GameLogic.Common;
using Unity.Entities;
using UnityEngine.SceneManagement;

namespace _GameLogic.Gameplay.Galaxy.StarSystems.Systems
{
    public partial class StarSystemMapOpeningSystem : SystemBase
    {
        protected override void OnCreate()
        {
            base.OnCreate();
            RequireForUpdate<StarSystem>();
            RequireForUpdate<ClickEvent>();
        }

        protected override void OnUpdate()
        {
            foreach (var (starSystem, entity) in SystemAPI.Query<StarSystem>().WithAll<ClickEvent>().WithEntityAccess())
            {
                if (!SceneManager.GetSceneByBuildIndex(3).isLoaded)
                {
                    var operation = SceneManager.LoadSceneAsync(3);
                    operation.completed += _ =>
                    {
                    };
                }
                
                var ecb = SystemAPI
                    .GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>()
                    .CreateCommandBuffer(World.Unmanaged);
                ecb.RemoveComponent<ClickEvent>(entity);
            }
        }
    }
}