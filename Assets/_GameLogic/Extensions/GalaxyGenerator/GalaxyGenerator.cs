using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using UnityEngine;
using Random = System.Random;

namespace _GameLogic.Extensions.GalaxyGenerator
{
    public class GalaxyGenerator
    {
        private const int FluctuationRate = 10;
        private const float DistanceBetweenSystems = 0.1f;

        private PointF[] _points;
        private readonly Random _random;

        public PointF[] GetPoints() => _points;

        public GalaxyConfiguration Configuration { get; set; }

        public GalaxyGenerator(GalaxyConfiguration configuration = default, int seed = -1)
        {
            Configuration = configuration;

            if (seed == -1)
            {
                _random = new Random();
            }
            else
            {
                _random = new Random(seed);
            }
        }

        public void GenerateGalacticBody()
        {
            var random = new Random();
            var starPositions = new PolarCoordinates[Configuration.NumberOfStars];

            using (var sw = File.CreateText("galaxy.csv"))
            {
                for (int i = 0; i < Configuration.NumberOfStars; i++)
                {
                    var distanceFromGalaxyCenter = (float)(random.NextDouble() * (0.01 - 0.99) + 0.99);
                    distanceFromGalaxyCenter = (float)Math.Pow(distanceFromGalaxyCenter, 2);
                    var angle = (float)random.NextDouble() * 2 * (float)Math.PI;
                    var armOffset = (float)random.NextDouble() * Configuration.MaxArmOffset;
                    armOffset -= Configuration.MaxArmOffset / 2;
                    armOffset *= 1 / distanceFromGalaxyCenter;

                    var squaredArmOffset = (float)Math.Pow(armOffset, 2);

                    if (armOffset < 0)
                    {
                        squaredArmOffset *= -1;
                    }

                    armOffset = squaredArmOffset;

                    var rotation = distanceFromGalaxyCenter * Configuration.RotationFactor;
                    angle = (int)(angle / Configuration.ArmSeparationDistance) * Configuration.ArmSeparationDistance + armOffset + rotation;
                    var positionX = (float)Math.Cos(angle) * distanceFromGalaxyCenter;
                    var positionY = (float)Math.Sin(angle) * distanceFromGalaxyCenter;
                    var randomOffsetX = (float)random.NextDouble() * Configuration.RandomOffsetOnAxis;
                    var randomOffsetY = (float)random.NextDouble() * Configuration.RandomOffsetOnAxis;
                    positionX += randomOffsetX;
                    positionY += randomOffsetY;
                    starPositions[i].X = positionX;
                    starPositions[i].Y = positionY;
                    sw.WriteLine(positionX + "," + positionY);
                }
            }

            var sideLenght = (int)(Configuration.NumberOfStars / Math.PI) * 3;
            var mapGalaxyTexture = new Texture2D(sideLenght, sideLenght);
            
            for (int i = 0; i < sideLenght; i++)
            {
                for (int j = 0; j < sideLenght; j++)
                {
                    mapGalaxyTexture.SetPixel(i, j, UnityEngine.Color.black);
                }
            }

            foreach (var cords in starPositions)
            {
                mapGalaxyTexture.SetPixel((int)cords.X, (int)cords.Y, UnityEngine.Color.blue);
            }

            mapGalaxyTexture.Apply();
            File.WriteAllBytes(Application.streamingAssetsPath, mapGalaxyTexture.GetRawTextureData());
        }
        
        public PointF[] GenerateGalaxy()
        {
            var result = new List<PointF>(Configuration.NumberOfStars);

            for (int i = 0; i < Configuration.NumberOfArms; i++)
            {
                result.AddRange(GenerateArm(
                    Configuration.NumberOfStars / Configuration.NumberOfArms, 
                    (float)i / Configuration.NumberOfArms, 
                    2, 
                    3,
                    Configuration.Scale));
            }

            _points = result.ToArray();
            return _points;
        }

        private PointF[] GenerateArm(int armNumber, float spin, double armSpread, double starsAtCenterRatio, float scale)
        {
            var numberOfStarsInArm = Configuration.NumberOfStars / Configuration.NumberOfArms;
            var rotation = armNumber / Configuration.NumberOfArms;
            var result = new PointF[numberOfStarsInArm];
            var random = new Random();

            for (int i = 0; i < numberOfStarsInArm; i++)
            {
                var part = (double)i / numberOfStarsInArm;
                part = Math.Pow(part, starsAtCenterRatio);
                var distanceFromCenter = (float)part;
                var position = (part * spin + rotation) * Math.PI * 2;
                var xFluctuation = (Pow3Constrained(random.NextDouble()) - Pow3Constrained(random.NextDouble())) * armSpread;
                var yFluctuation = (Pow3Constrained(random.NextDouble()) - Pow3Constrained(random.NextDouble())) * armSpread;
                var resultX = (float)Math.Cos(position) * distanceFromCenter / 2 + 0.5f + (float)xFluctuation;
                var resultY = (float)Math.Sin(position) * distanceFromCenter / 2 + 0.5f + (float)yFluctuation;
                result[i] = new PointF(resultX * scale, resultY * scale);
            }

            return result;
        }
        
        private static double Pow3Constrained(double x)
        {
            var value = Math.Pow(x - 0.5, 3) * 4 + 0.5d;
            return Math.Max(Math.Min(1, value), 0);
        }

        public PointF[] GenerateSimpleGalaxy()
        {
            var points = new PointF[Configuration.NumberOfStars];
            var rad = 360 / Math.PI;
            var t = -rad;
            var row = 1;

            for (int i = 0; i < points.Length; i++)
            {
                var distanceFromCenter = DistanceBetweenSystems * row;
                var circleLenght = distanceFromCenter * 2 * Math.PI;
                var delta = rad * (DistanceBetweenSystems / circleLenght);
                t = Math.Clamp(t + delta, -rad, rad);
                var fluctuationOfCirclePosition = _random.Next(100 - FluctuationRate, 100 + FluctuationRate) / 100d;
                var fluctuationOfDistanceFromCenter = _random.Next(100 - FluctuationRate, 100 + FluctuationRate) / 100f;
                var fluctuatedT = t * fluctuationOfCirclePosition;
                var distance = distanceFromCenter * fluctuationOfDistanceFromCenter;
                var xPos = (float)Math.Cos(fluctuatedT) * distance * Configuration.Scale;
                var yPos = (float)Math.Sin(fluctuatedT) * distance * Configuration.Scale;;
                points[i] = new PointF(xPos, yPos);

                if (t >= rad)
                {
                    t = -rad;
                    row++;
                }
            }

            _points = points;
            return _points;
        }
    }
}