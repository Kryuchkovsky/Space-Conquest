using Scellecs.Morpeh.Providers;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace _GameLogic.Core.Loading
{
    [Singleton]
    public class LoadingSceneUIProvider : MonoProvider<LoadingSceneUI>
    {
        [field: SerializeField] public Image BackgroundImage { get; private set; }
        [field: SerializeField] public Image BarFillingImage { get; private set; }
        [field: SerializeField] public TextMeshProUGUI LoadingProgressText { get; private set; }
    }
}