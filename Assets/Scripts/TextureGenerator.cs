using UnityEngine;
using System.Collections.Generic;

public static class TextureGenerator {		

	// Height Map Colors
	private static Color DeepColor = new Color(15/255f, 30/255f, 80/255f, 1);
	private static Color ShallowColor = new Color(15/255f, 40/255f, 90/255f, 1);
	private static Color RiverColor = new Color(30/255f, 120/255f, 200/255f, 1);
	private static Color SandColor = new Color(198 / 255f, 190 / 255f, 31 / 255f, 1);
	private static Color GrassColor = new Color(50 / 255f, 220 / 255f, 20 / 255f, 1);
	private static Color ForestColor = new Color(16 / 255f, 160 / 255f, 0, 1);
	private static Color RockColor = new Color(0.5f, 0.5f, 0.5f, 1);            
	private static Color SnowColor = new Color(1, 1, 1, 1);

	private static Color IceWater = new Color (210/255f, 255/255f, 252/255f, 1);
	private static Color ColdWater = new Color (119/255f, 156/255f, 213/255f, 1);
	private static Color RiverWater = new Color (65/255f, 110/255f, 179/255f, 1);

	// Height Map Colors
	private static Color Coldest = new Color(0, 1, 1, 1);
	private static Color Colder = new Color(170/255f, 1, 1, 1);
	private static Color Cold = new Color(0, 229/255f, 133/255f, 1);
	private static Color Warm = new Color(1, 1, 100/255f, 1);
	private static Color Warmer = new Color(1, 100/255f, 0, 1);
	private static Color Warmest = new Color(241/255f, 12/255f, 0, 1);

	//Moisture map
	private static Color Dryest = new Color(255/255f, 139/255f, 17/255f, 1);
	private static Color Dryer = new Color(245/255f, 245/255f, 23/255f, 1);
	private static Color Dry = new Color(80/255f, 255/255f, 0/255f, 1);
	private static Color Wet = new Color(85/255f, 255/255f, 255/255f, 1);
	private static Color Wetter = new Color(20/255f, 70/255f, 255/255f, 1);
	private static Color Wettest = new Color(0/255f, 0/255f, 100/255f, 1);

	public static Texture2D GetHeightMapTexture(int width, int height, Tile[,] tiles)
	{
		var texture = new Texture2D(width, height);
		var pixels = new Color[width * height];

		for (var x = 0; x < width; x++)
		{
			for (var y = 0; y < height; y++)
			{
				switch (tiles[x,y].HeightType)
				{
				case HeightType.DeepWater:
					pixels[x + y * width] = DeepColor;
					break;
				case HeightType.ShallowWater:
					pixels[x + y * width] = ShallowColor;
					break;
				case HeightType.Sand:
					pixels[x + y * width] = SandColor;
					break;
				case HeightType.Grass:
					pixels[x + y * width] = GrassColor;
					break;
				case HeightType.Forest:
					pixels[x + y * width] = ForestColor;
					break;
				case HeightType.Rock:
					pixels[x + y * width] = RockColor;
					break;
				case HeightType.Snow:
					pixels[x + y * width] = SnowColor;
					break;
				case HeightType.River:
					pixels[x + y * width] = RiverColor;
					break;
				}

				//darken the color if a edge tile
				if ((int)tiles[x,y].HeightType > 2 && tiles[x,y].Bitmask != 15)
					pixels[x + y * width] = Color.Lerp(pixels[x + y * width], Color.black, 0.4f);

			}
		}
		
		texture.SetPixels(pixels);
		texture.wrapMode = TextureWrapMode.Clamp;
		texture.Apply();
		return texture;
	}

	public static Texture2D GetHeatMapTexture(int width, int height, Tile[,] tiles)
	{
		var texture = new Texture2D(width, height);
		var pixels = new Color[width * height];
		
		for (var x = 0; x < width; x++)
		{
			for (var y = 0; y < height; y++)
			{
				switch (tiles[x,y].HeatType)
				{
				case HeatType.Coldest:
					pixels[x + y * width] = Coldest;
					break;
				case HeatType.Colder:
					pixels[x + y * width] = Colder;
					break;
				case HeatType.Cold:
					pixels[x + y * width] = Cold;
					break;
				case HeatType.Warm:
					pixels[x + y * width] = Warm;
					break;
				case HeatType.Warmer:
					pixels[x + y * width] = Warmer;
					break;
				case HeatType.Warmest:
					pixels[x + y * width] = Warmest;
					break;
				}
				
				//darken the color if a edge tile
				if ((int)tiles[x,y].HeightType > 2 && tiles[x,y].Bitmask != 15)
					pixels[x + y * width] = Color.Lerp(pixels[x + y * width], Color.black, 0.4f);
			}
		}
		
		texture.SetPixels(pixels);
		texture.wrapMode = TextureWrapMode.Clamp;
		texture.Apply();
		return texture;
	}

	public static Texture2D GetMoistureMapTexture(int width, int height, Tile[,] tiles)
	{
		var texture = new Texture2D(width, height);
		var pixels = new Color[width * height];
		
		for (var x = 0; x < width; x++)
		{
			for (var y = 0; y < height; y++)
			{
				Tile t = tiles[x,y];
				
				if (t.MoistureType == MoistureType.Dryest)           
					pixels[x + y * width] = Dryest;
				else if (t.MoistureType == MoistureType.Dryer)          
					pixels[x + y * width] = Dryer;
				else if (t.MoistureType == MoistureType.Dry)          
					pixels[x + y * width] = Dry;
				else if (t.MoistureType == MoistureType.Wet)          
					pixels[x + y * width] = Wet; 
				else if (t.MoistureType == MoistureType.Wetter)          
					pixels[x + y * width] = Wetter; 
				else      
					pixels[x + y * width] = Wettest; 

				//darken the color if a edge tile
//				if ((int)tiles[x,y].HeightType > 2 && tiles[x,y].Bitmask != 15)
//					pixels[x + y * width] = Color.Lerp(pixels[x + y * width], Color.black, 0.4f);
			}
		}
		
		texture.SetPixels(pixels);
		texture.wrapMode = TextureWrapMode.Clamp;
		texture.Apply();
		return texture;
	}

}
