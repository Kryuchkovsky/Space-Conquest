using UnityEngine;
using Random = UnityEngine.Random;

namespace _GameLogic.Gameplay.Galaxy.StarSystems.Stars
{
    [CreateAssetMenu(menuName = "Create StarsCatalog", fileName = "StarsCatalog", order = 0)]
    public class StarsCatalog : ScriptableObject
    {
        [SerializeField] private StarData[] _starsData =
        {
            new(SpectralType.M, 0.76f),
            new(SpectralType.K, 0.12f),
            new(SpectralType.G, 0.076f),
            new(SpectralType.F, 0.03f),
            new(SpectralType.A, 0.0061f),
            new(SpectralType.B, 0.0012f),
            new(SpectralType.O, 0.0000003f),
            new(SpectralType.L, 0.0022332f),
            new(SpectralType.T, 0.0022332f),
            new(SpectralType.undefined, 0.0022332f)
        };

        public StarData GetStarData()
        {
            var t = Random.Range(0, 1f);
            var min = 0f;
            var max = 0f;

            for (int i = 0; i < _starsData.Length; i++)
            {
                max += _starsData[i].SpreadingRate;

                if (t >= min && t <= max)
                {
                    return _starsData[i];
                }

                min += _starsData[i].SpreadingRate;
            }

            return default;
        }
    }
}