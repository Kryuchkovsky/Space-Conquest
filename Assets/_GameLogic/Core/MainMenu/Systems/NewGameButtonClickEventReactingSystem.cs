using _GameLogic.Core.GameStates;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace _GameLogic.Core.MainMenu.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [CreateAssetMenu(menuName = "ECS/Systems/Core/MainMenu/" + nameof(NewGameButtonClickEventReactingSystem))]
    public class NewGameButtonClickEventReactingSystem : UpdateSystem
    {
        private Event<NewGameButtonClickEvent> _newGameButtonClickEvent;

        public override void OnAwake()
        {
            _newGameButtonClickEvent = World.GetEvent<NewGameButtonClickEvent>();
        }

        public override void OnUpdate(float deltaTime)
        {
            foreach (var ent in _newGameButtonClickEvent.publishedChanges)
            {
                var request = World.GetRequest<PlayStateSwitchingOnRequest>();
                request.Publish(new PlayStateSwitchingOnRequest(), true);
            }
        }
    }
}