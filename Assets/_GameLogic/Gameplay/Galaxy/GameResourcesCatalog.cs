using _GameLogic.Gameplay.Galaxy.StarSystems;
using UnityEngine;

namespace _GameLogic.Gameplay.Galaxy
{
    [CreateAssetMenu(menuName = "Configs/")]
    public class GameResourcesProvider : ScriptableObject
    {
        [field: SerializeField] public GalaxyView GalaxyView { get; private set; }
        [field: SerializeField] public StarSystemProvider StarSystemProvider { get; private set; }
        
    }
}