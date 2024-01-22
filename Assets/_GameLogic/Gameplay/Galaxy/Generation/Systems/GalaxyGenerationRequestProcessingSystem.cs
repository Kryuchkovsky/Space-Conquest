using _GameLogic.Common;
using _GameLogic.Core;
using _GameLogic.Extensions;
using _GameLogic.Extensions.Configs;
using _GameLogic.Gameplay.Camera;
using _GameLogic.Gameplay.Galaxy.StarSystems;
using _GameLogic.Gameplay.Galaxy.StarSystems.Planets;
using _GameLogic.Gameplay.Galaxy.StarSystems.Stars;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _GameLogic.Gameplay.Galaxy.Generation.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class GalaxyGenerationRequestProcessingSystem : AbstractSystem
    {
        private GalaxyConfiguration _configuration;
        private GameResourcesCatalog _gameResourcesCatalog;
        private GalaxySettings _galaxySettings;
        private StarsCatalog _starsCatalog;
        private PlanetsCatalog _planetsCatalog;

        public override void OnAwake()
        {
            _gameResourcesCatalog = ConfigsManager.GetConfig<GameResourcesCatalog>();
            _galaxySettings = ConfigsManager.GetConfig<GalaxySettings>();
            _starsCatalog = ConfigsManager.GetConfig<StarsCatalog>();
            _planetsCatalog = ConfigsManager.GetConfig<PlanetsCatalog>();
        }

        public override void OnUpdate(float deltaTime)
        {
            foreach (var request in World.GetRequest<GalaxyGenerationRequest>().Consume())
            {
                World.GetRequest<GameCameraSwitchingRequest>().Publish(new GameCameraSwitchingRequest
                {
                    CameraIndex = 0
                }, true);
                
                _configuration = new GalaxyConfiguration(
                    "Galaxy", request.StarSystemsNumber, 
                    4, 1.05f, 5, 0.05f);
                GenerateGalaxy();
            }
        }
        
        private void GenerateGalaxy()
        {
            var galaxy = Object.Instantiate(_gameResourcesCatalog.GalaxyPrefab);
            var distanceFromCenter = _galaxySettings.DistanceBetweenSystems * _galaxySettings.GalacticCoreRadiusFactor;
            var t = 0f;

            for (int systemIndex = 0; systemIndex < _configuration.NumberOfSystems; systemIndex++)
            {
                var circleLenght = distanceFromCenter * 2f * Mathf.PI;
                var delta = _galaxySettings.DistanceBetweenSystems * _galaxySettings.GetFluctuationMultiplier() / circleLenght;
                t += delta;

                if (t >= 1)
                {
                    t -= 1;
                    distanceFromCenter += _galaxySettings.DistanceBetweenSystems;
                }

                var fluctuatedDistance = distanceFromCenter + _galaxySettings.DistanceBetweenSystems * (1 - _galaxySettings.GetFluctuationMultiplier());
                var rad = Mathf.Lerp(0, 360, t) / Mathf.Rad2Deg;
                var xPos = Mathf.Cos(rad);
                var yPos = Mathf.Sin(rad);
                var pos = new Vector3(xPos, 0, yPos) * fluctuatedDistance;

                var starSystemProvider = Object.Instantiate(
                    _gameResourcesCatalog.StarSystemPrefab, pos, Quaternion.identity, galaxy.transform);
                ref var starSystemComponent = ref starSystemProvider.Entity.GetComponent<StarSystem>();
                starSystemComponent.Provider = starSystemProvider;

                var systemName = ExtensionMethods.ConvertArabicNumberToRomanNumber(systemIndex + 1);
                starSystemProvider.Entity.SetComponent(new StellarObjectData
                {
                    Name = systemName
                });
                
                var starEntities = new Entity[1];
                
                for (int starIndex = 0; starIndex < starEntities.Length; starIndex++)
                {
                    var starEntity = World.CreateEntity();
                    starEntities[starIndex] = starEntity;
                    var starData = _starsCatalog.GetRandomStarData();
                    var starView = Object.Instantiate(starData.Prefab, starSystemProvider.transform);
                    var starComponent = new StarData
                    {
                        Value = starData
                    };
                    starEntity.SetComponent(starComponent);
                    
                    var name = $"{systemName} {ExtensionMethods.ConvertIntToLetter(starIndex + 1).ToLower()}";
                    starEntity.SetComponent(new StellarObjectData
                    {
                        Name = name
                    });
                }
                
                starSystemComponent.StarEntities = starEntities; 
                
                var numberOfPlanets = Random.Range(0, 11);
                var planetEntities = new Entity[numberOfPlanets];

                for (int planetIndex = 0; planetIndex < planetEntities.Length; planetIndex++)
                {
                    var planetEntity = World.CreateEntity();
                    planetEntities[planetIndex] = planetEntity;
                    var planetData = _planetsCatalog.GetRandomData();
                    var planetDataComponent = new PlanetData
                    {
                        Value = planetData
                    };
                    planetEntity.SetComponent(planetDataComponent);

                    if (planetData.IsHabitable)
                    {
                        planetEntity.AddComponent<HabitableFlag>();
                    }
                    
                    var pRad = Random.Range(0f, 360f) / Mathf.Rad2Deg;
                    var position = new Vector3(Mathf.Cos(pRad), 0, Mathf.Sin(pRad)) * (planetIndex + 1);
                    var planetPosition = new PositionInStarSystemMap
                    {
                        Value = position * _galaxySettings.DistanceBetweenPlanets * _galaxySettings.GetFluctuationMultiplier()
                    };
                    planetEntity.SetComponent(planetPosition);
                    
                    var name = $"{systemName} {ExtensionMethods.ConvertArabicNumberToRomanNumber(planetIndex + 1)}";
                    planetEntity.SetComponent(new StellarObjectData
                    {
                        Name = name
                    });
                }

                var radius = (planetEntities.Length + 1) * _galaxySettings.DistanceBetweenPlanets;
                starSystemComponent.PlanetEntities = planetEntities;
                starSystemProvider.Entity.SetComponent(new Boundaries
                {
                    Value = new Bounds(Vector3.zero, new Vector3(radius, 1 ,radius) * _galaxySettings.GalaxyBoundsMultiplier)
                });
            }

            var bounds = new Bounds(
                Vector3.zero, 
                new Vector3(distanceFromCenter, 1, distanceFromCenter) * _galaxySettings.GalaxyBoundsMultiplier);
            galaxy.Entity.SetComponent(new Boundaries
            {
                Value = bounds
            });
            World.GetRequest<GameCameraBoundsSettingRequest>().Publish(new GameCameraBoundsSettingRequest
            {
                Bounds = bounds
            }, true);
        }
    }
}