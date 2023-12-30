using TMPro;
using UnityEngine;

namespace _GameLogic.Gameplay.Galaxy
{
    public class StellarObjectLabel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;

        public void SetText(string text) => _text.SetText(text);
    }
}