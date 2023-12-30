using System;
using System.Collections.Generic;
using UnityEngine;

namespace _GameLogic.Gameplay.Galaxy.StarSystems.Planets
{
    [Serializable]
    public class PlanetCatalogData : CelestialBodyCatalogData
    {
        [field: SerializeField] public List<Material> PlanetMaterials { get; private set; }
        [field: SerializeField] public List<Material> CloudsMaterials { get; private set; }
        [field: SerializeField] public PlanetView Prefab { get; private set; }
        [field: SerializeField] public PlanetType Type { get; private set; }

        public PlanetCatalogData(PlanetType type, int resourceModifier = 0) : base(resourceModifier, true)
        {
            Type = type;
        }
    }
}