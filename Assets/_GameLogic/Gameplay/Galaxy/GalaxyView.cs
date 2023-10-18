using Unity.Entities;
using UnityEngine;

namespace _GameLogic.Gameplay.Galaxy
{
    public class GalaxyAuthoring : MonoBehaviour
    {
        private class Baker : Baker<GameResourcesAuthoring>
        {
            public override void Bake(GameResourcesAuthoring authoring)
            {
                var entity = GetEntity(authoring.transform, TransformUsageFlags.WorldSpace);
                AddComponent<IsGalaxy>(entity);
            }
        }
    }
}