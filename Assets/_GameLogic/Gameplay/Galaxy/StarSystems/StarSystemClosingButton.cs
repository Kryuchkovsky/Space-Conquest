using _GameLogic.Core.MainMenu;
using Scellecs.Morpeh;
using UnityEngine;
using UnityEngine.UI;

namespace _GameLogic.Gameplay.Galaxy.StarSystems
{
    public class StarSystemClosingButton : MonoBehaviour
    {
        [field: SerializeField] public Button Button { get; private set; }

        private void Awake()
        {
            Button.onClick.AddListener(() =>
            {
                var clickEvent = World.Default.GetEvent<StarSystemClosingButtonClickEvent>();
                clickEvent.NextFrame(new StarSystemClosingButtonClickEvent());
            });;
        }
    }
}