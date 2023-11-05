using UnityEngine;
using Random = UnityEngine.Random;

namespace _GameLogic.Gameplay.Galaxy.StarSystems.Stars
{
    [CreateAssetMenu(menuName = "Create StarsCatalog", fileName = "StarsCatalog", order = 0)]
    public class StarsCatalog : ScriptableObject
    {
        [SerializeField] private StarCatalogData[] _starsData =
        {
            new(StarType.M, 0.76f),
            new(StarType.K, 0.12f),
            new(StarType.G, 0.076f),
            new(StarType.F, 0.03f),
            new(StarType.A, 0.0061f),
            new(StarType.B, 0.0012f),
            new(StarType.O, 0.0000003f),
            new(StarType.L, 0.0022332f),
            new(StarType.T, 0.0022332f),
            new(StarType.BlackHole, 0.0022332f),
            new(StarType.NeutronStar, 0.0022332f),
        };

        public StarCatalogData GetRandomStarData()
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