using _GameLogic.Common;
using _GameLogic.Core;
using _GameLogic.Extensions.Configs;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace _GameLogic.Gameplay.Galaxy.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class GalaxyObjectLabelsHandlingSystem : AbstractSystem
    {
        private FilterBuilder _galaxyObjectFilterBuilder;
        private FilterBuilder _gameCameraFilter;
        private GameResourcesCatalog _gameResourcesCatalog;
        private readonly Vector3 _offset = new(0, -50, 0);

        public override void OnAwake()
        {
            _galaxyObjectFilterBuilder = World.Filter
                .With<GalaxyObjectFlag>()
                .With<StellarObjectData>()
                .With<TransformLink>();
            _gameCameraFilter = World.Filter.With<GameCameraLink>().With<Index>();
            _gameResourcesCatalog = ConfigsManager.GetConfig<GameResourcesCatalog>();
        }

        public override void OnUpdate(float deltaTime)
        {
            foreach (var cameraEntity in _gameCameraFilter.Build())
            {
                if (cameraEntity.GetComponent<Index>().Value == 0)
                {
                    var camera = cameraEntity.GetComponent<GameCameraLink>().Value;
                    
                    foreach (var entity in _galaxyObjectFilterBuilder.Build())
                    {
                        if (entity.Has<StellarObjectLabelLink>())
                        {
                            var view = entity.GetComponent<TransformLink>().Value;
                            var label = entity.GetComponent<StellarObjectLabelLink>().Value;
                            var screenPosition = camera.WorldToScreenPoint(view.position) + _offset;
                            label.transform.position = screenPosition;
                        }
                        else
                        {
                            var parent = GalaxyUIContainer.Instance.LabelsContainer;
                            var label = Object.Instantiate(_gameResourcesCatalog.StellarObjectLabelPrefab, parent);
                            var name = entity.GetComponent<StellarObjectData>().Name;
                            label.SetText(name);
                            entity.SetComponent(new StellarObjectLabelLink
                            {
                                Value = label
                            });
                        }
                    }
                }
            }
        }
    }
}