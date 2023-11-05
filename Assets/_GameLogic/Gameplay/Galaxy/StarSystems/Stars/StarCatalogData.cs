using System;
using UnityEngine;

namespace _GameLogic.Gameplay.Galaxy.StarSystems.Stars
{
    [Serializable]
    public class StarCatalogData : CelestialBodyCatalogData
    {
        [field: SerializeField] public StarProvider Prefab { get; set; }
        [field: SerializeField] public StarType StarType { get; set; }
        [field: SerializeField] public float SpreadingRate { get; set; }

        public StarCatalogData(StarType starType, float spreadingRate, int resourceModifier = 0) : base(resourceModifier)
        {
            StarType = starType;
            SpreadingRate = spreadingRate;
        }
    }
}