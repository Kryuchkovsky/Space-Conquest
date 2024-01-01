using Scellecs.Morpeh;

namespace _GameLogic.Common
{
    public struct GameCameraLink : IComponent
    {
        public UnityEngine.Camera Value;
    }

    public struct Index : IComponent
    {
        public int Value;
    }

    public struct IsActiveFlag : IComponent
    {
    }

    public struct Boundaries : IComponent
    {
        public UnityEngine.Bounds Value;
    }
}