using UnityEngine;

namespace _GameLogic.Gameplay.Galaxy.StarSystems.Planets
{
    [CreateAssetMenu(menuName = "Create PlanetCatalog", fileName = "PlanetCatalog", order = 0)]
    public class PlanetsCatalog : ScriptableObject
    {
        [SerializeField] private PlanetCatalogData[] _planetsData =
        {
            new(PlanetType.Arid, 80),
            new(PlanetType.Desert, 80),
            new(PlanetType.Savannah, 80),
            new(PlanetType.Alpine, 80),
            new(PlanetType.Arctic, 80),
            new(PlanetType.Tundra, 80),
            new(PlanetType.Continental, 80),
            new(PlanetType.Ocean, 80),
            new(PlanetType.Tropical, 80),
            new(PlanetType.Hive, 80),
            new(PlanetType.Machine, 80),
            new(PlanetType.Barren, 80),
            new(PlanetType.Cracked, 80),
            new(PlanetType.GasGiant, 80),
            new(PlanetType.Lava, 80),
            new(PlanetType.Toxic, 80)
        };

        public PlanetCatalogData GetRandomData() => _planetsData[Random.Range(0, _planetsData.Length)];
    }
}