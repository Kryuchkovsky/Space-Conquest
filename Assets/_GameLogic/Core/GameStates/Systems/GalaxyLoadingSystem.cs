using _GameLogic.Gameplay.Galaxy.Generation;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using Unity.IL2CPP.CompilerServices;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _GameLogic.Core.GameStates.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [CreateAssetMenu(menuName = "ECS/Systems/Core/GameStates/" + nameof(GalaxyLoadingSystem))]
    public class GalaxyLoadingSystem : UpdateSystem
    {
        private FilterBuilder _filterBuilder;
        private readonly float _loadingDuration = 1;

        public override void OnAwake()
        {
            _filterBuilder = World.Filter.With<StateMachine>().With<PlayState>().With<LoadingState>();
        }

        public override void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filterBuilder.Build())
            {
                ref var gameState = ref entity.GetComponent<PlayState>();
                
                if (gameState.GalaxySceneIsLoaded && !gameState.GalaxyGenerationRequestIsCreated && !gameState.GalaxyIsCreated)
                {
                    var galaxyGenerationRequest = World.GetRequest<GalaxyGenerationRequest>();
                    galaxyGenerationRequest.Publish(new GalaxyGenerationRequest
                    {
                        StarSystemsNumber = 1000
                    });
                    gameState.GalaxyGenerationRequestIsCreated = true;
                }

                ref var loadingState = ref entity.GetComponent<LoadingState>();
                var progress = math.clamp(loadingState.LoadingTime / _loadingDuration, 0, 1);
                loadingState.Progress = progress;

                if (loadingState.LoadingTime < _loadingDuration)
                {
                    loadingState.LoadingTime += deltaTime;
                }
                else
                {
                    entity.RemoveComponent<LoadingState>();
                    
                    if (SceneManager.GetSceneByBuildIndex(1).isLoaded)
                    {
                        var loadingSceneUnloadingOperation = SceneManager.UnloadSceneAsync(1);
                        loadingSceneUnloadingOperation.completed += _ => { };
                    }
                }
            }
        }
    }
}