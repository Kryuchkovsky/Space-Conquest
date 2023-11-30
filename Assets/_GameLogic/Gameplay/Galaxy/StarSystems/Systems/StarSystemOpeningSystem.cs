using _GameLogic.Core;
using _GameLogic.Core.GameStates;
using _GameLogic.Extensions;
using _GameLogic.Gameplay.Camera;
using _GameLogic.Gameplay.Galaxy.StarSystems.Planets;
using _GameLogic.Gameplay.Galaxy.StarSystems.Stars;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _GameLogic.Gameplay.Galaxy.StarSystems.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class StarSystemOpeningSystem : AbstractSystem
    {
        private Event<StarSystemClickEvent> _clickEvent;
        private Request<GameCameraSwitchingRequest> _cameraSwitchingRequest;
        private FilterBuilder _stateMachineFilterBuilder;

        public override void OnAwake()
        {
            _clickEvent = World.GetEvent<StarSystemClickEvent>();
            _cameraSwitchingRequest = World.GetRequest<GameCameraSwitchingRequest>();
            _stateMachineFilterBuilder = World.Filter.With<StateMachine>().With<PlayState>().Without<LoadingState>();
        }

        public override void OnUpdate(float deltaTime)
        {
            foreach (var evt in _clickEvent.publishedChanges)
            {
                foreach (var stateMachineEntity in _stateMachineFilterBuilder.Build())
                {
                    ref var playState = ref stateMachineEntity.GetComponent<PlayState>();

                    if (!SceneManager.GetSceneByBuildIndex(4).isLoaded)
                    {
                        var starSystemEntity = evt.Entity;
                        var operation = SceneManager.LoadSceneAsync(4, LoadSceneMode.Additive);
                        operation.completed += _ =>
                        {
                            var starSystemScene = SceneManager.GetSceneByBuildIndex(4);
                            SceneManager.SetActiveScene(starSystemScene);
                            
                            var starSystemComponent = starSystemEntity.GetComponent<StarSystem>();
                            var layer = LayerMask.NameToLayer("StarSystem");
                            var starSystemObject = new GameObject($"[star_system_{starSystemEntity.ID}]");

                            for (int i = 0; i < starSystemComponent.StarEntities.Length; i++)
                            {
                                var starComponent = starSystemComponent.StarEntities[i].GetComponent<Star>();
                                var star = Object.Instantiate(starComponent.Provider, starSystemObject.transform);
                            }

                            for (int i = 0; i < starSystemComponent.PlanetEntities.Length; i++)
                            {
                                var entity = starSystemComponent.PlanetEntities[i];
                                var planetComponent = entity.GetComponent<Planet>();
                                var position = entity.GetComponent<Position>().Value;
                                var planet = Object.Instantiate(planetComponent.Provider, position, Quaternion.identity, starSystemObject.transform);
                                planet.OrbitDrawer.Draw(Vector3.zero);
                            }

                            starSystemObject.transform.SetLayerRecursively(layer);
                            _cameraSwitchingRequest.Publish(new GameCameraSwitchingRequest
                            {
                                CameraIndex = 1
                            });
                        };
                    }
                }
            }
        }
    }
}