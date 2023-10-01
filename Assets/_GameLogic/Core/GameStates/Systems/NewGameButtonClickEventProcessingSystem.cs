using _GameLogic.Loading;
using _GameLogic.MainMenu;
using Unity.Entities;
using Unity.VisualScripting;
using UnityEngine;

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
            
            foreach (var (isStateMachine, entity) in  SystemAPI.Query<IsStateMachine>()
                         .WithAll<IsMainMenuState>().WithNone<IsGameState, LoadingStateProcess>().WithEntityAccess())
            {
                ecb.RemoveComponent<IsMainMenuState>(entity);
                ecb.AddComponent<IsGameState>(entity);
                ecb.AddComponent<LoadingStateProcess>(entity);
            }
        }
    }
}