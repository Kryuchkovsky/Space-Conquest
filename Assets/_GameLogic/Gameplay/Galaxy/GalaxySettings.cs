using UnityEngine;

namespace _GameLogic.Gameplay.Galaxy
{
    [CreateAssetMenu(menuName = "Create GalaxySettings", fileName = "GalaxySettings")]
    public class GalaxySettings: ScriptableObject
    {
        [SerializeField, Range(0, 1)] private float _fluctuationRate = 0.25f;
        
        [field: SerializeField, Range(20, 50)] public float DistanceBetweenSystems { get; private set; } = 30f;
        [field: SerializeField, Range(20, 50)] public float DistanceBetweenPlanets { get; private set; } = 20f;
        [field: SerializeField, Range(1, 5)] public float GalaxyBoundsMultiplier { get; private set; } = 3f;
        [field: SerializeField, Range(1, 5)] public float SystemBoundsMultiplier { get; private set; } = 3f;
        [field: SerializeField, Range(1, 5)] public float GalacticCoreRadiusFactor { get; private set; } = 3f;

        public float GetFluctuationMultiplier() => 1 + Random.Range(-_fluctuationRate, _fluctuationRate);
    }
}