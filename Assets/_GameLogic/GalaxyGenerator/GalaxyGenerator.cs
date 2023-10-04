using System.Drawing;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _GameLogic.GalaxyGenerator
{
    public class GalaxyGenerator
    {
        private const int FluctuationRate = 10;
        private const float DistanceBetweenSystems = 0.1f;

        private PointF[] _points;

        public PointF[] GetPoints() => _points;

        public GalaxyConfiguration Configuration { get; set; }

        public GalaxyGenerator(GalaxyConfiguration configuration = default, int seed = -1)
        {
            Configuration = configuration;

            if (seed != -1)
            {
                Random.InitState(seed);
            }
        }

        public PointF[] GenerateSimpleGalaxy()
        {
            var points = new PointF[Configuration.NumberOfStars];
            var t = -Mathf.Rad2Deg;
            var row = 1;

            for (int i = 0; i < points.Length; i++)
            {
                var distanceFromCenter = DistanceBetweenSystems * row;
                var circleLenght = distanceFromCenter * 2 * Mathf.PI;
                var delta = Mathf.Rad2Deg * (DistanceBetweenSystems / circleLenght);
                t = Mathf.Clamp(t + delta, -Mathf.Rad2Deg, Mathf.Rad2Deg);
                var fluctuationOfCirclePosition = Random.Range(1 - FluctuationRate, 1 + FluctuationRate);
                var fluctuationOfDistanceFromCenter = Random.Range(1 - FluctuationRate, 1 + FluctuationRate);
                var fluctuatedT = t * fluctuationOfCirclePosition;
                var distance = distanceFromCenter * fluctuationOfDistanceFromCenter;
                var xPos = Mathf.Cos(fluctuatedT) * distance * Configuration.Scale;
                var yPos = Mathf.Sin(fluctuatedT) * distance * Configuration.Scale;;
                points[i] = new PointF(xPos, yPos);

                if (t >= Mathf.Rad2Deg)
                {
                    t = -Mathf.Rad2Deg;
                    row++;
                }
            }

            _points = points;
            return _points;
        }
    }
}