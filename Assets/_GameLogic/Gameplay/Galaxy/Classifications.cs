using _GameLogic.Gameplay.Galaxy.StarSystems.Planets;
using _GameLogic.Gameplay.Galaxy.StarSystems.Stars;

namespace _GameLogic.GalaxyGenerator
{
	public class StellarDirectory
	{
		public class StarSystemData
		{
			public StarProvider[] Stars { get; private set; }
			public Planet[] Planets { get; private set; }
		}
		
		public enum GalaxyType
		{
			Elliptical,
			Ring
		}
	}
}