using _GameLogic.Loading;
using Unity.Entities;

namespace _GameLogic.Core.GameStates.Systems
{
    public partial class GameStateMachineInitializingSystem : SystemBase
    {
        protected override void OnStartRunning()
        {
            base.OnStartRunning();
            
            var entity = EntityManager.CreateSingleton<StateMachine>();
            EntityManager.AddComponent<MainMenuState>(entity);
            EntityManager.AddComponent<LoadingState>(entity);
        }

        protected override void OnUpdate()
        {
        }
    }
}