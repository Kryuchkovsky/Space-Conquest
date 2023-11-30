using UnityEngine;

namespace _GameLogic.Gameplay.Camera
{
    [CreateAssetMenu(menuName = "Create GameCameraSettings", fileName = "GameCameraSettings")]
    public class GameCameraSettings : ScriptableObject
    {
        [field: SerializeField, Range(1, 5)] 
        public float ZoomSensitivity { get; private set; } = 3;

        [field: SerializeField, Range(0, 1000)] 
        public float CloseRangeSensitivity { get; private set; } = 75;

        [field: SerializeField, Range(0, 1000)]
        public float LongRangeSensitivity { get; private set; } = 1000;
        
        [field: SerializeField, Range(0, 1500)] 
        public float MinRange { get; private set; } = 50;

        [field: SerializeField, Range(0, 2500)]
        public float MaxRange { get; private set; } = 1000;
    }
}