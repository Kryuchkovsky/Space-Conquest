using System;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _GameLogic.Gameplay.Galaxy.StarSystems
{
    public class StarSystemProvider : MonoProvider<StarSystem>
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