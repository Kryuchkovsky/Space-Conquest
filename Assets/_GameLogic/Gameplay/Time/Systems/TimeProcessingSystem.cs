using _GameLogic.Core;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace _GameLogic.Gameplay.Time.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class TimeProcessingSystem : AbstractSystem
    {
        private FilterBuilder _filterBuilder;
        private float _time;

        public override void OnAwake()
        {
            _filterBuilder = World.Filter.With<GameTime>();
        }

        public override void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filterBuilder.Build())
            {
                ref var gameTime = ref entity.GetComponent<GameTime>();
                _time += UnityEngine.Time.deltaTime * gameTime.TimeSetting.ProcessedDaysPerSecond;

                if (_time >= 1)
                {
                    gameTime.Date = gameTime.Date.AddDays(1);
                    TimeAdjustmentPanel.Instance.SetDate(gameTime.Date);
                    _time = 0;
                }
            }
        }
    }
}