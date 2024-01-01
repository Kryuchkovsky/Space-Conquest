using _GameLogic.Common;
using _GameLogic.Core;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace _GameLogic.Gameplay.Camera.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class GameCameraBoundsSettingsRequestProcessingSystem : AbstractSystem
    {
        private FilterBuilder _filterBuilder;
        
        public override void OnAwake()
        {
            _filterBuilder = World.Filter.With<GameCameraLink>().With<IsActiveFlag>();
        }

        public override void OnUpdate(float deltaTime)
        {
            foreach (var request in World.GetRequest<GameCameraBoundsSettingRequest>().Consume())
            {
                foreach (var entity in _filterBuilder.Build())
                {
                    entity.SetComponent(new Boundaries
                    {
                        Value = request.Bounds
                    });
                }
            }
        }
    }
}