using Scellecs.Morpeh;
using UnityEngine;
using UnityEngine.UI;

namespace _GameLogic.Gameplay.Galaxy.StarSystems
{
    public class StarSystemClosingButton : MonoBehaviour
    {
        [field: SerializeField] public Button Button { get; private set; }

        private void OnEnable()
        {
            Button.onClick.AddListener(OnClicked);
        }

        private void OnDisable()
        {
            Button.onClick.RemoveListener(OnClicked);
        }

        private void OnClicked()
        {
            var clickEvent = World.Default.GetEvent<StarSystemClosingButtonClickEvent>();
            clickEvent.NextFrame(new StarSystemClosingButtonClickEvent());
        }
    }
}