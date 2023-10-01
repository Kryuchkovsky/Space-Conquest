using Unity.Entities;

namespace _GameLogic.Gameplay.Galaxy.Generation
{
    public struct GalaxyGenerationRequest : IComponentData
    {
        public int StarSystemsNumber;
    }
}