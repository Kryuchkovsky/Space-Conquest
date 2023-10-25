using System;
using UnityEngine;

namespace _GameLogic.Gameplay.Galaxy
{
    [Serializable]
    public abstract class CelestialBodyData
    {
        [field: SerializeField] public int ResourceModifier { get; private set; }
        [field: SerializeField] public bool IsHabitable { get; private set; }

        public CelestialBodyData(int resourceModifier = 100, bool isHabitable = false)
        {
            ResourceModifier = resourceModifier;
            IsHabitable = isHabitable;
        }
    }
}