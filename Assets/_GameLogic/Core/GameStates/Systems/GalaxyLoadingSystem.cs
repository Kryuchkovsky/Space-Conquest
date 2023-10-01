using _GameLogic.Gameplay.Galaxy.Generation;
using _GameLogic.Loading;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine.SceneManagement;

namespace _GameLogic.Core.GameStates.Systems
{
    public partial class GalaxyGeneratingSystem : SystemBase
    {
        private readonly float _loadingDuration = 1;

        protected override void OnCreate()
        {
            base.OnCreate();
            RequireForUpdate<IsStateMachine>();
            RequireForUpdate<IsGameState>();
            RequireForUpdate<LoadingStateProcess>();
        }

        protected override void OnStartRunning()
        {
            base.OnStartRunning();

            var gameSceneLoadingOperation = SceneManager.LoadSceneAsync(2);
            gameSceneLoadingOperation.completed += _ =>
            {
            };
            
            var loadingSceneLoadingOperation = SceneManager.LoadSceneAsync(0, LoadSceneMode.Additive);
            loadingSceneLoadingOperation.completed += _ =>
            {
            };
            
            var ecb = SystemAPI
                .GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>()
                .CreateCommandBuffer(World.Unmanaged);

            foreach (var (loadingStateProcess, entity) in SystemAPI
                         .Query<LoadingStateProcess>().WithAll<IsStateMachine, IsGameState>().WithEntityAccess())
            {
                var galaxyGenerationRequestEntity = EntityManager.CreateEntity();
                ecb.AddComponent(galaxyGenerationRequestEntity, new GalaxyGenerationRequest
                {
                    StarSystemsNumber = 10000
                });
            }
        }

        protected override void OnUpdate()
        {
            var ecb = SystemAPI
                .GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>()
                .CreateCommandBuffer(World.Unmanaged);
            
            foreach (var (loadingStateProcess, entity) in SystemAPI
                         .Query<LoadingStateProcess>().WithAll<IsStateMachine, IsGameState>().WithEntityAccess())
            {
                var data = loadingStateProcess;
                var progress = math.clamp(data.LoadingTime / _loadingDuration, 0, 1);
                data.Progress = progress;

                if (data.LoadingTime < _loadingDuration)
                {
                    data.LoadingTime += SystemAPI.Time.DeltaTime;
                    ecb.SetComponent(entity, data);
                }
                else
                {
                    ecb.RemoveComponent<LoadingStateProcess>(entity);
                    SceneManager.UnloadSceneAsync(0);
                }
            }
        }
    }
}