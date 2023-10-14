using Unity.Entities;

namespace _GameLogic.Common
{
    public struct ClickEvent : IComponentData
    {
    }

    public struct Performing : IComponentData, IEnableableComponent
    {
    }
}