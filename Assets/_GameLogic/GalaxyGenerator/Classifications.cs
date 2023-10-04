using System.Collections.Generic;
using UnityEngine;

namespace _GameLogic.GalaxyGenerator
{
	public enum GalaxyType
	{
		Elliptical,
		Ring
	}
	
	public enum StellarType
	{
		MainSequenceStar,
		NeutronStar,
		BlackHole
	}

	public enum SpectralType
	{
		undefined,
		O, 
		B, 
		A, 
		F, 
		G, 
		K, 
		M,
		L,
		T
	}

	public enum QuantityType
	{
		Binary,
		Double
	}

	public static class StellarDirectory
	{
		private static StellarInfo[] _stellarInfos;

		static StellarDirectory()
		{
			_stellarInfos = new StellarInfo[]
			{
				new(SpectralType.M, 0.1f, 0.7f, 0.76f),
				new(SpectralType.K, 0.7f, 0.96f, 0.12f),
				new(SpectralType.G, 0.96f, 1.15f,0.076f),
				new(SpectralType.F, 1.15f, 1.4f,0.03f),
				new(SpectralType.A, 1.4f, 1.8f,0.0061f),
				new(SpectralType.B, 1.8f, 6.6f,0.0012f),
				new(SpectralType.O, 6.6f, 0.96f,0.0000003f),
				new(SpectralType.L, 0.05f, 0.1f,0.0022332f),
				new(SpectralType.T, 0.01f, 0.5f,0.0022332f),
				new(SpectralType.undefined, 0.7f, 0.96f,0.0022332f)
			};
		}

		public static StarInfo GetStarInfo()
		{
			var t = Random.Range(0, 1f);
			var min = 0f;
			var max = 0f;

			for (int i = 0; i < _stellarInfos.Length; i++)
			{
				max += _stellarInfos[i].SpreadingRate;

				if (t >= min && t <= max)
				{
					var solarRadius = Random.Range(_stellarInfos[i].MinSolarRadius, _stellarInfos[i].MaxSolarRadius);
					return new StarInfo(_stellarInfos[i].SpectralType, solarRadius);
				}
				
				min += _stellarInfos[i].SpreadingRate;
			}

			return default;
		}
		
		private struct StellarInfo
		{
			public SpectralType SpectralType { get; private set; }
			public float MinSolarRadius { get; private set; }
			public float MaxSolarRadius { get; private set; }
			public float SpreadingRate { get; private set; }


			public StellarInfo(SpectralType spectralType, float minSolarRadius, float maxSolarRadius, float spreadingRate)
			{
				SpectralType = spectralType;
				MinSolarRadius = minSolarRadius;
				MaxSolarRadius = maxSolarRadius;
				SpreadingRate = spreadingRate;
			}
		}
	}

	public struct StarInfo
	{
		public SpectralType SpectralType { get; private set; }
		public float SolarRadius { get; private set; }

		public StarInfo(SpectralType spectralType, float solarRadius)
		{
			SpectralType = spectralType;
			SolarRadius = solarRadius;
		}
	}
}