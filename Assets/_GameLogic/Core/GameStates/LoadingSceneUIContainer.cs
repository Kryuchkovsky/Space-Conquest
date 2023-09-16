using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace _GameLogic.Core.GameStates
{
    [Singleton]
    public class LoadingSceneUIContainer : MonoBehaviour, ISingleton
    {
        [field: SerializeField] public Image BackgroundImage { get; private set; }
        [field: SerializeField] public Image BarFillingImage { get; private set; }
        [field: SerializeField] public TextMeshProUGUI LoadingProgressText { get; private set; }
    }
}