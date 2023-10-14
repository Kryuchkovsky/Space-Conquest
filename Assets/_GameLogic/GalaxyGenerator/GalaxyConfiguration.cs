using System;
using System.Drawing;

namespace _GameLogic.GalaxyGenerator
{
	public class GalaxyConfiguration
	{
		public string Name { get; private set; }
		public int NumberOfStars { get; private set; }
		public int NumberOfArms { get; private set; }
		public float MaxArmOffset { get; private set; }
		public float RotationFactor { get; private set; }
		public float RandomOffsetOnAxis { get; private set; }
		public float ArmSeparationDistance { get; private set; }
		public float Scale { get; private set; }
		
		public GalaxyConfiguration(
			string name, 
			int numberOfStarsStars = 30000, 
			int numberOfArms = 4, 
			float maxArmOffset = 0.9f, 
			float rotationFactor = 5f, 
			float randomOffsetOnAxis = 0.04f,
			float scale = 100)
		{
			Name = name;
			NumberOfStars = numberOfStarsStars;
			NumberOfArms = numberOfArms;
			MaxArmOffset = maxArmOffset;
			RotationFactor = rotationFactor;
			RandomOffsetOnAxis = randomOffsetOnAxis;
			ArmSeparationDistance = 2 * (float)Math.PI / NumberOfArms;
			Scale = scale;
		}
	}

	public class StarSystemData
	{
		public PointF Position { get; private set; }
		
		public StarSystemData(PointF position)
		{
			Position = position;
		}
	}
}