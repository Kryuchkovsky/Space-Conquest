using Unity.Entities;

namespace _GameLogic.Loading
{
    public struct LoadingStateProcess : IComponentData
    {
        public float Progress;
        public float LoadingTime;
    }
}