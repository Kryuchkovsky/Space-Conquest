using UnityEngine;

namespace _GameLogic.Gameplay.Galaxy.StarSystems
{
    public class OrbitDrawer : MonoBehaviour
    {
        [SerializeField] private LineRenderer _lineRenderer;

        private readonly int _stepsAmount = 128;

        public void Draw(Vector3 center)
        {
            _lineRenderer.positionCount = _stepsAmount;
            var radius = (center - transform.position).magnitude;

            for (int step = 0; step < _stepsAmount; step++)
            {
                var circumferenceProgress = (float)step / _stepsAmount;
                var radian = circumferenceProgress * 2 * Mathf.PI;
                var xPos = Mathf.Cos(radian) * radius;
                var yPos = Mathf.Sin(radian) * radius;
                var position = new Vector3(xPos, center.y, yPos);
                _lineRenderer.SetPosition(step, position);
            }
        }
    }
}