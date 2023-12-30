using _GameLogic.Gameplay.Galaxy.StarSystems;
using UnityEngine;

namespace _GameLogic.Gameplay.Galaxy
{
    [CreateAssetMenu(menuName = "Create GameResourcesCatalog", fileName = "GameResourcesCatalog")]
    public class GameResourcesCatalog : ScriptableObject
    {
        [field: SerializeField] public GalaxyProvider GalaxyPrefab { get; private set; }
        [field: SerializeField] public StarSystemProvider StarSystemPrefab { get; private set; }
        [field: SerializeField] public StellarObjectLabel StellarObjectLabelPrefab { get; private set; }
    }
}