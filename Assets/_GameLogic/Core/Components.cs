using Scellecs.Morpeh;
using UnityEngine;

namespace _GameLogic.Core
{
    public struct Transform : IAspect, IFilterExtension
    {
        public Entity Entity { get; set; }

        public ref Position Position => ref _position.Get(Entity);
        public ref Rotation Rotation => ref _rotation.Get(Entity);
        public ref Scale Scale => ref _scale.Get(Entity);

        private Stash<Position> _position;
        private Stash<Rotation> _rotation;
        private Stash<Scale> _scale;

        public void OnGetAspectFactory(World world)
        {
            _position = world.GetStash<Position>();
            _rotation = world.GetStash<Rotation>();
            _scale = world.GetStash<Scale>();
        }
        
        public FilterBuilder Extend(FilterBuilder rootFilter) => rootFilter.With<Position>().With<Rotation>().With<Scale>();
    }

    public struct Position : IComponent
    {
        public Vector3 Value;
    }

    public struct Rotation : IComponent
    {
        public Quaternion Value;
    }

    public struct Scale : IComponent
    {
        public Vector3 Value;
    }
}