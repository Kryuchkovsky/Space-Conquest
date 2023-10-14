using Unity.Entities;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace _GameLogic.MainMenu
{
    [Singleton]
    public class MainMenuSceneUIContainer : MonoBehaviour, ISingleton
    {
        private EntityManager _entityManager;
        
        [field: SerializeField] public Button NewGameButton { get; private set; }

        private void Awake()
        {
            _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            
            NewGameButton.onClick.AddListener(() =>
            {
                var entity = _entityManager.CreateEntity();
                _entityManager.AddComponent<NewGameButtonClickEvent>(entity);
            });
        }
    }
}