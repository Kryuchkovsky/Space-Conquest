using _GameLogic.Gameplay.Galaxy.StarSystems.Planets;
using _GameLogic.Gameplay.Galaxy.StarSystems.Stars;
using Scellecs.Morpeh;

namespace _GameLogic.Gameplay.Galaxy
{
	public class StellarDirectory
	{
		public class StarSystemData
		{
			public StarView[] Stars { get; private set; }
			public Entity[] Planets { get; private set; }
		}
		
		public enum GalaxyType
		{
			Elliptical,
			Ring
		}
	}
}