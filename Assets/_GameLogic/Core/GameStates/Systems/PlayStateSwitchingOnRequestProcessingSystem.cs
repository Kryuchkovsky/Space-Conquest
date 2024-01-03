using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _GameLogic.Core.GameStates.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [CreateAssetMenu(menuName = "ECS/Systems/Core/GameStates/" + nameof(PlayStateSwitchingOnRequestProcessingSystem))]
    public class PlayStateSwitchingOnRequestProcessingSystem : UpdateSystem
    {
        private FilterBuilder _stateMachineFilterBuilder;
        private Request<PlayStateSwitchingOnRequest> _playStateSwitchingOnRequest;
        private readonly float _loadingDuration = 1;

        public override void OnAwake()
        {
            _stateMachineFilterBuilder = World.Filter.With<StateMachine>().With<MainMenuState>().Without<PlayState>().Without<LoadingState>();
            _playStateSwitchingOnRequest = World.GetRequest<PlayStateSwitchingOnRequest>();
        }

        public override void OnUpdate(float deltaTime)
        {
            foreach (var request in _playStateSwitchingOnRequest.Consume())
            {
                foreach (var entity in  _stateMachineFilterBuilder.Build())
                {
                    if (SceneManager.GetSceneByBuildIndex(2).isLoaded)
                    {
                        var mainMenuSceneUnloadingOperation = SceneManager.UnloadSceneAsync(2);
                        mainMenuSceneUnloadingOperation.completed += _ =>
                        {
                            entity.RemoveComponent<MainMenuState>();
                        };
                    }
                    
                    if (!SceneManager.GetSceneByBuildIndex(1).isLoaded)
                    {
                        var loadingSceneLoadingOperation = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
                        loadingSceneLoadingOperation.completed += _ =>
                        {
                            var loadingScene = SceneManager.GetSceneByBuildIndex(1);
                            SceneManager.SetActiveScene(loadingScene);
                            
                            if (!SceneManager.GetSceneByBuildIndex(3).isLoaded)
                            {
                                var galaxyMapSceneLoadingOperation = SceneManager.LoadSceneAsync(3, LoadSceneMode.Additive);
                                galaxyMapSceneLoadingOperation.completed += _ =>
                                {
                                    var galaxyMapScene = SceneManager.GetSceneByBuildIndex(3);
                                    SceneManager.SetActiveScene(galaxyMapScene);
                                    var playState = new PlayState();
                                    playState.GalaxySceneIsLoaded = true;
                                    entity.SetComponent(playState);
                                };
                            }
                        };
                        var loadingState = new LoadingState();
                        loadingState.SceneIsLoaded = true;
                        entity.SetComponent(loadingState);
                    }
                }
            }
        }
    }
}