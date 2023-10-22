using _GameLogic.Core.GameStates.Systems;
using Scellecs.Morpeh;

namespace _GameLogic.Core
{
    public class LogicSceneEcsBootstrapper : EcsBootstrapper
    {
        public override World World => World.Default;

        protected override void RegisterSystems()
        {
            AddInitializer<GameStateMachineInitializingSystem>()
                .AddSystem<GalaxyLoadingSystem>()
                .AddSystem<MainMenuLoadingSystem>()
                .AddSystem<PlayStateSwitchingOnRequestProcessingSystem>();
        }
    }
}