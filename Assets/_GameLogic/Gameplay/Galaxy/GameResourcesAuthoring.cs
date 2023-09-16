using _GameLogic.Gameplay.Galaxy.StarSystems;
using Unity.Entities;
using UnityEngine;

namespace _GameLogic.Gameplay.Galaxy
{
    public class GameResourcesAuthoring : MonoBehaviour
    {
        [field: SerializeField] public StarSystemAuthoring StarSystemAuthoring { get; private set; }
        
        private class Baker : Baker<GameResourcesAuthoring>
        {
            public override void Bake(GameResourcesAuthoring authoring)
            {
                var entity = GetEntity(authoring.transform, TransformUsageFlags.None);
                AddComponent(entity, new GameResourcesData
                {
                    StarSystemPrefab = GetEntity(authoring.StarSystemAuthoring, TransformUsageFlags.None)
                });
            }
        }
    }
}