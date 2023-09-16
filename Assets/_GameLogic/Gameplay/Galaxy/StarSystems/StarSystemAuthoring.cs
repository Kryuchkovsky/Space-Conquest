using Unity.Entities;
using UnityEngine;

namespace _GameLogic.Gameplay.Galaxy.StarSystems
{
    public class StarSystemAuthoring : MonoBehaviour
    {
        private class Baker : Baker<StarSystemAuthoring>
        {
            public override void Bake(StarSystemAuthoring authoring)
            {
                var entity = GetEntity(authoring.transform, TransformUsageFlags.WorldSpace);
                AddComponent<StarSystem>(entity);
            }
        }
    }
}
