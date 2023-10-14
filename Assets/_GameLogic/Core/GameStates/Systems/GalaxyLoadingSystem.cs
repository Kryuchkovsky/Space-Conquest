using _GameLogic.Gameplay.Galaxy.Generation;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine.SceneManagement;

namespace _GameLogic.Core.GameStates.Systems
{
    public partial class GalaxyLoadingSystem : SystemBase
    {
        private readonly float _loadingDuration = 1;

        protected override void OnCreate()
        {
            base.OnCreate();
            RequireForUpdate<StateMachine>();
            RequireForUpdate<GameState>();
            RequireForUpdate<LoadingState>();
        }

        protected override void OnStartRunning()
        {
            base.OnStartRunning();

            var gameSceneLoadingOperation = SceneManager.LoadSceneAsync(2);
            gameSceneLoadingOperation.completed += _ =>
            {
                foreach (var (gameState, entity) in SystemAPI
                             .Query<GameState>().WithAll<StateMachine, LoadingState>().WithEntityAccess())
                { 
                    var data = gameState; 
                    data.SceneIsLoaded = true; 
                    EntityManager.SetComponentData(entity, data);
                }
            };
            
            var loadingSceneLoadingOperation = SceneManager.LoadSceneAsync(0, LoadSceneMode.Additive);
            loadingSceneLoadingOperation.completed += _ =>
            {
                var ecb = SystemAPI
                    .GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>()
                    .CreateCommandBuffer(World.Unmanaged);
                
                foreach (var (loadingState, entity) in SystemAPI
                             .Query<LoadingState>().WithAll<StateMachine, GameState>().WithEntityAccess())
                {
                    var data = loadingState.ValueRO;
                    data.SceneIsLoaded = true;
                    ecb.SetComponent(entity, data);
                }
            };

            foreach (var (loadingState, entity) in SystemAPI
                         .Query<LoadingState>().WithAll<StateMachine, GameState>().WithEntityAccess())
            {
                var ecb = SystemAPI
                    .GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>()
                    .CreateCommandBuffer(World.Unmanaged);
                
                var galaxyGenerationRequestEntity = EntityManager.CreateEntity();
                ecb.AddComponent(galaxyGenerationRequestEntity, new GalaxyGenerationRequest
                {
                    StarSystemsNumber = 1000
                });
            }
        }

        protected override void OnUpdate()
        {
            var ecb = SystemAPI
                .GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>()
                .CreateCommandBuffer(World.Unmanaged);
            
            foreach (var (loadingStateProcess, entity) in SystemAPI
                         .Query<LoadingState>().WithAll<StateMachine, GameState>().WithEntityAccess())
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
                    ecb.RemoveComponent<LoadingState>(entity);
                    var loadingSceneUnLoadingOperation = SceneManager.UnloadSceneAsync(0);
                    loadingSceneUnLoadingOperation.completed += _ =>
                    {
                    };
                }
            }
        }
    }
}