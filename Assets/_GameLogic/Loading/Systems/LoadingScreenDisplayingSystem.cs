using Unity.Entities;
using Unity.VisualScripting;

namespace _GameLogic.Loading.Systems
{
    public partial class LoadingScreenHandlingSystem : SystemBase
    {
        protected override void OnCreate()
        {
            base.OnCreate();
            RequireForUpdate<LoadingStateProcess>();
        }

        protected override void OnUpdate()
        {
            //if (!Singleton<LoadingSceneUIContainer>.instantiated) return;
            
            foreach (var data in SystemAPI.Query<LoadingStateProcess>())
            {
                var loadingSceneUIContainer = Singleton<LoadingSceneUIContainer>.instance;
                loadingSceneUIContainer.BarFillingImage.fillAmount = data.Progress;
                loadingSceneUIContainer.LoadingProgressText.SetText("{0:0}", data.Progress * 100);
            }
        }
    }
}
