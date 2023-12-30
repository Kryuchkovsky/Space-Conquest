using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace _GameLogic.Gameplay.Galaxy.StarSystems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [System.Serializable]
    public struct StarSystem : IComponent
    {
        public StarSystemProvider Provider;
        public Entity[] StarEntities;
        public Entity[] PlanetEntities;
    }
    
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct StarSystemObjectViewLink : IComponent
    {
        public StarSystemObjectView Value;
    }
}