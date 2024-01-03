using _GameLogic.Extensions.Patterns;
using UnityEngine;

namespace _GameLogic.Gameplay.Galaxy
{
    public class GalaxyUIContainer : Singleton<GalaxyUIContainer>
    {
        [field: SerializeField] public RectTransform LabelsContainer { get; private set; }
    }
}