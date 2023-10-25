using _GameLogic.Gameplay.Galaxy.StarSystems;
using Unity.VisualScripting;
using UnityEngine;

namespace _GameLogic.Gameplay.Galaxy
{
    [CreateAssetMenu(menuName = "Configs/" + nameof(GameResourcesCatalog))]
    public class GameResourcesCatalog : ScriptableObject
    {
        [field: SerializeField] public GalaxyProvider GalaxyPrefab { get; private set; }
        [field: SerializeField] public StarSystemProvider StarSystemPrefab { get; private set; }
    }
}