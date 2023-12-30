using _GameLogic.Core;
using _GameLogic.Core.GameStates;
using _GameLogic.Gameplay.Camera;
using _GameLogic.Gameplay.Galaxy.StarSystems.Planets;
using _GameLogic.Gameplay.Galaxy.StarSystems.Stars;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine.SceneManagement;

namespace _GameLogic.Gameplay.Galaxy.StarSystems.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class StarSystemClosingSystem : AbstractSystem
    {
        private Event<StarSystemClosingButtonClickEvent> _clickEvent;
        private Request<GameCameraSwitchingRequest> _cameraSwitchingRequest;
        private FilterBuilder _stateMachineFilterBuilder;
        private FilterBuilder _starSystemObjectFilterBuilder;
        private FilterBuilder _planetViewFilterBuilder;

        public override void OnAwake()
        {
            _clickEvent = World.GetEvent<StarSystemClosingButtonClickEvent>();
            _cameraSwitchingRequest = World.GetRequest<GameCameraSwitchingRequest>();
            _stateMachineFilterBuilder = World.Filter.With<StateMachine>().With<PlayState>().Without<LoadingState>();
            _starSystemObjectFilterBuilder = World.Filter.With<StarSystemObjectViewLink>();
        }

        public override void OnUpdate(float deltaTime)
        {
            foreach (var evt in _clickEvent.publishedChanges)
            {
                foreach (var stateMachineEntity in _stateMachineFilterBuilder.Build())
                {
                    if (SceneManager.GetSceneByBuildIndex(4).isLoaded)
                    {
                        var operation = SceneManager.UnloadSceneAsync(4);
                        operation.completed += _ =>
                        {
                            var starSystemScene = SceneManager.GetSceneByBuildIndex(3);
                            SceneManager.SetActiveScene(starSystemScene);
                            _cameraSwitchingRequest.Publish(new GameCameraSwitchingRequest
                            {
                                CameraIndex = 0
                            });

                            foreach (var entity in _starSystemObjectFilterBuilder.Build())
                            {
                                entity.RemoveComponent<StarSystemObjectViewLink>();
                            }
                        };
                    }
                }
            }
        }
    }
}