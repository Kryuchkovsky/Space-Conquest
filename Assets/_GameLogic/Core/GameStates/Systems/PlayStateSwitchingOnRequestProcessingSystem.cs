using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace _GameLogic.Core.GameStates.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [CreateAssetMenu(menuName = "ECS/Systems/Core/GameStates/" + nameof(MainMenuLoadingSystem))]
    public class PlayStateSwitchingOnRequestProcessingSystem : UpdateSystem
    {
        private FilterBuilder _stateMachineFilterBuilder;
        private Request<PlayStateSwitchingOnRequest> _playStateSwitchingOnRequest;
        private readonly float _loadingDuration = 1;

        public override void OnAwake()
        {
            _stateMachineFilterBuilder = World.Filter.With<StateMachine>().With<MainMenuState>().Without<LoadingState>();
            _playStateSwitchingOnRequest = World.GetRequest<PlayStateSwitchingOnRequest>();
        }

        public override void OnUpdate(float deltaTime)
        {
            foreach (var request in _playStateSwitchingOnRequest.Consume())
            {
                foreach (var entity in  _stateMachineFilterBuilder.Build())
                {
                    entity.RemoveComponent<MainMenuState>();
                    entity.AddComponent<PlayState>();
                    entity.AddComponent<LoadingState>();
                }
            }
        }
    }
}