using _GameLogic.Common;
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
        private FilterBuilder _stateMachineFilterBuilder;

        public override void OnAwake()
        {
            _clickEvent = World.GetEvent<StarSystemClickEvent>();
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
                            
                            var starSystem = starSystemEntity.GetComponent<StarSystem>();
                            var layer = LayerMask.NameToLayer("StarSystem");
                            var starSystemObject = new GameObject($"[star_system_{starSystemEntity.ID}]");

                            for (int i = 0; i < starSystem.StarEntities.Length; i++)
                            {
                                var entity = starSystem.StarEntities[i];
                                var data = entity.GetComponent<StarData>().Value;
                                var star = Object.Instantiate(data.Prefab, starSystemObject.transform);
                                entity.SetComponent(new StarSystemObjectViewLink
                                {
                                    Value = star
                                });
                            }

                            for (int i = 0; i < starSystem.PlanetEntities.Length; i++)
                            {
                                var entity = starSystem.PlanetEntities[i];
                                var data = entity.GetComponent<PlanetData>().Value;
                                var position = entity.GetComponent<PositionInStarSystemMap>().Value;
                                var planet = Object.Instantiate(data.Prefab, position, Quaternion.identity, starSystemObject.transform);
                                planet.OrbitDrawer.Draw(Vector3.zero);
                                entity.SetComponent(new StarSystemObjectViewLink
                                {
                                    Value = planet
                                });
                            }

                            starSystemObject.transform.SetLayerRecursively(layer);
                            World.GetRequest<GameCameraSwitchingRequest>().Publish(new GameCameraSwitchingRequest
                            {
                                CameraIndex = 1
                            });
                            World.GetRequest<GameCameraBoundsSettingRequest>().Publish(new GameCameraBoundsSettingRequest
                            {
                                Bounds = starSystemEntity.GetComponent<Boundaries>().Value
                            }, true);
                            
                            GalaxyUIContainer.Instance.gameObject.SetActive(false);
                        };
                    }
                }
            }
        }
    }
}