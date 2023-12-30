using System;
using _GameLogic.Common;
using Scellecs.Morpeh;

namespace _GameLogic.Gameplay.Galaxy.StarSystems
{
    public class StarSystemProvider : ExtendedMonoProvider<StarSystem>
    {
        public event Action OnStarSystemClicked;

        protected override void Initialize()
        {
            base.Initialize();
            OnStarSystemClicked += AddClickEvent;
        }

        protected override void Deinitialize()
        {
            base.Deinitialize();
            OnStarSystemClicked -= AddClickEvent;
        }

        private void AddClickEvent()
        {
            var starSystemClickEvent = World.Default.GetEvent<StarSystemClickEvent>();
            starSystemClickEvent.NextFrame(new StarSystemClickEvent
            {
                Entity = Entity
            });
        }

        private void OnMouseDown()
        {
            OnStarSystemClicked?.Invoke();
        }
    }
}