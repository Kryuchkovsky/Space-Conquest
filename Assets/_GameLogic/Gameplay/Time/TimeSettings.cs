using System;
using System.Collections.Generic;
using System.Linq;
using TriInspector;
using UnityEngine;

namespace _GameLogic.Gameplay.Time
{
    [CreateAssetMenu(menuName = "Create TimeSettings", fileName = "TimeSettings", order = 2)]
    public class TimeSettings : ScriptableObject
    {
        [SerializeField, ReadOnly] private List<TimeSetting> _timeSettings = new()
        {
            new TimeSetting(TimeLapseMode.Slowest, 0.75f),
            new TimeSetting(TimeLapseMode.Slow, 1.5f),
            new TimeSetting(TimeLapseMode.Normal, 3),
            new TimeSetting(TimeLapseMode.Fast, 6),
            new TimeSetting(TimeLapseMode.Fastest, 12)
        };

        [field: SerializeField] public DateTime GameStartDate { get; set; } = new (3000, 1, 1);

        public TimeSetting GetTimeSetting(TimeLapseMode mode) => _timeSettings.First(s => s.Mode == mode);
    }
}