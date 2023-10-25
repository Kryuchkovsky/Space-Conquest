using System;
using UnityEngine;

namespace _GameLogic.Gameplay.Galaxy.StarSystems.Stars
{
    [Serializable]
    public class StarData : CelestialBodyData
    {
        [field: SerializeField] public StarProvider Prefab { get; private set; }
        [field: SerializeField] public SpectralType SpectralType { get; private set; }
        [field: SerializeField] public float SpreadingRate { get; private set; }

        public StarData(SpectralType spectralType, float spreadingRate, int resourceModifier = 0) : base(resourceModifier)
        {
            SpectralType = spectralType;
            SpreadingRate = spreadingRate;
        }
    }
}