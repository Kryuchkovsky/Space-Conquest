using Unity.Entities;

namespace _GameLogic.Core.GameStates
{
    public struct StateMachine : IComponentData
    {
    }
    
    public struct LoadingState : IComponentData
    {
        public float Progress;
        public float LoadingTime;
        public bool SceneIsLoaded;
    }

    public struct MainMenuState : IComponentData
    {
        public bool SceneIsLoaded;
    }

    public struct GameState : IComponentData
    {
        public bool SceneIsLoaded;
    }
}