using _GameLogic.Core.GameStates;
using Unity.Entities;
using Unity.VisualScripting;

namespace _GameLogic.Loading.Systems
{
    public partial class LoadingScreenDisplayingSystem : SystemBase
    {
        protected override void OnCreate()
        {
            base.OnCreate();
            RequireForUpdate<LoadingState>();
        }

        protected override void OnUpdate()
        {
            foreach (var data in SystemAPI.Query<LoadingState>())
            {
                if (data.SceneIsLoaded)
                {
                    var loadingSceneUIContainer = Singleton<LoadingSceneUIContainer>.instance;
                    loadingSceneUIContainer.BarFillingImage.fillAmount = data.Progress;
                    loadingSceneUIContainer.LoadingProgressText.SetText("{0:0}", data.Progress * 100);
                }
            }
        }
    }
}
