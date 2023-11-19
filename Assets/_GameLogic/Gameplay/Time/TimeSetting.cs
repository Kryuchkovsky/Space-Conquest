using System;
using UnityEngine;

namespace _GameLogic.Gameplay.Time
{
    [Serializable]
    public class TimeSetting
    {
        [field: SerializeField] public TimeLapseMode Mode { get; private set; }
        [field: SerializeField, Range(0.1f, 30)] public float ProcessedDaysPerSecond { get; private set; } = 1;

        public TimeSetting(TimeLapseMode mode, float processedDaysPerSecond)
        {
            Mode = mode;
            ProcessedDaysPerSecond = processedDaysPerSecond;
        }
    }
}