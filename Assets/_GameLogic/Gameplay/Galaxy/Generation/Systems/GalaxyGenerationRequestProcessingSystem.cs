using _GameLogic.Core;
using _GameLogic.Extensions.Configs;
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
        private const float FluctuationRate = 0.25f;
        private const float DistanceBetweenSystems = 30f;
        private const float DistanceBetweenPlanets = 20f;
        
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
            var galaxy = Object.Instantiate(_gameResourcesCatalog.GalaxyPrefab);
            var distanceFromCenter = DistanceBetweenSystems * 3;
            var t = 0f;

            for (int i = 0; i < _configuration.NumberOfStars; i++)
            {
                var circleLenght = distanceFromCenter * 2f * Mathf.PI;
                var delta = DistanceBetweenSystems * (1 + Random.Range(-FluctuationRate, FluctuationRate)) / circleLenght;
                t += delta;

                if (t >= 1)
                {
                    t -= 1;
                    distanceFromCenter += DistanceBetweenSystems;
                }

                var fluctuatedDistance = distanceFromCenter + DistanceBetweenSystems * Random.Range(-FluctuationRate, FluctuationRate);
                var rad = Mathf.Lerp(0, 360, t) / Mathf.Rad2Deg;
                var xPos = Mathf.Cos(rad);
                var yPos = Mathf.Sin(rad);
                var pos = new Vector3(xPos, 0, yPos) * fluctuatedDistance * _configuration.Scale;

                var starSystemProvider = Object.Instantiate(
                    _gameResourcesCatalog.StarSystemPrefab, pos, Quaternion.identity, galaxy.transform);
                ref var starSystemComponent = ref starSystemProvider.Entity.GetComponent<StarSystem>();
                starSystemComponent.Provider = starSystemProvider;
                
                var starEntities = new Entity[1];
                
                for (int starIndex = 0; starIndex < starEntities.Length; starIndex++)
                {
                    var starEntity = World.CreateEntity();
                    starEntities[starIndex] = starEntity;
                    var starData = _starsCatalog.GetRandomStarData();
                    var starProvider = Object.Instantiate(starData.Prefab, starSystemProvider.transform);
                    World.Default.RemoveEntity(starProvider.Entity);
                    var starComponent = new Star
                    {
                        Provider = starData.Prefab
                    };
                    starEntity.SetComponent(starComponent);
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
                    
                    var pRad = Random.Range(0f, 360f) / Mathf.Rad2Deg;
                    var planetPosition = new Position
                    {
                        Value = new Vector3(Mathf.Cos(pRad), 0, Mathf.Sin(pRad)) * (planetIndex + 1) *
                                DistanceBetweenPlanets * (1 + Random.Range(-FluctuationRate, FluctuationRate))
                    };
                    planetEntity.SetComponent(planetPosition);
                }

                starSystemComponent.PlanetEntities = planetEntities;
            }
        }
    }
}