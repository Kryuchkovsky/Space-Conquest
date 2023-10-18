using _GameLogic.MainMenu;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace _GameLogic.Core.GameStates.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [CreateAssetMenu(menuName = "ECS/Systems/" + nameof(NewGameButtonClickEventProcessingSystem))]
    public class NewGameButtonClickEventProcessingSystem : UpdateSystem
    {
        private Event<NewGameButtonClickEvent> _newGameButtonClickEvent;
        private FilterBuilder _stateMachineFilterBuilder;

        public override void OnAwake()
        {
            _newGameButtonClickEvent = World.GetEvent<NewGameButtonClickEvent>();
            _stateMachineFilterBuilder = World.Filter
                .With<StateMachine>()
                .With<MainMenuState>()
                .Without<GameState>()
                .Without<LoadingState>();
        }

        public override void OnUpdate(float deltaTime)
        {
            foreach (var entity in _newGameButtonClickEvent.publishedChanges)
            {
            }
            
            foreach (var entity in  _stateMachineFilterBuilder.Build())
            {
                entity.RemoveComponent<MainMenuState>();
                entity.AddComponent<GameState>();
                entity.AddComponent<LoadingState>();
            }
        }
    }
}