using Scellecs.Morpeh;

namespace _GameLogic.Gameplay.Galaxy.StarSystems
{
    public struct StarSystemClosingButtonClickEvent : IEventData
    {
    }
    
    public struct StarSystemClickEvent : IEventData
    {
        public Entity Entity;
    }
}