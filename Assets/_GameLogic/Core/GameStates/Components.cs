using Unity.Entities;

namespace _GameLogic.Core.GameStates
{
    public struct GameState : IComponentData
    {
    }
    
    public struct LoadingScreenData : IComponentData
    {
        public float Progress;
    }

    public struct MainMenuLoading : IComponentData
    {
    }
}