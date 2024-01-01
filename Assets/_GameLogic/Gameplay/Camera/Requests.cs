using Scellecs.Morpeh;
using UnityEngine;

namespace _GameLogic.Gameplay.Camera
{
    public struct GameCameraSwitchingRequest : IRequestData
    {
        public int CameraIndex;
    }
    
    public struct GameCameraBoundsSettingRequest : IRequestData
    {
        public Bounds Bounds;
    }
}