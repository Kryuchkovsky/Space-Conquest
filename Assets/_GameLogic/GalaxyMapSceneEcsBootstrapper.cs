using _GameLogic.Core;
using _GameLogic.Gameplay.Galaxy.Generation.Systems;
using _GameLogic.Gameplay.Galaxy.StarSystems.Systems;
using _GameLogic.Gameplay.Time.Systems;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;

namespace _GameLogic
{
    public class GalaxyMapSceneEcsBootstrapper : EcsBootstrapper
    {
        public override World World => World.Default;

        protected override void RegisterSystems()
        {
            AddInitializer<GalaxyMapInitializer>();
        }
    }

    public class GalaxyMapInitializer : Initializer
    {
        private SystemsGroup _systemsGroup;
        
        public override void OnAwake()
        {
            _systemsGroup = World.CreateSystemsGroup();
            _systemsGroup.AddInitializer(new TimeInitializingSystem());
            _systemsGroup.AddSystem(new TimeProcessingSystem());
            _systemsGroup.AddSystem(new GalaxyGenerationRequestProcessingSystem());
            _systemsGroup.AddSystem(new StarSystemOpeningSystem());
            _systemsGroup.AddSystem(new StarSystemClosingSystem());
            World.AddSystemsGroup(4, _systemsGroup);
        }

        public override void Dispose()
        {
            base.Dispose();
            World.RemoveSystemsGroup(_systemsGroup);
        }
    }
}