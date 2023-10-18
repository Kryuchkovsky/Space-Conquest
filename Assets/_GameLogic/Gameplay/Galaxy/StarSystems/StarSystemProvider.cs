using System;
using _GameLogic.Common;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _GameLogic.Gameplay.Galaxy.StarSystems
{
    public class StarSystemAuthoring : MonoProvider<StarSystem>, IPointerClickHandler
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
            Entity.AddComponent<ClickEvent>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log(1);
            OnStarSystemClicked?.Invoke();
        }
    }
}