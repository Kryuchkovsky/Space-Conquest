using System;
using _GameLogic.Core;
using _GameLogic.Extensions.Configs;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace _GameLogic.Gameplay.Time.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class TimeLapseModeSwitchingSystem : AbstractSystem
    {
        private FilterBuilder _filterBuilder;
        private Event<TimeAccelerationButtonClickEvent> _timeAccelerationButtonClickEvent;
        private Event<TimeDecelerationButtonClickEvent> _timeDecelerationButtonClickEvent;
        private Event<TimeStateSwitchingButtonClickEvent> _timeStateSwitchingButtonClickEvent;
        private TimeSettings _timeSettings;

        public override void OnAwake()
        {
            _filterBuilder = World.Filter.With<GameTime>();
            _timeAccelerationButtonClickEvent = World.GetEvent<TimeAccelerationButtonClickEvent>();
            _timeDecelerationButtonClickEvent = World.GetEvent<TimeDecelerationButtonClickEvent>();
            _timeStateSwitchingButtonClickEvent = World.GetEvent<TimeStateSwitchingButtonClickEvent>();
            _timeSettings = ConfigsManager.GetConfig<TimeSettings>();
        }

        public override void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filterBuilder.Build())
            {
                ref var gameTime = ref entity.GetComponent<GameTime>();
                
                foreach (var evt in _timeAccelerationButtonClickEvent.publishedChanges)
                {
                    var timeMode = gameTime.TimeSetting.Mode switch
                    {
                        TimeLapseMode.Slowest => TimeLapseMode.Slow,
                        TimeLapseMode.Slow => TimeLapseMode.Normal,
                        TimeLapseMode.Normal => TimeLapseMode.Fast,
                        TimeLapseMode.Fast => TimeLapseMode.Fastest,
                        TimeLapseMode.Fastest => TimeLapseMode.Fastest,
                        _ => throw new ArgumentOutOfRangeException()
                    };
                    
                    gameTime.TimeSetting = _timeSettings.GetTimeSetting(timeMode);
                    TimeAdjustmentPanel.Instance.SetTimeLapseMode(timeMode);
                }
            
                foreach (var evt in _timeDecelerationButtonClickEvent.publishedChanges)
                {
                    var timeMode = gameTime.TimeSetting.Mode switch
                    {
                        TimeLapseMode.Slowest => TimeLapseMode.Slowest,
                        TimeLapseMode.Slow => TimeLapseMode.Slowest,
                        TimeLapseMode.Normal => TimeLapseMode.Slow,
                        TimeLapseMode.Fast => TimeLapseMode.Normal,
                        TimeLapseMode.Fastest => TimeLapseMode.Fast,
                        _ => throw new ArgumentOutOfRangeException()
                    };
                    
                    gameTime.TimeSetting = _timeSettings.GetTimeSetting(timeMode);
                    TimeAdjustmentPanel.Instance.SetTimeLapseMode(timeMode);
                }
            
                foreach (var evt in _timeStateSwitchingButtonClickEvent.publishedChanges)
                {
                    gameTime.IsPaused = !gameTime.IsPaused;
                    TimeAdjustmentPanel.Instance.SetTimeState(gameTime.IsPaused);
                }
            }
        }
    }
}