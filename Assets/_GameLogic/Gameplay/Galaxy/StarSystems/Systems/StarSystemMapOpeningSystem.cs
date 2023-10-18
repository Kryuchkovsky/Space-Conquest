using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _GameLogic.Gameplay.Galaxy.StarSystems.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [CreateAssetMenu(menuName = "ECS/Systems/Gameplay/Galaxy/StarSystems/" + nameof(StarSystemMapOpeningSystem))]
    public class StarSystemMapOpeningSystem : UpdateSystem
    {
        private Event<StarSystemClickEvent> _clickEvent;

        public override void OnAwake()
        {
            _clickEvent = World.GetEvent<StarSystemClickEvent>();
        }

        public override void OnUpdate(float deltaTime)
        {
            foreach (var evt in _clickEvent.publishedChanges)           
            {
                if (!SceneManager.GetSceneByBuildIndex(3).isLoaded)
                {
                    var operation = SceneManager.LoadSceneAsync(3);
                    operation.completed += _ =>
                    {
                    };
                }
            }
        }
    }
}