using _GameLogic.Core.GameStates.Systems;
using _GameLogic.Core.Loading.Systems;
using _GameLogic.Core.MainMenu.Systems;
using _GameLogic.Gameplay.Galaxy.Generation.Systems;
using _GameLogic.Gameplay.Galaxy.StarSystems.Systems;
using Scellecs.Morpeh;

namespace _GameLogic.Core
{
    public class LogicSceneEcsBootstrapper : EcsBootstrapper
    {
        public override World World => World.Default;

        protected override void RegisterSystems()
        {
            AddInitializer<GameStateMachineInitializingSystem>();

            AddSystem<GalaxyLoadingSystem>();
            AddSystem<LoadingScreenHandlingSystem>();
            
            AddSystem<MainMenuLoadingSystem>();
            AddSystem<NewGameButtonClickEventReactingSystem>();

            AddSystem<PlayStateSwitchingOnRequestProcessingSystem>();
            AddSystem<GalaxyGenerationRequestProcessingSystem>();
            AddSystem<StarSystemOpeningSystem>();
            AddSystem<StarSystemClosingSystem>();
        }
    }
}