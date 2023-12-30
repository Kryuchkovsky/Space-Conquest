using _GameLogic.Common;
using _GameLogic.Core;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace _GameLogic.Gameplay.Camera.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class GameCameraSwitchingRequestProcessingSystem : AbstractSystem
    {
        private Request<GameCameraSwitchingRequest> _cameraSwitchingRequest;
        private FilterBuilder _filterBuilder;
        
        public override void OnAwake()
        {
            _cameraSwitchingRequest = World.GetRequest<GameCameraSwitchingRequest>();
            _filterBuilder = World.Filter.With<GameCameraLink>().With<Index>();
        }

        public override void OnUpdate(float deltaTime)
        {
            foreach (var request in _cameraSwitchingRequest.Consume())
            {
                foreach (var entity in _filterBuilder.Build())
                {
                    var isActive = entity.GetComponent<Index>().Value == request.CameraIndex;

                    if (isActive && !entity.Has<IsActiveFlag>())
                    {
                        entity.SetComponent(new IsActiveFlag());
                    }
                    else if (!isActive && entity.Has<IsActiveFlag>())
                    {
                        entity.RemoveComponent<IsActiveFlag>();
                    }
                }
            }
        }
    }
}