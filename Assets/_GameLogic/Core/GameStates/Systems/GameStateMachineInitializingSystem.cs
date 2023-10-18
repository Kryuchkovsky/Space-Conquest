using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace _GameLogic.Core.GameStates.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [CreateAssetMenu(menuName = "ECS/Systems/Core/GameStates/" + nameof(GameStateMachineInitializingSystem))]
    public class GameStateMachineInitializingSystem : Initializer
    {
        public override void OnAwake()
        {
            var entity = World.CreateEntity();
            entity.AddComponent<StateMachine>();
            entity.AddComponent<MainMenuState>();
            entity.AddComponent<LoadingState>();
        }
    }
}