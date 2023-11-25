using _GameLogic.Core;
using Scellecs.Morpeh;

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
}