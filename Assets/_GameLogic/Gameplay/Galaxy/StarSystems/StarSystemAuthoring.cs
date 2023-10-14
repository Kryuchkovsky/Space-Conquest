using System;
using _GameLogic.Common;
using Unity.Entities;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _GameLogic.Gameplay.Galaxy.StarSystems
{
    public class StarSystemAuthoring : MonoBehaviour, IPointerClickHandler
    {
        public event Action OnStarSystemClicked;
        
        private EntityManager _entityManager;
        private Entity _entity;
        
        private void Awake()
        {
            _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

            OnStarSystemClicked += AddClickEvent;
        }

        private void OnDestroy()
        {
            OnStarSystemClicked -= AddClickEvent;
        }
        
        private class Baker : Baker<StarSystemAuthoring>
        {
            public override void Bake(StarSystemAuthoring authoring)
            {
                authoring._entity = GetEntity(authoring.transform, TransformUsageFlags.WorldSpace);
                AddComponent<StarSystem>(authoring._entity);
            }
        }

        private void AddClickEvent()
        {
            _entityManager.AddComponent<ClickEvent>(_entity);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log(1);
            OnStarSystemClicked?.Invoke();
        }

        private void OnMouseDown()
        {
            Debug.Log(2);
        }
    }
}