using _GameLogic.Common;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace _GameLogic.Core.MainMenu
{
    [Singleton]
    public class MainMenuSceneUIProvider : ExtendedMonoProvider<MainMenuSceneUI>
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