using UnityEngine;

namespace _GameLogic.Extensions
{
    public static class ExtensionMethods
    {
        public static void SetLayerRecursively(this Transform parent, int layer)
        {
            parent.gameObject.layer = layer;
 
            for (int i = 0, count = parent.childCount; i < count; i++)
            {
                parent.GetChild(i).SetLayerRecursively(layer);
            }
        }
    }
}