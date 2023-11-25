using System;
using Scellecs.Morpeh;

namespace _GameLogic.Gameplay.Time
{
    public struct GameTime : IComponent
    {
        public TimeSetting TimeSetting;
        public DateTime Date;
        public bool IsPaused;
    }
}