using _GameLogic.Gameplay.Galaxy.Generation;
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
            EntityManager.AddComponent<IsGameState>(entity);
            EntityManager.AddComponent<LoadingStateProcess>(entity);
            EntityManager.AddComponent<GalaxyGenerationRequest>(entity);
            EntityManager.SetComponentEnabled<IsGameState>(entity, false);
            EntityManager.SetComponentEnabled<GalaxyGenerationRequest>(entity, false);
        }

        protected override void OnUpdate()
        {
        }
    }
}