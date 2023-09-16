using Unity.Entities;
using Unity.VisualScripting;
using UnityEngine;

namespace _GameLogic.Core.GameStates.Systems
{
    public partial class LoadingScreenHandlingSystem : SystemBase
    {
        protected override void OnCreate()
        {
            base.OnCreate();
            RequireForUpdate<GameState>();
            RequireForUpdate<LoadingScreenData>();
        }

        protected override void OnUpdate()
        {
            var gameStateEntity = SystemAPI.GetSingletonEntity<GameState>();

            if (SystemAPI.HasComponent<LoadingScreenData>(gameStateEntity))
            {
                var data = SystemAPI.GetComponentRW<LoadingScreenData>(gameStateEntity).ValueRW;
                var loadingSceneUIContainer = Singleton<LoadingSceneUIContainer>.instance;
                loadingSceneUIContainer.BarFillingImage.fillAmount = data.Progress;
                loadingSceneUIContainer.LoadingProgressText.SetText("{0:0}", data.Progress * 100);
            }
        }
    }
}
