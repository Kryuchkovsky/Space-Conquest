using _GameLogic.Core.GameStates;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _GameLogic.Gameplay.Galaxy.StarSystems.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [CreateAssetMenu(menuName = "ECS/Systems/Gameplay/Galaxy/StarSystems/" + nameof(StarSystemClosingSystem))]
    public class StarSystemClosingSystem : UpdateSystem
    {
        private Event<StarSystemClosingButtonClickEvent> _clickEvent;
        private FilterBuilder _stateMachineFilterBuilder;

        public override void OnAwake()
        {
            _stateMachineFilterBuilder = World.Filter.With<StateMachine>().With<PlayState>().Without<LoadingState>();
            _clickEvent = World.GetEvent<StarSystemClosingButtonClickEvent>();
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
                        };
                    }
                }
            }
        }
    }
}