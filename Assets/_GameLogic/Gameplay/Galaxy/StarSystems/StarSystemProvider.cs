using System;
using _GameLogic.Common;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _GameLogic.Gameplay.Galaxy.StarSystems
{
    public class StarSystemProvider : MonoProvider<StarSystem>, IPointerClickHandler
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
            starSystemClickEvent.NextFrame(new StarSystemClickEvent());
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log(1);
            OnStarSystemClicked?.Invoke();
        }
    }
}