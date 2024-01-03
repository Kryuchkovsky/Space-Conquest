using _GameLogic.Common;
using Scellecs.Morpeh;

namespace _GameLogic.Gameplay.Galaxy
{
    public abstract class GalaxyObjectProvider<T> : ExtendedMonoProvider<T> where T : struct, IComponent
    {
        protected override void Initialize()
        {
            base.Initialize();
            Entity.SetComponent(new GalaxyObjectFlag());
        }
    }
}