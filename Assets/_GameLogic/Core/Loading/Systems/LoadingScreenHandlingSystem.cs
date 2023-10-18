using _GameLogic.Core.GameStates;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace _GameLogic.Core.Loading.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [CreateAssetMenu(menuName = "ECS/Systems/Core/Loading/" + nameof(LoadingScreenHandlingSystem))]
    public class LoadingScreenHandlingSystem : UpdateSystem
    {
        private FilterBuilder _loadingStateFilterBuilder;
        private FilterBuilder _loadingScreenUIFilterBuilder;
        
        public override void OnAwake()
        {
            _loadingStateFilterBuilder = World.Filter.With<LoadingState>();
            _loadingScreenUIFilterBuilder = World.Filter.With<LoadingSceneUI>();
        }

        public override void OnUpdate(float deltaTime)
        {
            foreach (var entity in _loadingStateFilterBuilder.Build())
            {
                ref var state = ref entity.GetComponent<LoadingState>();

                if (state.SceneIsLoaded)
                {
                    var loadingSceneUI = _loadingScreenUIFilterBuilder.Build().First().GetComponent<LoadingSceneUI>().Value;
                    loadingSceneUI.BarFillingImage.fillAmount = state.Progress;
                    loadingSceneUI.LoadingProgressText.SetText("{0:0}", state.Progress * 100);
                }
            }
        }
    }
}
