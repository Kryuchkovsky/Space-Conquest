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
    [CreateAssetMenu(menuName = "ECS/Systems/Gameplay/Galaxy/StarSystems/" + nameof(StarSystemMapOpeningSystem))]
    public class StarSystemMapOpeningSystem : UpdateSystem
    {
        private FilterBuilder _stateMachineFilterBuilder;
        private Event<StarSystemClickEvent> _clickEvent;
        
        public override void OnAwake()
        {
            _stateMachineFilterBuilder = World.Filter.With<StateMachine>().With<PlayState>().Without<LoadingState>();
            _clickEvent = World.GetEvent<StarSystemClickEvent>();
        }

        public override void OnUpdate(float deltaTime)
        {
            foreach (var evt in _clickEvent.publishedChanges)           
            {
                foreach (var entity in _stateMachineFilterBuilder.Build())
                {
                    ref var playState = ref entity.GetComponent<PlayState>();
                    
                    if (!SceneManager.GetSceneByBuildIndex(4).isLoaded)
                    {
                        var operation = SceneManager.LoadSceneAsync(4);
                        operation.completed += _ =>
                        {
                        };
                    }
                }
            }
        }
    }
}