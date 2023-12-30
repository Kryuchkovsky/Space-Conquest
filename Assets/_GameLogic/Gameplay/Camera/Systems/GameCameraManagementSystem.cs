using _GameLogic.Common;
using _GameLogic.Core;
using _GameLogic.Extensions.Configs;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace _GameLogic.Gameplay.Camera.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class GameCameraManagementSystem : AbstractSystem
    {
        private FilterBuilder _filterBuilder;
        private GameCameraSettings _settings;
        
        private readonly Vector2 _center = new(0.5f, 0.5f);
        private float _zoomT = 0.5f;

        public override void OnAwake()
        {
            _filterBuilder = World.Filter.With<GameCameraLink>().With<IsActiveFlag>();
            _settings = ConfigsManager.GetConfig<GameCameraSettings>();
        }

        public override void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filterBuilder.Build())
            {
                var camera = entity.GetComponent<GameCameraLink>().Value;
                var cameraPos = camera.transform.position;
                var direction = new Vector2();

                if (Input.mousePosition.x <= 1 || Input.mousePosition.x >= Screen.width - 1)
                {
                    direction.x = Input.mousePosition.x - _center.x;
                }

                if (Input.mousePosition.y <= 1 || Input.mousePosition.y >= Screen.height - 1)
                {
                    direction.y = Input.mousePosition.y - _center.y;
                }

                var zoomDelta = Input.mouseScrollDelta.y * deltaTime * _settings.ZoomSensitivity;
                _zoomT = Mathf.Clamp01(_zoomT - zoomDelta);

                var yPosition = Mathf.Lerp(_settings.MinRange, _settings.MaxRange, _zoomT);
                var sensitivity = Mathf.Lerp(_settings.CloseRangeSensitivity, _settings.LongRangeSensitivity, _zoomT);
                var offset = direction.normalized * deltaTime * sensitivity;
                camera.transform.position = new Vector3(cameraPos.x + offset.x, yPosition, cameraPos.z + offset.y);
            }
        }
    }
}