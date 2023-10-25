using UnityEngine;

namespace _GameLogic.Gameplay.Galaxy.StarSystems.Planets
{
    [CreateAssetMenu(menuName = "Create PlanetCatalog", fileName = "PlanetCatalog", order = 0)]
    public class PlanetsCatalog : ScriptableObject
    {
        [SerializeField] private PlanetData[] _planetsData =
        {
            new(Classifications.Arid, 80),
            new(Classifications.Desert, 80),
            new(Classifications.Savanna, 80),
            new(Classifications.Alpine, 80),
            new(Classifications.Arctic, 80),
            new(Classifications.Tundra, 80),
            new(Classifications.Continental, 80),
            new(Classifications.Ocean, 80),
            new(Classifications.Tropical, 80),
            new(Classifications.Hive, 80),
            new(Classifications.Machine, 80),
            new(Classifications.Barren, 80),
            new(Classifications.Cracked, 80),
            new(Classifications.GasGiant, 80),
            new(Classifications.GasGiant, 80),
            new(Classifications.Lava, 80),
            new(Classifications.Toxic, 80)
        };
    }
}