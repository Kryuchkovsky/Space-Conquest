using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace _GameLogic.Gameplay.Galaxy
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [System.Serializable]
    public struct GalaxyLink : IComponent
    {
        public GalaxyProvider Value;
    }
    
    public struct HabitableFlag : IComponent
    {
    }
    
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [System.Serializable]
    public struct StellarObjectLabelLink : IComponent
    {
        public StellarObjectLabel Value;
    }
    
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [System.Serializable]
    public struct StellarObjectData : IComponent
    {
        public string Name;
    }

    public struct PositionInGalaxyMap : IComponent
    {
        public Vector3 Value;
    }
    
    public struct PositionInStarSystemMap : IComponent
    {
        public Vector3 Value;
    }
}