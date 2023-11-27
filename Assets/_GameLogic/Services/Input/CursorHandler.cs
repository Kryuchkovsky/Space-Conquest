using UnityEngine;

namespace _GameLogic.Services.Input
{
    public class CursorHandler : MonoBehaviour
    {
        public static Vector2 Direction { get; private set; }

        private readonly Vector2 _center = new(0.5f, 0.5f);
        
        // todo: divide this class and place part of this logic into system
        private void Update()
        {
            var pos = UnityEngine.Input.mousePosition;
            var direction = new Vector2();

            if (pos.x <= 1 || pos.x >= Screen.width - 1)
            {
                direction.x = pos.x - _center.x;
            }
            
            if (pos.y <= 1 || pos.y >= Screen.height - 1)
            {
                direction.y = pos.y - _center.y;
            }

            Direction = direction.normalized * 50 * Time.deltaTime;
            
            
            Camera.main.transform.position += new Vector3(Direction.x, 0, Direction.y);
        }
    }
}
