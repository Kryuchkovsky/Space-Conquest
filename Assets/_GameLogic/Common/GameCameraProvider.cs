using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using UnityEngine;

namespace _GameLogic.Common
{
    public class GameCameraProvider : MonoProvider<GameCamera>
    {
        [SerializeField] private Camera _camera;
        [SerializeField, Min(0)] private int _index;
        
        protected override void Initialize()
        {
            base.Initialize();
            Entity.SetComponent(new GameCamera
            {
                Value = _camera
            });
            Entity.SetComponent(new Index
            {
                Value = _index
            });
        }
    }
}