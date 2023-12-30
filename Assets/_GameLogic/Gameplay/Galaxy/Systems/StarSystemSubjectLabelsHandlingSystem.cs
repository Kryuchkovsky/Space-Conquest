using _GameLogic.Common;
using _GameLogic.Core;
using _GameLogic.Extensions.Configs;
using _GameLogic.Gameplay.Galaxy.StarSystems;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace _GameLogic.Gameplay.Galaxy.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class StarSystemSubjectLabelsHandlingSystem : AbstractSystem
    {
        private FilterBuilder _starSystemSubjectFilterBuilder;
        private FilterBuilder _gameCameraFilter;
        private GameResourcesCatalog _gameResourcesCatalog;
        private readonly Vector3 _offset = new(0, -50, 0);

        public override void OnAwake()
        {
            _starSystemSubjectFilterBuilder = World.Filter
                .With<StarSystemObjectViewLink>()
                .With<StellarObjectData>();
            _gameCameraFilter = World.Filter.With<GameCameraLink>().With<Index>();
            _gameResourcesCatalog = ConfigsManager.GetConfig<GameResourcesCatalog>();
        }

        public override void OnUpdate(float deltaTime)
        {
            foreach (var cameraEntity in _gameCameraFilter.Build())
            {
                if (cameraEntity.GetComponent<Index>().Value == 1)
                {
                    var camera = cameraEntity.GetComponent<GameCameraLink>().Value;
                    
                    foreach (var entity in _starSystemSubjectFilterBuilder.Build())
                    {
                        if (entity.Has<StellarObjectLabelLink>())
                        {
                            var view = entity.GetComponent<StarSystemObjectViewLink>().Value;
                            var label = entity.GetComponent<StellarObjectLabelLink>().Value;
                            var screenPosition = camera.WorldToScreenPoint(view.transform.position) + _offset;
                            label.transform.position = screenPosition;
                        }
                        else
                        {
                            var parent = StarSystemLocalUIContainer.Instance.RectTransform;
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