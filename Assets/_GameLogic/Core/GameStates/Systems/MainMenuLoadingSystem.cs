using Unity.Entities;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

namespace _GameLogic.Core.GameStates.Systems
{
    public partial class MainMenuLoadingSystem : SystemBase
    {
        private readonly float _loadingDuration = 3;
        private float _loadingTime;
        
        protected override void OnCreate()
        {
            base.OnCreate();
            RequireForUpdate<GameState>();
            RequireForUpdate<LoadingScreenData>();
            World.EntityManager.CreateSingleton<GameState>();
            var entity = SystemAPI.GetSingletonEntity<GameState>();
            EntityManager.AddComponent<LoadingScreenData>(entity);
            _loadingTime = 0;
        }

        protected override void OnUpdate()
        {
            var gameStateEntity = SystemAPI.GetSingletonEntity<GameState>();

            if (EntityManager.HasComponent<LoadingScreenData>(gameStateEntity))
            {
                var data = EntityManager.GetComponentData<LoadingScreenData>(gameStateEntity);
                var progress = math.clamp(_loadingTime / _loadingDuration, 0, 1);
                data.Progress = progress;
                var loadingSceneUIContainer = Singleton<LoadingSceneUIContainer>.instance;
                loadingSceneUIContainer.BarFillingImage.fillAmount = progress;
                loadingSceneUIContainer.LoadingProgressText.SetText("{0:0}", progress * 100);

                if (_loadingTime < _loadingDuration)
                {
                    _loadingTime += SystemAPI.Time.DeltaTime;
                    EntityManager.SetComponentData(gameStateEntity, data);
                }
                else
                {
                    SceneManager.LoadScene(1);
                    EntityManager.RemoveComponent<LoadingScreenData>(gameStateEntity);
                }
            }
        }
    }
}