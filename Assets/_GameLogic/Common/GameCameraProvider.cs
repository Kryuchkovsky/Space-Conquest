using Scellecs.Morpeh;
using UnityEngine;

namespace _GameLogic.Common
{
    public class GameCameraProvider : ExtendedMonoProvider<GameCameraLink>
    {
        [SerializeField] private Camera _camera;
        [SerializeField, Min(0)] private int _index;
        
        protected override void Initialize()
        {
            base.Initialize();
            Entity.SetComponent(new GameCameraLink
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