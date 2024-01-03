using _GameLogic.Extensions.Patterns;
using UnityEngine;

namespace _GameLogic.Gameplay.Galaxy.StarSystems
{
    public class StarSystemLocalUIContainer : Singleton<StarSystemLocalUIContainer>
    {
        [field: SerializeField] public RectTransform RectTransform { get; private set; }
        [field: SerializeField] public RectTransform LabelsContainer { get; private set; }
    }
}