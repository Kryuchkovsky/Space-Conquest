using _GameLogic.Extensions.Configs;
using _GameLogic.Gameplay.Galaxy.StarSystems;
using _GameLogic.Gameplay.Galaxy.StarSystems.Planets;
using _GameLogic.Gameplay.Galaxy.StarSystems.Stars;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using Unity.IL2CPP.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _GameLogic.Gameplay.Galaxy.Generation.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [CreateAssetMenu(menuName = "ECS/Systems/Gameplay/Galaxy/Generation/" + nameof(GalaxyGenerationRequestProcessingSystem))]
    public class GalaxyGenerationRequestProcessingSystem : UpdateSystem
    {
        private const int FluctuationRate = 10;
        private const float DistanceBetweenSystems = 0.1f;
        
        private Request<GalaxyGenerationRequest> _galaxyGenerationRequest;
        private GalaxyConfiguration _configuration;
        private GameResourcesCatalog _gameResourcesCatalog;
        private StarsCatalog _starsCatalog;
        private PlanetsCatalog _planetsCatalog;

        public override void OnAwake()
        {
            _galaxyGenerationRequest = World.GetRequest<GalaxyGenerationRequest>();
            _gameResourcesCatalog = ConfigManager.GetConfig<GameResourcesCatalog>();
            _starsCatalog = ConfigManager.GetConfig<StarsCatalog>();
            _planetsCatalog = ConfigManager.GetConfig<PlanetsCatalog>();
        }

        public override void OnUpdate(float deltaTime)
        {
            foreach (var request in _galaxyGenerationRequest.Consume())
            {
                _configuration = new GalaxyConfiguration(
                    "Galaxy", request.StarSystemsNumber, 
                    4, 1.05f, 5, 0.05f);
                GenerateGalaxy();
            }
        }
        
        private void GenerateGalaxy()
        {
            var galaxy = Instantiate(_gameResourcesCatalog.GalaxyPrefab);
            var t = -Mathf.Rad2Deg;
            var row = 1;

            for (int i = 0; i < _configuration.NumberOfStars; i++)
            {
                var distanceFromCenter = DistanceBetweenSystems * row;
                var circleLenght = distanceFromCenter * 2 * Mathf.PI;
                var delta = Mathf.Rad2Deg * (DistanceBetweenSystems / circleLenght);
                
                t += delta;
                
                if (t >= Mathf.Rad2Deg)
                {
                    t -= 2 * Mathf.Rad2Deg;
                    row++;
                }
                
                var fluctuationOfCirclePosition = Random.Range(1 - FluctuationRate, 1 + FluctuationRate);
                var fluctuationOfDistanceFromCenter = Random.Range(1 - FluctuationRate, 1 + FluctuationRate);
                var fluctuatedT = t * fluctuationOfCirclePosition;
                var distance = distanceFromCenter * fluctuationOfDistanceFromCenter;
                var xPos = Mathf.Cos(fluctuatedT) * distance * _configuration.Scale;
                var yPos = Mathf.Sin(fluctuatedT) * distance * _configuration.Scale;;
                var pos = new Vector3(xPos, 0, yPos);

                var starSystemProvider = Instantiate(
                    _gameResourcesCatalog.StarSystemPrefab, pos, Quaternion.identity, galaxy.transform);
                ref var starSystemComponent = ref starSystemProvider.Entity.GetComponent<StarSystem>();
                starSystemComponent.Provider = starSystemProvider;
                
                var starEntities = new Entity[1];
                
                for (int starIndex = 0; starIndex < starEntities.Length; starIndex++)
                {
                    var starData = _starsCatalog.GetRandomStarData();
                    var starProvider = Instantiate(starData.Prefab, starSystemProvider.transform);
                    var starComponent = new Star
                    {
                        Provider = starProvider
                    };
                    starProvider.Entity.SetComponent(starComponent);
                    starEntities[starIndex] = starProvider.Entity;
                }
                
                starSystemComponent.StarEntities = starEntities; 
                
                var numberOfPlanets = Random.Range(0, 11);
                var planetEntities = new Entity[numberOfPlanets];

                for (int planetIndex = 0; planetIndex < planetEntities.Length; planetIndex++)
                {
                    var planetEntity = World.CreateEntity();
                    planetEntities[planetIndex] = planetEntity;
                    var planetData = _planetsCatalog.GetRandomData();
                    var planetComponent = new Planet
                    {
                        Provider = planetData.Prefab
                    };
                    planetEntity.SetComponent(planetComponent);

                    if (planetData.IsHabitable)
                    {
                        planetEntity.AddComponent<IsHabitable>();
                    }
                }

                starSystemComponent.PlanetEntities = planetEntities;
            }
        }
    }
}