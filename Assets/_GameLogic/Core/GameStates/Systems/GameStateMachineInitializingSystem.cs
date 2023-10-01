using _GameLogic.Loading;
using Unity.Entities;

namespace _GameLogic.Core.GameStates.Systems
{
    public partial class GameStateMachineInitializingSystem : SystemBase
    {
        protected override void OnStartRunning()
        {
            base.OnStartRunning();
            
            var entity = EntityManager.CreateSingleton<IsStateMachine>();
            EntityManager.AddComponent<IsMainMenuState>(entity);
            EntityManager.AddComponent<LoadingStateProcess>(entity);
        }

        protected override void OnUpdate()
        {
        }
    }
}