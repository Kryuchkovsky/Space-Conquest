using Unity.Entities;

namespace _GameLogic.Gameplay.Galaxy
{
    public struct IsGalaxy : IComponentData
    {
    }
    
    public struct GameResourcesData : IComponentData
    {
        public Entity GalaxyPrefab;
        public Entity StarSystemPrefab;
    }
}