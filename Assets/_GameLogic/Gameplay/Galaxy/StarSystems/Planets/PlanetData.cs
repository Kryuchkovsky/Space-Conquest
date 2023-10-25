using System;
using UnityEngine;

namespace _GameLogic.Gameplay.Galaxy.StarSystems.Planets
{
    [Serializable]
    public class PlanetData : CelestialBodyData
    {
        [field: SerializeField] public PlanetProvider Prefab { get; private set; }
        [field: SerializeField] public Classifications Type { get; private set; }

        public PlanetData(Classifications type, int resourceModifier = 0) : base(resourceModifier, true)
        {
            Type = type;
        }
    }
}