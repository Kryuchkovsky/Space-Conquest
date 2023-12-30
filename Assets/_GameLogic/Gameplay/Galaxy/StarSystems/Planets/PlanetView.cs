using UnityEngine;

namespace _GameLogic.Gameplay.Galaxy.StarSystems.Planets
{
    public class PlanetView : StarSystemObjectView
    {
        [field: SerializeField] public MeshRenderer PlanetMeshRenderer { get; private set; }
        [field: SerializeField] public MeshRenderer CloudsMeshRenderer { get; private set; }
        [field: SerializeField] public OrbitDrawer OrbitDrawer { get; private set; }
    }
}