using _GameLogic.Loading;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine.SceneManagement;

namespace _GameLogic.Core.GameStates.Systems
{
    public partial class MainMenuLoadingSystem : SystemBase
    {
        private readonly float _loadingDuration = 1;

        protected override void OnCreate()
        {
            base.OnCreate();
            RequireForUpdate<IsStateMachine>();
            RequireForUpdate<IsMainMenuState>();
            RequireForUpdate<LoadingStateProcess>();
        }

        protected override void OnStartRunning()
        {
            base.OnStartRunning();
            var entity = SystemAPI.GetSingletonEntity<IsStateMachine>();
            EntityManager.SetComponentData(entity, new LoadingStateProcess
            {
                Progress = 0,
                LoadingTime = 0
            });
        }

        protected override void OnUpdate()
        {
            foreach (var (loadingStateProcess, entity) in SystemAPI.Query<LoadingStateProcess>()
                         .WithAll<IsMainMenuState>().WithEntityAccess())
            {
                var data = loadingStateProcess;
                var progress = math.clamp(data.LoadingTime / _loadingDuration, 0, 1);
                data.Progress = progress;

                if (data.LoadingTime < _loadingDuration)
                {
                    data.LoadingTime += SystemAPI.Time.DeltaTime;
                    EntityManager.SetComponentData(entity, data);
                }
                else
                {
                    var operation = SceneManager.LoadSceneAsync(1);
                    operation.completed += _ => EntityManager.RemoveComponent<LoadingStateProcess>(entity);
                }
            }
        }
    }
}