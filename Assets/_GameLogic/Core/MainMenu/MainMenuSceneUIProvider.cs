using _GameLogic.Core.MainMenu;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace _GameLogic.Core.GameStates
{
    [Singleton]
    public class MainMenuSceneUIProvider : MonoProvider<MainMenuSceneUI>
    {
        [field: SerializeField] public Button NewGameButton { get; private set; }

        protected override void Initialize()
        {
            base.Initialize();
            
            NewGameButton.onClick.AddListener(() =>
            {
                var clickEvent = World.Default.GetEvent<NewGameButtonClickEvent>();
                clickEvent.NextFrame(new NewGameButtonClickEvent());
            });
        }

        protected override void Deinitialize()
        {
            base.Deinitialize();
        }
    }
}