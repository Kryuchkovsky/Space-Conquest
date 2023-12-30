using _GameLogic.Core;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;

namespace _GameLogic.Common
{
    public abstract class ExtendedMonoProvider<T> : MonoProvider<T> where T : struct, IComponent
    {
        protected override void Initialize()
        {
            base.Initialize();
            Entity.SetComponent(new TransformLink
            {
                Value = transform
            });
        }
    }
}