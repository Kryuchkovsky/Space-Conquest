using _GameLogic.Core;
using _GameLogic.Extensions.Configs;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace _GameLogic.Gameplay.Time.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class TimeInitializingSystem : AbstractInitializer
    {
        private TimeSettings _timeSettings;

        public override void OnAwake()
        {
            _timeSettings = ConfigsManager.GetConfig<TimeSettings>();
            var entity = World.CreateEntity();
            entity.SetComponent(new GameTime
            {
                TimeSetting = _timeSettings.GetTimeSetting(TimeLapseMode.Normal),
                Date = _timeSettings.GameStartDate
            });
        }
    }
}