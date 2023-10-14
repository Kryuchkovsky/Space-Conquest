using _GameLogic.Loading;
using _GameLogic.MainMenu;
using Unity.Entities;

namespace _GameLogic.Core.GameStates.Systems
{
    public partial class NewGameButtonClickEventProcessingSystem : SystemBase
    {
        protected override void OnCreate()
        {
            base.OnCreate();
            RequireForUpdate<NewGameButtonClickEvent>();
        }

        protected override void OnUpdate()
        {
            var ecb = SystemAPI
                .GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>()
                .CreateCommandBuffer(World.Unmanaged);
            
            foreach (var (clickEvent, entity) in SystemAPI.Query<NewGameButtonClickEvent>().WithEntityAccess())
            {
                ecb.DestroyEntity(entity);
            }
            
            foreach (var (isStateMachine, entity) in  SystemAPI.Query<StateMachine>()
                         .WithAll<MainMenuState>().WithNone<GameState, LoadingState>().WithEntityAccess())
            {
                ecb.RemoveComponent<MainMenuState>(entity);
                ecb.AddComponent<GameState>(entity);
                ecb.AddComponent<LoadingState>(entity);
            }
        }
    }
}