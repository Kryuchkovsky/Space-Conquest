using Scellecs.Morpeh.Providers;
using UnityEngine;

namespace _GameLogic.Gameplay.Galaxy.StarSystems.Planets
{
    public class PlanetProvider : MonoProvider<Planet>
    {
        [field: SerializeField] public MeshRenderer PlanetMeshRenderer { get; private set; }
        [field: SerializeField] public MeshRenderer CloudsMeshRenderer { get; private set; }
    }
}