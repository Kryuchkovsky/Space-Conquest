using Scellecs.Morpeh.Providers;
using Unity.IL2CPP.CompilerServices;

namespace _GameLogic.Gameplay.Galaxy
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [System.Serializable]
    public class GalaxyProvider : MonoProvider<Galaxy>
    {
    }
}