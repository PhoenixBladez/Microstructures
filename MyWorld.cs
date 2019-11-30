using System.IO;
using System;
using System.Linq;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Generation;
using Terraria.ModLoader.IO;
using System.Reflection;
using Terraria.Utilities;
using System.Runtime.Serialization.Formatters.Binary;

namespace Microstructures
{
	public class MyWorld : ModWorld
	{
		
		#region MageIsland
		private void PlaceIsland(int i, int j, int[,] ShrineArray, int[,] WallsArray, int[,] LootArray) {
			
			for (int y = 0; y < WallsArray.GetLength(0); y++) { // This Loop Placzs Furnitures.(So that they have blocks to spawn on).
				for (int x = 0; x < WallsArray.GetLength(1); x++) {
					int k = i - 3 + x;
					int l = j - 6 + y;
					if (WorldGen.InWorld(k, l, 30)){
						Tile tile = Framing.GetTileSafely(k, l);
						switch (WallsArray[y, x]) {
							case 1:
								Framing.GetTileSafely(k, l).ClearTile();
								WorldGen.KillWall(k, l); // Stone Slab
								break;	
							case 2:
								Framing.GetTileSafely(k, l).ClearTile();
								WorldGen.PlaceWall(k, l, 73); // Stone Slab
								break;	
							case 3:
								Framing.GetTileSafely(k, l).ClearTile();
								WorldGen.PlaceWall(k, l, 144); // Stone Slab
								break;	
						}
					}
				}
			}
			for (int y = 0; y < ShrineArray.GetLength(0); y++) { // This loops clears the area (makes the proper hemicircle) and placed dirt in the bottom if there are no blocks (so that the chest and fireplace can be placed).
				for (int x = 0; x < ShrineArray.GetLength(1); x++) {
					int k = i - 3 + x;
					int l = j - 6 + y;
					if (WorldGen.InWorld(k, l, 30)){
						Tile tile = Framing.GetTileSafely(k, l);
						switch (ShrineArray[y, x]) {
							case 0:
								break; // no changes
							case 1:
								Framing.GetTileSafely(k, l).ClearTile();
								break;
							case 2:
								Framing.GetTileSafely(k, l).ClearTile();
								break;		
						}
					}
				}
			}
			
			for (int y = 0; y < ShrineArray.GetLength(0); y++) { // This Loop Placzs Furnitures.(So that they have blocks to spawn on).
				for (int x = 0; x < ShrineArray.GetLength(1); x++) {
					int k = i - 3 + x;
					int l = j - 6 + y;
					if (WorldGen.InWorld(k, l, 30)){
						Tile tile = Framing.GetTileSafely(k, l);
						switch (ShrineArray[y, x]) {
							case 1:
								WorldGen.PlaceTile(k, l, 0); // Stone Slab
								tile.active(true);
								break;	
							case 2:
								WorldGen.PlaceTile(k, l, 189); // Platforms
								tile.active(true);
								break;			
						}
					}
				}
			}
			for (int y = 0; y < LootArray.GetLength(0); y++) { // This Loop Placzs Furnitures.(So that they have blocks to spawn on).
				for (int x = 0; x < LootArray.GetLength(1); x++) {
					int k = i - 3 + x;
					int l = j - 6 + y;
					if (WorldGen.InWorld(k, l, 30)){
						Tile tile = Framing.GetTileSafely(k, l);
						switch (LootArray[y, x]) {
							case 5:
								WorldGen.PlaceTile(k, l, 28);  // Pot
								tile.active(true);
								break;
							case 4:
								WorldGen.PlaceObject(k, l -1, mod.TileType("LunarStaff"));
								break;
						}
					}
				}
			}
		}

		public void GenerateIsland()
		{
			int[,] IslandShape = new int[,]
			{
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,2,2,2,2,2,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,2,2,2,2,2,2,0,0,0},
				{0,0,0,0,2,2,2,2,2,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,2,2,2,2,2,2,0,0,0,0},
				{0,0,0,0,0,0,2,2,2,2,2,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,2,2,2,2,2,0,0,0,0,0,0,0},
				{0,0,0,0,0,2,2,2,2,2,2,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,2,2,2,2,2,2,2,0,0,0,0,0,0},
				{0,0,0,0,0,0,2,0,2,2,2,2,2,2,2,1,1,1,1,1,1,1,1,1,1,1,1,2,2,2,2,2,2,2,2,2,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,2,2,2,2,2,2,2,2,2,1,1,1,1,1,1,2,2,2,2,2,2,2,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,2,2,2,0,2,2,2,2,1,1,1,2,2,2,2,2,2,2,2,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,2,2,2,2,2,2,0,2,2,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,2,2,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},


			

			};
			int[,] IslandWalls = new int[,]
			{	
			
			
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,0,0,0,0,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,3,0,0,0,0,3,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,3,3,3,3,3,3,3,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,2,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,2,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,2,2,2,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,2,2,2,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,2,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,2,2,2,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,2,2,2,2,2,1,1,1,1,1,1,1,1,1,1,1,1,2,2,2,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,2,0,0,2,2,2,2,1,1,1,1,1,1,2,2,2,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,2,2,2,2,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
	
			};
			int[,] IslandLoot = new int[,]
			{
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,5,0,5,0,0,0,4,0,0,0,0,5,0,5,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,2,2,2,2,2,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,2,2,2,2,2,2,0,0,0},
				{0,0,0,0,2,2,2,2,2,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,2,2,2,2,2,2,0,0,0,0},
				{0,0,0,0,0,0,2,2,2,2,2,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,2,2,2,2,2,0,0,0,0,0,0,0},
				{0,0,0,0,0,2,2,2,2,2,2,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,2,2,2,2,2,2,2,0,0,0,0,0,0},
				{0,0,0,0,0,0,2,0,2,2,2,2,2,2,2,1,1,1,1,1,1,1,1,1,1,1,1,2,2,2,2,2,2,2,2,2,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,2,2,2,2,2,2,2,2,2,1,1,1,1,1,1,2,2,2,2,2,2,2,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,2,2,2,0,2,2,2,2,1,1,1,2,2,2,2,2,2,2,2,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,2,2,2,2,2,2,0,2,2,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,2,2,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
			};
			bool placed = false;
			while (!placed)
			{
				// Select a place in the first 6th of the world
				int towerX = WorldGen.genRand.Next(300, Main.maxTilesX / 6); // from 50 since there's a unaccessible area at the world's borders
                // 50% of choosing the last 6th of the world
				if (WorldGen.genRand.NextBool())
				{
					towerX = Main.maxTilesX - towerX;
				}
                int towerY = WorldGen.genRand.Next(Main.maxTilesY / 12, Main.maxTilesY / 10);
			    Tile tile = Main.tile[towerX, towerY];
				if (tile.active())
				{
					continue;
				}
				// place the tower
				PlaceIsland(towerX, towerY, IslandShape, IslandWalls, IslandLoot);
				placed = true;
			}
		}
		#endregion
		private void PlaceAsteroid(int i, int j, int[,] ShrineArray) 
		{
			for (int y = 0; y < ShrineArray.GetLength(0); y++) { // This Loop Placzs Furnitures.(So that they have blocks to spawn on).
				for (int x = 0; x < ShrineArray.GetLength(1); x++) {
					int k = i - 3 + x;
					int l = j - 6 + y;
					if (WorldGen.InWorld(k, l, 30)){
						Tile tile = Framing.GetTileSafely(k, l);
						switch (ShrineArray[y, x]) {
							case 1:
								Framing.GetTileSafely(k, l).ClearTile();
								WorldGen.PlaceTile(k, l, mod.TileType("AsteroidBlock_Tile")); // Stone Slab
								break;	
							case 2:
								Framing.GetTileSafely(k, l).ClearTile();
								if (Main.rand.Next(2) == 0)
								{
									WorldGen.PlaceTile(k, l, mod.TileType("AsteroidBlock_Tile")); // Stone Slab
								}
								break;								
						}
					}
				}
			}
		}
		public void GenerateAsteroid()
		{
			int[,] AsteroidShape1 = new int[,]
			{
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,2,2,0,0,2,2,0,0,0,0},
				{0,0,2,1,1,1,1,1,1,1,2,0,0,0},
				{0,0,2,2,1,1,1,1,1,1,1,2,0,0},
				{0,0,2,1,1,1,1,1,1,1,1,2,0,0},
				{0,0,0,2,1,1,1,1,1,1,2,0,0,0},
				{0,0,0,0,2,1,1,1,1,2,0,0,0,0},
				{0,0,0,0,0,2,2,2,2,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0},

			};
			int[,] AsteroidShape2 = new int[,]
			{
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,2,2,2,2,2,2,2,2,0,0,0},
				{0,0,2,1,1,1,1,1,1,1,2,0,0,0},
				{0,2,1,1,1,1,1,1,1,2,0,0,0,0},
				{0,0,2,1,1,1,1,1,2,0,0,0,0,0},
				{0,0,0,2,2,1,1,1,2,0,0,0,0,0},
				{0,0,0,0,0,2,2,2,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0},

			};
			int[,] AsteroidShape3 = new int[,]
			{
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,2,2,2,2,0,0,0,0},
				{0,0,0,0,0,2,1,1,1,1,2,0,0,0},
				{0,0,0,0,0,2,1,1,1,1,1,2,0,0},
				{0,2,1,1,0,0,2,1,1,1,1,2,0,0},
				{0,2,1,1,2,0,0,1,1,2,2,0,0,0},
				{0,0,2,2,2,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0},


			};
			bool placed = false;
			while (!placed)
			{
				// Select a place in the first 6th of the world
				int towerX = WorldGen.genRand.Next(300, Main.maxTilesX/3); // from 50 since there's a unaccessible area at the world's borders
                // 50% of choosing the last 6th of the world
				if (WorldGen.genRand.NextBool())
				{
					towerX = Main.maxTilesX - towerX;
				}
                int towerY = WorldGen.genRand.Next(Main.maxTilesY / 20, Main.maxTilesY / 17);
			    Tile tile = Main.tile[towerX, towerY];
				if (towerX == Main.spawnTileX)
				{
					continue;
				}
				if (tile.active())
				{
					continue;
				}
				// place the tower
				PlaceAsteroid(towerX, towerY, AsteroidShape3);
				for (int k = 0; k < Main.rand.Next(3, 7); k++)
				{
					if (Main.rand.Next(3) == 0)
					{
						PlaceAsteroid(towerX + Main.rand.Next(-40, 40), towerY + Main.rand.Next(-20, 20), AsteroidShape1);
					}
					else if (Main.rand.Next(3) == 0)
					{
						PlaceAsteroid(towerX + Main.rand.Next(-40, 40), towerY + Main.rand.Next(-20, 20), AsteroidShape3);
					}
					else
					{
						PlaceAsteroid(towerX + Main.rand.Next(-40, 40), towerY + Main.rand.Next(-20, 20), AsteroidShape2);
					}
				}
				placed = true;
			}
		}

		private void PlaceGrave(int i, int j, int[,] ShrineArray, int[,] WallArray) 
		{
			
				for (int y = 0; y < WallArray.GetLength(0); y++) { // This Loop Placzs Furnitures.(So that they have blocks to spawn on).
				for (int x = 0; x < WallArray.GetLength(1); x++) {
					int k = i - 3 + x;
					int l = j - 6 + y;
					if (WorldGen.InWorld(k, l, 30)){
						Tile tile = Framing.GetTileSafely(k, l);
						switch (WallArray[y, x]) {
							case 1:
								Framing.GetTileSafely(k, l).ClearTile();
								WorldGen.KillWall(k, l);
								break;	
							case 2:
								Framing.GetTileSafely(k, l).ClearTile();
								WorldGen.KillWall(k, l);
								WorldGen.PlaceWall(k, l, 106); 
								break;	
							case 3:
								Framing.GetTileSafely(k, l).ClearTile();
								WorldGen.KillWall(k, l);
								if (Main.rand.Next(2) == 0)
								{
									WorldGen.PlaceWall(k, l, 106); 
								}
								else
								{
									WorldGen.PlaceWall(k, l, 63);
								}
								break;
							case 4:
								Framing.GetTileSafely(k, l).ClearTile();
								WorldGen.KillWall(k, l);
								{
									WorldGen.PlaceWall(k, l, 63); 
								}
								break;
							case 5:
								Framing.GetTileSafely(k, l).ClearTile();
								WorldGen.KillWall(k, l);
								break;								
						}
					}
				}		
			}
			for (int y = 0; y < ShrineArray.GetLength(0); y++) { // This Loop Placzs Furnitures.(So that they have blocks to spawn on).
				for (int x = 0; x < ShrineArray.GetLength(1); x++) {
					int k = i - 3 + x;
					int l = j - 6 + y;
					if (WorldGen.InWorld(k, l, 30)){
						Tile tile = Framing.GetTileSafely(k, l);
						switch (ShrineArray[y, x]) {
							case 1:
								Framing.GetTileSafely(k, l).ClearTile();
								WorldGen.PlaceTile(k, l, 0); // Dirt
								break;	
							case 3:
								Framing.GetTileSafely(k, l).ClearTile();
								WorldGen.PlaceTile(k, l, 0); // Dirt
								break;													
						}
					}
				}
			}
			for (int y = 0; y < ShrineArray.GetLength(0); y++) { // This Loop Placzs Furnitures.(So that they have blocks to spawn on).
				for (int x = 0; x < ShrineArray.GetLength(1); x++) {
					int k = i - 3 + x;
					int l = j - 6 + y;
					if (WorldGen.InWorld(k, l, 30)){
						Tile tile = Framing.GetTileSafely(k, l);
						switch (ShrineArray[y, x]) {
							case 1:
								WorldGen.PlaceTile(k, l, 2); // Dirt
								break;
							case 2:
								Framing.GetTileSafely(k, l).ClearTile();
								WorldGen.PlaceObject(k, l, 85, true, Main.rand.Next(0, 5));
								break;							
						}
					}
				}
			}
			for (int y = 0; y < ShrineArray.GetLength(0); y++) { // This Loop Placzs Furnitures.(So that they have blocks to spawn on).
				for (int x = 0; x < ShrineArray.GetLength(1); x++) {
					int k = i - 3 + x;
					int l = j - 6 + y;
					if (WorldGen.InWorld(k, l, 30)){
						Tile tile = Framing.GetTileSafely(k, l);
						switch (ShrineArray[y, x]) {
							case 2:
								int num256 = Sign.ReadSign(k, l, true);
								if (num256 >= 0)
								{
									switch (Main.rand.Next(6))
									{
										case 0:
										Sign.TextSign(num256, "The grave of a long-forgotten soul lies here.");
										break;
										case 1:
										Sign.TextSign(num256, "The text is too faded to read.");
										break;
										case 2:
										Sign.TextSign(num256, "");
										break;
										case 3:
										Sign.TextSign(num256, "");
										break;
										case 4:
										Sign.TextSign(num256, "The grave seems to be covered in indecipherable runes.");
										break;
										case 5:
										Sign.TextSign(num256, "The grave lists the burial practices of an ancient civilization.");
										break;
										default:
										Sign.TextSign(num256, "The text is too faded to read.");
										break;
									}
								}
							break;
						}
					}
				}
			}
		}

		public void GenerateGrave()
		{
			int[,] GraveShape = new int[,]
			{
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,2,0,0,2,0,0,2,0,0,2,0,0,2,0,0},
				{0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
				{0,0,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,0},
			};
			int[,] GraveWalls = new int[,]
			{
				{0,0,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,0},
				{0,0,5,5,4,5,5,4,5,5,5,5,5,5,5,5,5,5,0},
				{0,0,5,4,4,5,5,4,4,5,5,5,4,4,5,5,5,5,0},
				{0,0,4,4,2,3,3,3,2,3,3,3,2,2,2,3,3,5,0},
				{0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
				{0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
			};	
			bool placed = false;
			while (!placed)
			{
				// Select a place in the first 6th of the world
				int towerX = WorldGen.genRand.Next(Main.maxTilesX / 3, Main.maxTilesX / 3 * 2); // from 50 since there's a unaccessible area at the world's borders
				// 50% of choosing the last 6th of the world
				if (WorldGen.genRand.NextBool())
				{
					towerX = Main.maxTilesX - towerX;
				}
				int towerY = 0;
				// We go down until we hit a solid tile or go under the world's surface
				while (!WorldGen.SolidTile(towerX, towerY) && towerY <= Main.worldSurface)
				{
					towerY++;
				}
				// If we went under the world's surface, try again
				if (towerY > Main.worldSurface)
				{
					continue;
				}
				Tile tile = Main.tile[towerX, towerY];
				// If the type of the tile we are placing the tower on doesn't match what we want, try again
				if (tile.type != TileID.Dirt && tile.type != TileID.Grass && tile.type != TileID.Stone)
				{
					continue;
				}
				// place the tower
				PlaceGrave(towerX, towerY + 2, GraveShape, GraveWalls);
				placed = true;
			}
		}
		private void PlaceAbandonedHouse(int i, int j, int[,] ShrineArray, int[,] WallArray) 
		{
				for (int y = 0; y < WallArray.GetLength(0); y++) { // This Loop Placzs Furnitures.(So that they have blocks to spawn on).
				for (int x = 0; x < WallArray.GetLength(1); x++) {
					int k = i - 3 + x;
					int l = j - 6 + y;
					if (WorldGen.InWorld(k, l, 30)){
						Tile tile = Framing.GetTileSafely(k, l);
						switch (WallArray[y, x]) {
							case 1:
								Framing.GetTileSafely(k, l).ClearTile();
								WorldGen.KillWall(k, l);
								break;	
							case 2:
								Framing.GetTileSafely(k, l).ClearTile();
								WorldGen.KillWall(k, l);
								WorldGen.PlaceWall(k, l, 63); 
								break;	
							case 3:
								Framing.GetTileSafely(k, l).ClearTile();
								WorldGen.KillWall(k, l);
								if (Main.rand.Next(2) == 0)
								{
									WorldGen.PlaceWall(k, l, 4); 
								}
								else
								{
									WorldGen.PlaceWall(k, l, 63);
								}
								break;
							case 4:
								Framing.GetTileSafely(k, l).ClearTile();
								WorldGen.KillWall(k, l);
								{
									WorldGen.PlaceWall(k, l, 4); 
								}
								break;
							case 5:
								Framing.GetTileSafely(k, l).ClearTile();
								WorldGen.KillWall(k, l);
								break;
							case 6:
								Framing.GetTileSafely(k, l).ClearTile();
								WorldGen.KillWall(k, l);
								WorldGen.PlaceWall(k, l, 106);
								break;									
						}
					}
				}		
			}
			for (int y = 0; y < ShrineArray.GetLength(0); y++) { // This Loop Placzs Furnitures.(So that they have blocks to spawn on).
				for (int x = 0; x < ShrineArray.GetLength(1); x++) {
					int k = i - 3 + x;
					int l = j - 6 + y;
					if (WorldGen.InWorld(k, l, 30)){
						Tile tile = Framing.GetTileSafely(k, l);
						switch (ShrineArray[y, x]) {
							case 1:
								Framing.GetTileSafely(k, l).ClearTile();
								WorldGen.PlaceTile(k, l, 0); 
								break;	
							case 2:
								Framing.GetTileSafely(k, l).ClearTile();
								WorldGen.PlaceTile(k, l, 30); // Dirt
								break;	
							case 3:
								if (Main.rand.Next(2) == 0)	
								{
									Framing.GetTileSafely(k, l).ClearTile();
									WorldGen.PlaceTile(k, l, 30); // Dirt
								}
								break;
							case 7:	
								WorldGen.PlaceTile(k, l, 19); // Platforms
								tile.active(true);							
								break;												
						}
					}
				}
			}
			for (int y = 0; y < ShrineArray.GetLength(0); y++) { // This Loop Placzs Furnitures.(So that they have blocks to spawn on).
				for (int x = 0; x < ShrineArray.GetLength(1); x++) {
					int k = i - 3 + x;
					int l = j - 6 + y;
					if (WorldGen.InWorld(k, l, 30)){
						Tile tile = Framing.GetTileSafely(k, l);
						switch (ShrineArray[y, x]) {
							case 1:
								if (!Main.tile[k,l - 1].active())
								{
									WorldGen.PlaceTile(k, l, 2); // Dirt
								}
								break;
							case 5:
								Framing.GetTileSafely(k, l).ClearTile();
								WorldGen.PlaceTile(k, l, 28);  // Pot
								tile.active(true);
								break;
							case 6:	
								Framing.GetTileSafely(k, l).ClearTile();
								if (Main.rand.Next(2) == 0)
								{
									WorldGen.PlaceTile(k, l, 86);  
								}
								else if (Main.rand.Next(2) == 0)
								{
									WorldGen.PlaceTile(k, l, 18);
								}
								else
								{
									WorldGen.PlaceTile(k, l, 106);
								}
								tile.active(true);
								break;
							case 8:	
								Framing.GetTileSafely(k, l).ClearTile();
								WorldGen.PlaceTile(k, l, 15); //Chair
								break;
							case 9:	
								Framing.GetTileSafely(k, l).ClearTile();
								WorldGen.PlaceTile(k, l, 51); //Cobweb
								tile.active(true);
								break;	
							case 10:	
								WorldGen.PlaceObject(k, l, 13); //Bottles
								break;						
							case 11:	
								WorldGen.PlaceObject(k, l, 406); //Chimney
								break;
							case 12:	
								WorldGen.PlaceObject(k, l, 50, true, Main.rand.Next(0, 5)); //Books
								break;		
							case 13:
								WorldGen.PlaceChest(k, l, (ushort)mod.TileType("AbandonedChest"), false, 0); 
								break;		
																																								
						}
					}
				}
			}
		}	
		public void GenerateAbandonedHouse()
		{
			int[,] HouseShape = new int[,]
			{
				{0,0,0,0,0,0,0,0,0,0,0,0,0,11,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,3,2,2,2,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,2,0,0,2,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,13,0,2,0,0,0,0},
				{0,0,2,2,2,3,3,3,2,3,3,2,2,2,2,0,0,0,0},
				{0,0,2,0,10,12,0,0,0,0,0,9,9,9,2,0,0,0,0},
				{0,0,2,7,7,7,0,0,0,0,12,10,12,9,2,0,0,0,0},
				{0,0,2,9,9,0,0,0,0,0,7,7,7,7,2,0,0,0,0},
				{0,0,2,9,9,0,0,0,0,0,0,0,0,0,3,0,0,0,0},
				{0,0,0,9,0,0,0,0,0,0,0,0,0,9,3,0,0,0,0},
				{0,0,0,0,5,0,0,6,0,8,0,5,0,0,3,0,0,0,0},
				{1,1,2,2,1,1,2,2,1,2,2,2,1,1,2,1,1,1,1},
				{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},								
			};
			int[,] WallsShape = new int[,]
			{
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,5,5,3,2,2,3,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,5,4,3,3,2,0,0,0},
				{0,0,2,3,2,5,5,5,3,3,0,5,5,2,2,5,0,0,0},
				{0,0,0,3,4,2,2,2,3,3,3,5,5,3,3,5,5,5,0},
				{0,0,2,2,3,3,2,4,3,3,2,2,5,3,5,5,5,5,0},
				{0,0,3,3,3,3,2,4,5,5,5,2,3,3,5,5,5,5,0},
				{0,0,0,3,4,3,3,4,4,2,5,3,3,3,2,2,5,5,0},
				{0,0,0,2,4,4,4,3,3,3,3,2,3,3,2,2,5,5,0},
				{6,6,3,2,4,4,4,4,2,2,2,2,2,5,5,6,6,6,6},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,4,0,0,0,0,0,0,0,0,0,0,0,0},							
			};

			bool placed = false;
			while (!placed)
			{
				// Select a place in the first 6th of the world
				int towerX = WorldGen.genRand.Next(300, Main.maxTilesX); // from 50 since there's a unaccessible area at the world's borders
				// 50% of choosing the last 6th of the world
				if (WorldGen.genRand.NextBool())
				{
					towerX = Main.maxTilesX - towerX;
				}
				int towerY = 0;
				// We go down until we hit a solid tile or go under the world's surface
				while (!WorldGen.SolidTile(towerX, towerY) && towerY <= Main.worldSurface)
				{
					towerY++;
				}
				// If we went under the world's surface, try again
				if (towerY > Main.worldSurface)
				{
					continue;
				}
				Tile tile = Main.tile[towerX, towerY];
				// If the type of the tile we are placing the tower on doesn't match what we want, try again
				if (tile.type != TileID.Dirt && tile.type != TileID.Grass && tile.type != TileID.Stone)
				{
					continue;
				}
				// place the tower
				PlaceAbandonedHouse(towerX, towerY - 5, HouseShape, WallsShape);
				placed = true;
			}
		}
		private void PlaceGlowGrave(int i, int j, int[,] BlocksArray) 
		{
				for (int y = 0; y < BlocksArray.GetLength(0); y++) { // This Loop Placzs Furnitures.(So that they have blocks to spawn on).
				for (int x = 0; x < BlocksArray.GetLength(1); x++) {
					int k = i - 3 + x;
					int l = j - 6 + y;
					if (WorldGen.InWorld(k, l, 30)){
						Tile tile = Framing.GetTileSafely(k, l);
						switch (BlocksArray[y, x]) {
							case 0:
								break;
							case 1:
								tile.ClearTile();
								tile.type = 0;
								WorldGen.PlaceTile(k, l, 2);
								tile.active(true);
								break;
							case 3:
								tile.ClearTile();
								tile.type = 0;
								tile.active(true);
								break;
							case 5:
								tile.ClearTile();
								break;
						}
					}
				}
			}
			for (int y = 0; y < BlocksArray.GetLength(0); y++) { // This Loop Placzs Furnitures.(So that they have blocks to spawn on).
				for (int x = 0; x < BlocksArray.GetLength(1); x++) {
					int k = i - 3 + x;
					int l = j - 6 + y;
					if (WorldGen.InWorld(k, l, 30)){
						Tile tile = Framing.GetTileSafely(k, l);
						switch (BlocksArray[y, x]) {
							case 1:
								WorldGen.PlaceTile(k, l, 2);
								tile.active(true);
								break;
							case 2:
								Framing.GetTileSafely(k, l).ClearTile();
								WorldGen.PlaceObject(k, l, 85, true, Main.rand.Next(0, 5));
								int num256 = Sign.ReadSign(k, l, true);
								if (num256 >= 0)
								{
									switch (Main.rand.Next(6))
									{
										case 0:
										Sign.TextSign(num256, "The grave of a long-forgotten soul lies here.");
										break;
										case 1:
										Sign.TextSign(num256, "The text is too faded to read.");
										break;
										case 2:
										Sign.TextSign(num256, "");
										break;
										case 3:
										Sign.TextSign(num256, "");
										break;
										case 4:
										Sign.TextSign(num256, "The grave seems to be covered in indecipherable runes.");
										break;
										case 5:
										Sign.TextSign(num256, "The grave lists the burial practices of an ancient civilization.");
										break;
										default:
										Sign.TextSign(num256, "The text is too faded to read.");
										break;
									}
								}
								break;
							case 4:
								tile.ClearTile();
								if (Main.rand.Next(3) == 0)
								{
									WorldGen.PlaceTile(k, l, mod.TileType("GlowCap1"));
								}
								else if (Main.rand.Next(3) == 0)
								{
									WorldGen.PlaceTile(k, l, mod.TileType("GlowCap2"));
								}
								else
								{
									WorldGen.PlaceTile(k, l, mod.TileType("GlowCap3"));
								}
								break;

						}
					}
				}
			}
		}

		public void GenerateGlowGrave()
		{
			int[,] GraveShape1 = new int[,]
			{
				{0,0,3,3,1,1,1,1,1,1,1,3,3,0,0},
				{0,0,3,3,1,5,5,5,5,5,1,3,3,0,0},
				{0,3,3,1,1,5,5,5,5,5,1,1,3,3,0},
				{0,3,1,1,5,5,5,5,5,5,5,5,1,3,0},
				{0,3,1,1,5,5,5,5,5,5,5,5,1,3,0},
				{0,3,1,5,5,5,5,5,5,5,5,5,1,3,0},
				{0,3,1,5,5,5,5,5,5,5,5,5,1,3,0},
				{0,3,1,1,5,4,5,2,5,5,5,4,1,3,0},
				{0,3,3,1,1,1,1,1,1,1,1,1,3,3,0},
				{0,0,3,3,3,3,3,3,3,3,3,3,3,0,0},
			};
			bool placed = false;
			while (!placed)
			{
				int hideoutX = Main.rand.Next(50, Main.maxTilesX); // from 50 since there's a unaccessible area at the world's borders
				int hideoutY = Main.spawnTileY + Main.rand.Next(120, 700);
				Tile tile = Main.tile[hideoutX, hideoutY];
				if (!tile.active() || tile.type != TileID.Stone)
				{
					continue;
				}
			
				{
					PlaceGlowGrave(hideoutX, hideoutY, GraveShape1);
				}
				placed = true;
			}
		}
		private void PlacePond(int i, int j, int[,] ShrineArray, int[,] SecondArray) 
		{
				for (int y = 0; y < ShrineArray.GetLength(0); y++) { // This Loop Placzs Furnitures.(So that they have blocks to spawn on).
				for (int x = 0; x < ShrineArray.GetLength(1); x++) {
					int k = i - 3 + x;
					int l = j - 6 + y;
					if (WorldGen.InWorld(k, l, 30)){
						Tile tile = Framing.GetTileSafely(k, l);
						switch (ShrineArray[y, x]) {
							case 1:
								tile.ClearTile();
								tile.type = TileID.Dirt;
								tile.active(true); 
								if (!Main.tile[k,l - 1].active())
								{
									WorldGen.PlaceTile(k, l, 2); // Dirt
								}
								break;	
							case 2:
								tile.ClearTile();
								WorldGen.KillWall(k, l);
								tile.liquid = 255;
								break;	
							case 3:
								tile.ClearTile();
								WorldGen.PlaceTile(k, l, 19); 
								tile.active(true);
								break;
							case 4:
								tile.ClearTile();
								WorldGen.PlaceTile(k, l, 19); 
								tile.active(true);
								Main.tile[k, l].slope(2);
								break;	
							case 5:
								tile.ClearTile();
								WorldGen.PlaceTile(k, l, 19); 
								tile.active(true);
								Main.tile[k, l].slope(1);
								break;	
							case 6:
								tile.ClearTile();
								tile.type = TileID.WoodenBeam;
								tile.active(true);
								break;	
							case 7:
								tile.ClearTile();
								tile.type = TileID.RedDynastyShingles;
								tile.active(true);
								break;	
							case 8:
								tile.ClearTile();
								WorldGen.PlaceObject(k, l - 1, 42, true, 6);
								break;	
							case 9:
								tile.ClearTile();
								WorldGen.KillWall(k, l);
								break;
							case 11:
								tile.ClearTile();
								WorldGen.KillWall(k,l);
								WorldGen.PlaceWall(k, l, 106);
								break;
						}
					}
				}
			}
				for (int y = 0; y < SecondArray.GetLength(0); y++) { // This Loop Placzs Furnitures.(So that they have blocks to spawn on).
					for (int x = 0; x < SecondArray.GetLength(1); x++) {
						int k = i - 3 + x;
						int l = j - 6 + y;
						if (WorldGen.InWorld(k, l, 30)){
							Tile tile = Framing.GetTileSafely(k, l);
							switch (SecondArray[y, x]) 
							{
							case 1:
								WorldGen.KillWall(k, l);
								break;
							case 2:
								WorldGen.PlaceTile(k, l, 28);
								tile.active(true);
								break;
							case 3:
								WorldGen.PlaceChest(k, l, (ushort)mod.TileType("DynastyChestAbandoned"), false, 0); // Gold Chest
								break;	
							case 4:	
								WorldGen.PlaceWall(k, l, 106);	
								break;						
							case 5:	
								WorldGen.PlaceWall(k, l, 4);	
								break;						
														
						}
					}
				
				}
			}
		}
		public void GeneratePond()
		{
			int[,] PondShape = new int[,]
			{
				{9,9,9,9,9,9,9,9,9,9,7,7,7,7,9,9,9,9,9,9,9,9,9,9},
				{9,9,9,9,9,9,9,9,9,7,7,7,7,7,7,9,9,9,9,9,9,9,9,9},
				{9,9,9,9,9,9,9,9,7,7,7,7,7,7,7,7,9,9,9,9,9,9,9,9},
				{9,9,9,9,9,9,9,7,7,7,7,7,7,7,7,7,7,9,9,9,9,9,9,9},
				{9,9,9,9,9,9,9,9,9,6,9,9,9,9,6,9,9,9,9,9,9,9,9,9},
				{9,9,9,9,9,9,9,9,8,6,9,9,9,9,6,8,9,9,9,9,9,9,9,9},
				{9,9,9,9,9,9,9,9,9,6,9,9,9,9,6,9,9,9,9,9,9,9,9,9},
				{9,9,9,9,9,9,9,9,9,6,9,9,9,9,6,9,9,9,9,9,9,9,9,9},
				{9,9,9,9,9,9,9,4,3,3,3,3,3,3,3,3,5,9,9,9,9,9,9,9},
				{9,9,9,9,9,9,4,9,9,9,9,9,9,9,9,9,9,5,9,9,9,9,9,9},
				{9,9,9,9,9,4,9,9,9,9,9,9,9,9,9,9,9,9,5,9,9,9,9,9},
				{11,11,11,11,4,9,9,9,9,9,9,9,9,9,9,9,9,9,9,5,11,11,11,11},
				{1,1,1,1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1,1,1,1},
				{1,1,1,1,1,1,2,2,2,2,2,2,2,2,2,2,2,2,2,1,1,1,1,1},
				{1,1,1,1,1,1,1,2,2,2,2,2,2,2,2,2,2,2,2,1,1,1,1,1},
				{1,1,1,1,1,1,1,2,2,2,2,2,2,2,2,2,2,1,1,1,1,1,1,1},
				{1,1,1,1,1,1,1,1,1,1,2,2,2,2,2,2,1,1,1,1,1,1,1,1},
				{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
			};		
			int[,] LootShape = new int[,]
			{
				{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
				{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
				{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
				{1,1,1,1,1,1,1,1,5,5,5,5,5,5,5,5,1,1,1,1,1,1,1,1},	
				{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
				{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
				{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
				{1,1,1,1,1,1,1,1,4,4,4,4,4,4,4,4,1,1,1,1,1,1,1,1},	
				{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
				{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
				{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
				{2,4,4,4,4,1,1,1,1,1,1,1,1,1,1,1,1,1,1,4,4,4,2,4},	
				{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
				{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
				{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
				{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},	
				{1,1,1,1,1,1,1,1,1,1,1,1,3,1,1,1,1,1,1,1,1,1,1,1},
				{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},			
			};
			bool placed = false;
			while (!placed)
			{
				// Select a place in the first 6th of the world
				int towerX = WorldGen.genRand.Next(300, Main.maxTilesX); // from 50 since there's a unaccessible area at the world's borders
				// 50% of choosing the last 6th of the world
				if (WorldGen.genRand.NextBool())
				{
					towerX = Main.maxTilesX - towerX;
				}
				
				int towerY = 0;
				// We go down until we hit a solid tile or go under the world's surface
				while (!WorldGen.SolidTile(towerX, towerY) && towerY <= Main.worldSurface)
				{
					towerY++;
				}
				// If we went under the world's surface, try again
				if (towerY > Main.worldSurface)
				{
					continue;
				}

				Tile tile = Main.tile[towerX, towerY];
				// If the type of the tile we are placing the tower on doesn't match what we want, try again
				if (tile.type != TileID.Dirt && tile.type != TileID.Grass)
				{
					continue;
				}
				// place the tower
				PlacePond(towerX, towerY - 4, PondShape, LootShape);
				placed = true;
			}		
		}

		private void PlaceRuin(int i, int j, int[,] ShrineArray, int[,] SecondArray) 
		{
				for (int y = 0; y < ShrineArray.GetLength(0); y++) { // This Loop Placzs Furnitures.(So that they have blocks to spawn on).
				for (int x = 0; x < ShrineArray.GetLength(1); x++) {
					int k = i - 3 + x;
					int l = j - 6 + y;
					if (WorldGen.InWorld(k, l, 30)){
						Tile tile = Framing.GetTileSafely(k, l);
						switch (ShrineArray[y, x]) {
							case 1:
								tile.ClearTile();
								tile.type = TileID.Dirt;
								tile.active(true); 
								if (!Main.tile[k,l - 1].active())
								{
									WorldGen.PlaceTile(k, l, 2); // Dirt
								}
								break;	
							case 2:
								tile.ClearTile();
								if (Main.rand.Next(3) == 0)
								{
									tile.active(false);
								}
								else
								{
									tile.type = TileID.StoneSlab;
									tile.active(true);
								}
								break;	
							case 3:
								tile.ClearTile();
								tile.type = TileID.WoodenBeam;
								tile.active(true);
								break;
							case 4:
								tile.ClearTile();
								tile.type = TileID.StoneSlab;
								tile.active(true); 
								break;	
							case 5:
								tile.ClearTile();
								WorldGen.PlaceTile(k, l, 19); 
								tile.active(true);
								break;										
						}
					}
				}
			}
			int bannerType = Main.rand.Next(new int[]{0, 1, 2});
			for (int y = 0; y < ShrineArray.GetLength(0); y++) { // This Loop Placzs Furnitures.(So that they have blocks to spawn on).
				for (int x = 0; x < ShrineArray.GetLength(1); x++) {
					int k = i - 3 + x;
					int l = j - 6 + y;
					if (WorldGen.InWorld(k, l, 30)){
						Tile tile = Framing.GetTileSafely(k, l);
						switch (ShrineArray[y, x]) {
						case 6:
							if (Main.rand.Next(4) == 0)
							{
								WorldGen.PlaceObject(k, l, 13, true, Main.rand.Next(0, 2)); //Bottles
							}
							else
							{
								WorldGen.PlaceObject(k, l, 50, true, Main.rand.Next(0, 5)); //Books
							}
							break;		
						case 7:
							WorldGen.PlaceChest(k, l, (ushort)mod.TileType("AbandonedChest"), false, 0); // Gold Chest
							break;	
						case 8:
							WorldGen.PlaceTile(k, l, 28);  // Pot
							tile.active(true);
							break;		
						case 9:
							WorldGen.PlaceObject(k, l, 91, true, bannerType);  // Pot
							tile.active(true);
							break;		
						case 10:
							Framing.GetTileSafely(k, l).ClearTile();
							WorldGen.PlaceTile(k, l, 51); //Cobweb
							tile.active(true);
							break;	
						case 11:
							WorldGen.PlaceTile(k, l, 388, true, false); //Gate
							tile.active(true);
							break;	
						case 12:
							WorldGen.PlaceTile(k, l, mod.TileType("MapScroll_Tile"));
							break;
						case 13:
							WorldGen.PlaceTile(k, l, mod.TileType("WallScroll_Tile"));
							break;
						}
					}
				}
			}
			for (int y = 0; y < SecondArray.GetLength(0); y++) { // This Loop Placzs Furnitures.(So that they have blocks to spawn on).
					for (int x = 0; x < SecondArray.GetLength(1); x++) {
						int k = i - 3 + x;
						int l = j - 6 + y;
						if (WorldGen.InWorld(k, l, 30)){
							Tile tile = Framing.GetTileSafely(k, l);
							switch (SecondArray[y, x]) 
							{
							case 1:
								WorldGen.KillWall(k, l);
								WorldGen.PlaceWall(k, l, WallID.GrayBrick);	
								break;
							case 2:
								WorldGen.KillWall(k, l);
								WorldGen.PlaceWall(k, l, WallID.GrassUnsafe);	
								break;
							case 3:
								WorldGen.KillWall(k, l);
								WorldGen.PlaceWall(k, l, WallID.StoneSlab);	
								break;	
							case 4:	
								WorldGen.KillWall(k, l);
								if(Main.rand.Next(3) == 0)
								{
									WorldGen.PlaceWall(k, l, WallID.GrassUnsafe);	
								}
								else
								{
									WorldGen.PlaceWall(k, l, WallID.StoneSlab);	
								}
								break;											
														
						}
					}
				
				}
			}
		}
		public void GenerateRuin()
		{
			int[,] RuinShape1 = new int[,]
			{
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,8,0,8,0,0,0,0,0,0,0,0,0,8,0,0,0,0,0},
				{0,4,4,4,4,4,4,4,4,0,0,0,0,4,4,4,4,4,4,4,4,0},
				{0,4,4,4,4,4,2,4,4,5,5,5,5,4,2,2,3,4,4,0,4,0},
				{0,0,4,4,0,3,0,0,0,10,10,10,0,9,0,0,3,0,4,4,0,0},
				{0,0,4,0,6,3,6,6,12,0,10,10,0,0,6,6,3,0,2,2,0,0},
				{0,0,4,0,5,5,5,5,5,0,0,0,0,0,5,5,5,5,2,2,0,0},
				{0,0,4,0,0,3,0,0,0,0,0,0,0,0,0,0,3,0,4,0,0,0},
				{0,0,0,0,10,3,10,10,0,0,0,0,0,0,0,0,3,0,0,0,0,0},
				{0,0,0,0,10,3,10,10,10,0,0,0,0,0,0,0,3,0,0,0,0,0},
				{1,1,4,4,1,1,4,4,4,1,1,4,4,4,1,1,1,1,1,1,1,1},
				{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},			
			};
			int[,] RuinShape2 = new int[,]
			{
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,8,0,0,0,0,0,0,0,0,0,0,8,0,0,0,0,0},
				{0,4,4,4,4,4,4,2,2,0,0,0,0,4,2,4,4,4,4,4,4,0},
				{0,4,4,2,4,4,4,4,2,5,5,5,5,4,4,2,4,4,4,4,4,0},
				{0,0,4,2,0,3,9,0,0,0,0,0,0,0,0,9,3,0,2,2,0,0},
				{0,0,4,4,0,3,0,0,6,6,6,6,6,12,0,0,3,0,4,4,0,0},
				{0,0,4,4,10,3,0,0,5,5,5,5,5,5,0,10,3,0,4,4,0,0},
				{0,0,2,2,10,3,10,0,0,0,0,0,0,10,10,10,3,0,4,2,0,0},
				{0,0,2,2,10,3,10,0,0,0,0,0,0,10,10,10,3,0,4,2,0,0},
				{0,0,4,4,0,3,0,8,0,0,0,0,0,0,8,0,3,0,2,2,0,0},
			};
			int[,] RuinShape3 = new int[,]
			{
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,8,0,0,0,0,0,0,0,0,8,0,0,0,0,0,0},
				{0,4,4,4,4,4,4,4,4,0,0,0,0,4,4,4,4,4,4,4,4,0},
				{0,4,4,2,4,4,4,4,2,5,5,5,5,4,4,2,4,4,4,4,4,0},
				{0,0,4,2,0,3,10,10,0,0,0,0,0,6,6,6,3,6,2,4,0,0},
				{0,0,2,2,0,3,10,10,10,0,0,0,0,5,5,5,5,5,4,4,0,0},
				{0,0,4,4,6,3,6,6,12,10,0,0,0,0,0,10,3,0,4,2,0,0},
				{0,0,4,4,5,5,5,5,5,0,0,0,13,0,0,10,3,10,4,2,0,0},
				{0,0,4,4,10,3,10,0,0,0,0,0,0,0,0,0,3,10,2,2,0,0},
				{0,0,4,4,10,3,10,0,0,0,0,0,0,0,0,0,3,0,2,2,0,0},
			};
			int[,] RuinShape4 = new int[,]
			{
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,4,4,4,4,4,4,4,2,4,4,4,4,4,4,4,4,4,4,4,4,0},
				{0,4,4,2,4,4,2,2,2,4,4,4,2,4,4,2,4,4,4,4,4,0},
				{0,0,4,2,0,3,10,10,10,0,0,0,0,0,10,10,3,0,2,4,0,0},
				{0,0,2,2,0,3,10,10,0,0,7,0,0,0,0,0,3,0,4,4,0,0},
				{0,0,4,4,0,3,0,0,0,5,5,5,5,0,0,0,3,0,4,4,0,0},
				{0,0,4,4,10,3,0,0,0,0,0,0,0,0,0,0,3,0,4,2,0,0},
				{0,0,4,4,10,3,10,0,0,0,0,0,0,0,0,0,3,0,2,2,0,0},
				{0,0,4,4,10,3,10,0,0,0,0,0,0,0,6,10,3,0,2,2,0,0},
			};
			int[,] RuinWalls1 = new int[,]
			{
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,2,2,1,3,3,2,2,4,4,2,3,3,3,4,4,1,0,0,0,0},
				{0,0,0,2,2,3,3,3,2,2,2,2,4,4,4,3,3,2,0,0,0,0},
				{0,0,0,0,1,0,0,2,3,3,3,3,3,2,0,2,2,1,0,0,0,0},
				{0,0,0,0,1,2,2,3,3,4,4,4,3,4,0,0,2,1,0,0,0,0},
				{0,0,0,0,2,2,4,4,4,3,3,3,3,3,3,0,3,1,0,0,0,0},
				{0,0,0,2,1,3,4,4,4,3,0,0,2,4,4,4,4,1,2,2,0,0},
				{0,2,2,2,1,2,3,2,2,3,2,4,4,2,0,2,3,2,2,2,2,0},
				{0,2,2,2,1,2,3,2,2,2,4,4,2,2,2,3,3,2,2,2,2,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
			};
			int[,] RuinWalls2 = new int[,]
			{
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,1,3,2,2,3,0,0,4,4,4,4,4,4,1,0,0,0,0},
				{0,0,0,0,1,3,2,2,3,3,2,2,0,0,3,0,0,2,0,0,0,0},
				{0,0,0,0,1,3,3,3,4,4,3,4,4,0,2,2,0,1,0,0,0,0},
				{0,0,0,0,1,3,2,2,2,0,4,4,0,2,2,2,0,2,0,0,0,0},
				{0,0,0,0,1,0,2,2,2,0,4,4,0,2,2,4,0,1,0,0,0,0},
				{0,0,0,0,1,4,3,3,0,0,3,4,4,4,4,4,4,1,0,0,0,0},
				{0,0,0,0,2,2,2,0,2,0,2,2,2,4,3,3,2,1,0,0,0,0},
				{0,0,0,0,2,2,2,2,2,0,2,2,2,2,2,2,2,2,0,0,0,0},				
			};
			bool placed = false;
			bool secondStory = false;
			bool thirdStory = false;
			while (!placed)
			{
				// Select a place in the first 6th of the world
				int towerX = WorldGen.genRand.Next(300, Main.maxTilesX/6); // from 50 since there's a unaccessible area at the world's borders
				// 50% of choosing the last 6th of the world
				if (WorldGen.genRand.NextBool())
				{
					towerX = Main.maxTilesX - towerX;
				}
				int towerY = 0;
				// We go down until we hit a solid tile or go under the world's surface
				while (!WorldGen.SolidTile(towerX, towerY) && towerY <= Main.worldSurface)
				{
					towerY++;
				}
				// If we went under the world's surface, try again
				if (towerY > Main.worldSurface)
				{
					continue;
				}
				Tile tile = Main.tile[towerX, towerY];
				// If the type of the tile we are placing the tower on doesn't match what we want, try again
				if (tile.type != TileID.Dirt && tile.type != TileID.Grass)
				{
					continue;
				}
				// place the tower
				PlaceRuin(towerX, towerY - 5, RuinShape1, RuinWalls1);
				{		
					if (WorldGen.genRand.Next(2) == 0)
					{
						PlaceRuin(towerX, towerY - 12, RuinShape3, RuinWalls2);
					}
					else
					{
						PlaceRuin(towerX, towerY - 12, RuinShape2, RuinWalls2);
					}
				}
				if (WorldGen.genRand.Next(2) == 0)
				{
					if (WorldGen.genRand.Next(2) == 0)
					{
						PlaceRuin(towerX, towerY - 19, RuinShape2, RuinWalls2);
					}
					else
					{
						PlaceRuin(towerX, towerY - 19, RuinShape3, RuinWalls2);
					}
					thirdStory = true;
				}
				if (thirdStory && WorldGen.genRand.Next(2) == 0)
				{
					PlaceRuin(towerX, towerY - 26, RuinShape4, RuinWalls2);
				}
				placed = true;
			}		
		}
		private void PlaceIglooLake(int i, int j, int[,] ShrineArray, int[,] SecondArray) 
		{
				for (int y = 0; y < SecondArray.GetLength(0); y++) { // This Loop Placzs Furnitures.(So that they have blocks to spawn on).
				for (int x = 0; x < SecondArray.GetLength(1); x++) {
					int k = i - 3 + x;
					int l = j - 6 + y;
					if (WorldGen.InWorld(k, l, 30)){
						Tile tile = Framing.GetTileSafely(k, l);
						switch (SecondArray[y, x]) {	
						case 3:
							tile.ClearTile();
							WorldGen.PlaceWall(k, l, 40);
							break;

							}
						}
					}		  
				}
				for (int y = 0; y < ShrineArray.GetLength(0); y++) { // This Loop Placzs Furnitures.(So that they have blocks to spawn on).
				for (int x = 0; x < ShrineArray.GetLength(1); x++) {
					int k = i - 3 + x;
					int l = j - 6 + y;
					if (WorldGen.InWorld(k, l, 30)){
						Tile tile = Framing.GetTileSafely(k, l);
						switch (ShrineArray[y, x]) {
							case 1:
								tile.ClearTile();
								tile.type = TileID.IceBlock;
								tile.active(true); 	
								break;
							case 2:
								tile.ClearTile();
								tile.type = TileID.SnowBlock;
								tile.active(true); 	
								break;	
							case 3:
								tile.ClearTile();
								WorldGen.KillWall(k, l);
								tile.liquid = 255;
								break;
						}
					}
				}
			}
				for (int y = 0; y < ShrineArray.GetLength(0); y++) { // This Loop Placzs Furnitures.(So that they have blocks to spawn on).
				for (int x = 0; x < ShrineArray.GetLength(1); x++) {
					int k = i - 3 + x;
					int l = j - 6 + y;
					if (WorldGen.InWorld(k, l, 30)){
						Tile tile = Framing.GetTileSafely(k, l);
						switch (ShrineArray[y, x]) {
							case 4:
								Framing.GetTileSafely(k, l).ClearTile();
								WorldGen.PlaceTile(k, l, 15); //Chair
								break;	
							case 5:
								tile.ClearTile();
								WorldGen.PlaceObject(k, l, 376); //WoodCrate
								break;	
							case 6:
								tile.ClearTile();
								WorldGen.PlaceObject(k, l, 215, true, 0);
								break;	
							case 7:	
								tile.ClearTile();
								WorldGen.PlaceTile(k, l, 10, true, true, 29);
								break;	
							}
						}
					}
				}
				for (int y = 0; y < ShrineArray.GetLength(0); y++) { // This Loop Placzs Furnitures.(So that they have blocks to spawn on).
				for (int x = 0; x < ShrineArray.GetLength(1); x++) {
					int k = i - 3 + x;
					int l = j - 6 + y;
					if (WorldGen.InWorld(k, l, 30)){
						Tile tile = Framing.GetTileSafely(k, l);
						switch (ShrineArray[y, x]) {
							case 8:	
								tile.ClearTile();
								WorldGen.PlaceObject(k, l, 33, true, 20);
								break;	
							case 9:
								tile.ClearTile();
								WorldGen.PlaceObject(k, l, 376); //WoodCrate
								break;
							case 10: 
								WorldGen.PlaceObject(k, l, 50, true, 2); //Books
								break;
						}
					}
				}
			}
		}

		public void GenerateIglooPond()
		{
			int[,] IglooShape1 = new int[,]
			{
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,2,2,2,2,2,2,2,2,2,2,2,2,2,2,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,2,2,2,1,1,1,1,1,1,1,1,1,1,1,2,2,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,2,2,1,1,1,1,0,0,0,0,0,0,0,1,1,2,2,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,2,2,1,1,0,0,0,0,0,0,0,0,0,0,0,1,1,2,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,2,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,2,2,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,7,0,0,0,10,0,9,8,0,0,0,0,0,0,0,1,2,2,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,2,2,2,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,5,0,5,4,0,0,0,0,6,0,1,2,2,2,2,0,0,0,0,0},
				{0,0,2,1,1,1,1,2,2,2,1,1,1,2,2,2,2,2,2,1,1,1,1,1,1,2,2,0,0,0,0,0},
				{0,0,2,2,1,1,1,2,2,2,2,1,1,2,2,2,2,2,2,1,1,1,1,1,1,2,0,0,0,0,0,0},
				{0,0,0,2,2,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,2,2,2,0,0,0,0,0,0},
				{0,0,0,2,2,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,2,2,2,0,0,0,0,0,0,0},
				{0,0,0,0,2,2,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,2,2,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,0,0,0,0,0,0,0,0},
			};
			int[,] IglooWalls1 = new int[,]
			{
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,1,1,3,3,3,3,3,3,3,3,3,1,1,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,1,1,3,3,3,3,3,3,3,3,3,3,3,1,1,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,1,1,3,3,3,3,3,3,3,3,3,3,3,3,3,1,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,1,3,3,3,3,3,3,3,3,3,3,3,3,3,3,1,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,1,3,3,3,3,3,3,3,3,3,3,3,3,3,3,1,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,1,3,3,3,3,3,3,3,3,3,3,3,3,3,3,1,0,0,0,0,0,0,0,0,0},
				{0,0,2,1,1,1,1,2,2,2,1,1,1,2,2,2,2,2,2,1,1,1,1,1,1,2,2,0,0,0,0,0},
				{0,0,2,2,1,1,1,2,2,2,2,1,1,2,2,2,2,2,2,1,1,1,1,1,1,2,0,0,0,0,0,0},
				{0,0,0,2,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,2,2,0,0,0,0,0,0},
				{0,0,0,2,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,2,2,0,0,0,0,0,0,0},
				{0,0,0,0,2,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,2,2,0,0,0,0,0,0,0},
				{0,0,0,0,0,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,0,0,0,0,0,0,0},
				{0,0,0,0,0,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,0,0,0,0,0,0,0,0},
			};
			bool placed = false;
			while (!placed)
			{
				// Select a place in the first 6th of the world
				int towerX = WorldGen.genRand.Next(300, Main.maxTilesX); // from 50 since there's a unaccessible area at the world's borders
				// 50% of choosing the last 6th of the world
				if (WorldGen.genRand.NextBool())
				{
					towerX = Main.maxTilesX - towerX;
				}
				
				int towerY = 0;
				// We go down until we hit a solid tile or go under the world's surface
				while (!WorldGen.SolidTile(towerX, towerY) && towerY <= Main.worldSurface)
				{
					towerY++;
				}
				// If we went under the world's surface, try again
				if (towerY > Main.worldSurface)
				{
					continue;
				}

				Tile tile = Main.tile[towerX, towerY];
				// If the type of the tile we are placing the tower on doesn't match what we want, try again
				if (tile.type != TileID.IceBlock && tile.type != TileID.SnowBlock)
				{
					continue;
				}
				// place the tower
				PlaceIglooLake(towerX, towerY - 4, IglooShape1, IglooWalls1);
				placed = true;
			}		
		}
		private void PlaceIceCabin(int i, int j, int[,] ShrineArray, int[,] SecondArray) 
		{
			{
				for (int y = 0; y < SecondArray.GetLength(0); y++) { // This Loop Placzs Furnitures.(So that they have blocks to spawn on).
				for (int x = 0; x < SecondArray.GetLength(1); x++) {
					int k = i - 3 + x;
					int l = j - 6 + y;
					if (WorldGen.InWorld(k, l, 30)){
						Tile tile = Framing.GetTileSafely(k, l);
						switch (SecondArray[y, x]) {	
						case 1:
							tile.ClearTile();
							WorldGen.KillWall(k, l);
							break;
						case 2:
							tile.ClearTile();
							WorldGen.KillWall(k, l);
							break;
						case 3:
							tile.ClearTile();
							WorldGen.KillWall(k, l);
							if (Main.rand.Next(6) == 0)
							{
								WorldGen.PlaceWall(k, l, 127);
							}
							else if (Main.rand.Next(3) == 0)
							{
								WorldGen.PlaceWall(k, l, 84);
							}
							else
							{
								WorldGen.PlaceWall(k, l, 40);
							}
							break;

							}
						}
					}		  
				}
			}
			{
				for (int y = 0; y < ShrineArray.GetLength(0); y++) { // This Loop Placzs Furnitures.(So that they have blocks to spawn on).
				for (int x = 0; x < ShrineArray.GetLength(1); x++) {
					int k = i - 3 + x;
					int l = j - 6 + y;
					if (WorldGen.InWorld(k, l, 30)){
						Tile tile = Framing.GetTileSafely(k, l);
						switch (ShrineArray[y, x]) {
							case 1:
								WorldGen.PlaceTile(k, l, 206);
								break;
							case 2:
								if (Main.rand.Next(6) == 0)
								{
									WorldGen.PlaceTile(k, l, TileID.SnowBlock);
								}
								else
								{
									WorldGen.PlaceTile(k, l, TileID.IceBlock);
								}
								break;
							case 9:
								tile.ClearTile();
								WorldGen.PlaceTile(k, l, 19, true, false, -1, 35); 
								break;
							}
						}
					}
				}
			}
			{
					for (int y = 0; y < ShrineArray.GetLength(0); y++) { // This Loop Placzs Furnitures.(So that they have blocks to spawn on).
					for (int x = 0; x < ShrineArray.GetLength(1); x++) {
						int k = i - 3 + x;
						int l = j - 6 + y;
						if (WorldGen.InWorld(k, l, 30)){
							Tile tile = Framing.GetTileSafely(k, l);
							switch (ShrineArray[y, x]) {
							case 5:
								Framing.GetTileSafely(k, l).ClearTile();
								WorldGen.PlaceTile(k, l, 15, true, false, -1, 28); //Chair
								break;	
							case 6:
								tile.ClearTile();
								{								
									WorldGen.PlaceTile(k, l, 14, true, false, -1, 24); //Table
								}
								break;	
							case 7:
								tile.ClearTile();
								if (Main.rand.Next(2) == 0)
								{
									WorldGen.PlaceTile(k, l, 104, true, false, -1, 11);
								}
								else if (Main.rand.Next(2) == 0)
								{
									WorldGen.PlaceTile(k + 1, l, 172, true, false, -1, 21);
								}
								else if (Main.rand.Next(2) == 0)
								{
									WorldGen.PlaceTile(k + 1, l, 101, true, false, -1, 17);
								}
								else
								{
									WorldGen.PlaceTile(k + 1, l, 87, true, false, -1, 7);
								}
								break;	
							case 8:	
								tile.ClearTile();
								WorldGen.PlaceTile(k, l, 28, true, false, -1, Main.rand.Next(5, 6));
								break;	
							case 10:
								tile.ClearTile();
								WorldGen.PlaceObject(k, l, 13, true, Main.rand.Next(0, 2));
								break;
							case 11:
								Framing.GetTileSafely(k, l).ClearTile();
								if (Main.rand.Next(3) == 0)
								{
									WorldGen.PlaceTile(k, l, mod.TileType("FrostTome_Tile"));
								}
								break;
								
							}
						}
					}
				}
			}			
		}

		public void GenerateIceCabin()
		{
			int[,] IceCabin1 = new int[,]
			{
				{0,0,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,0,0,0},
				{0,0,2,2,3,3,3,3,3,3,3,3,2,2,2,2,2,0,0,0},
				{0,0,2,10,11,3,3,3,3,3,3,3,3,3,2,2,2,0,0,0},
				{0,0,2,9,9,3,3,3,3,3,3,3,3,3,3,2,2,0,0,0},
				{0,0,2,3,3,3,3,3,3,3,3,3,3,3,3,2,2,0,0,0},
				{0,0,2,3,3,3,3,3,3,3,3,3,3,8,3,3,2,0,0,0},
				{0,0,2,3,3,7,3,3,6,3,5,3,3,1,1,1,2,0,0,0},
				{0,0,2,2,2,2,1,2,2,1,1,1,1,1,2,2,2,0,0,0},
			};
			int[,] IceCabin2 = new int[,]
			{
				{0,0,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,0,0,0},
				{0,0,2,2,3,3,3,3,3,3,3,3,3,3,3,3,3,2,0,0,0},
				{0,0,2,10,11,8,3,3,3,3,3,3,3,3,3,3,3,2,0,0,0},
				{0,0,1,9,9,9,9,3,3,3,3,3,3,3,10,3,10,2,0,0,0},
				{0,0,1,2,3,3,3,3,3,3,3,3,3,9,9,9,9,2,0,0,0},
				{0,0,1,2,1,2,3,3,3,3,3,3,3,3,2,2,2,2,0,0,0},
				{0,0,2,2,2,2,3,3,6,3,7,3,3,3,2,2,2,2,0,0,0},
				{0,0,2,2,2,1,1,1,2,2,1,1,1,1,1,2,2,2,0,0,0},
			};
			int[,] IceCabinWalls = new int[,]
			{
				{0,0,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,0,0,0},
				{0,0,2,3,3,3,3,3,3,3,3,3,3,3,3,3,3,2,0,0,0},
				{0,0,2,3,3,3,3,3,3,3,3,3,3,3,3,3,3,2,0,0,0},
				{0,0,1,3,3,3,3,3,3,3,3,3,3,3,3,3,3,2,0,0,0},
				{0,0,1,3,3,3,3,3,3,3,3,3,3,3,3,3,3,2,0,0,0},
				{0,0,1,3,3,3,3,3,3,3,3,3,3,3,3,3,3,2,0,0,0},
				{0,0,2,2,3,3,3,3,3,3,3,3,3,3,1,1,1,2,0,0,0},
				{0,0,2,2,2,2,1,1,2,2,1,1,1,1,1,2,2,2,0,0,0},
			};	
			bool placed = false;
			while (!placed)
			{
				int hideoutX = Main.rand.Next(50, Main.maxTilesX); // from 50 since there's a unaccessible area at the world's borders
				int hideoutY = Main.spawnTileY + Main.rand.Next(120, 700);
				Tile tile = Main.tile[hideoutX, hideoutY];
				if (!tile.active() || tile.type != TileID.IceBlock)
				{
					continue;
				}
				if (WorldGen.genRand.Next(2) == 0)
				{
					PlaceIceCabin(hideoutX, hideoutY, IceCabin1, IceCabinWalls);
				}
				else
				{
					PlaceIceCabin(hideoutX, hideoutY, IceCabin2, IceCabinWalls);
				}
				placed = true;
			}
		}
		private void PlaceGemStash(int i, int j, int[,] BlocksArray, int[,] WallsArray, int[,] LootArray) 
		{
			for (int y = 0; y < WallsArray.GetLength(0); y++) 
			{
				for (int x = 0; x < WallsArray.GetLength(1); x++)
				 {
					int k = i - 3 + x;
					int l = j - 6 + y;
					if (WorldGen.InWorld(k, l, 30)){
						Tile tile = Framing.GetTileSafely(k, l);
						switch (WallsArray[y, x]) {
							case 0:
								break;
							case 1:
								WorldGen.KillWall(k, l);
								Framing.GetTileSafely(k, l).ClearTile();
								break;	
							case 2:
								WorldGen.KillWall(k, l);
								Framing.GetTileSafely(k, l).ClearTile();
								break;	
							case 3:
								WorldGen.KillWall(k, l);
								Framing.GetTileSafely(k, l).ClearTile();
								break;	
							case 5:
								WorldGen.KillWall(k, l);
								Framing.GetTileSafely(k, l).ClearTile();
								break;			
						}
					}
				}
			}	
			for (int y = 0; y < BlocksArray.GetLength(0); y++) 
			{
				for (int x = 0; x < BlocksArray.GetLength(1); x++)
				 {
					int k = i - 3 + x;
					int l = j - 6 + y;
					if (WorldGen.InWorld(k, l, 30)){
						Tile tile = Framing.GetTileSafely(k, l);
						switch (BlocksArray[y, x]) {
							case 0:
								break;
							case 1:
								Framing.GetTileSafely(k, l).ClearTile();
								break;	
							case 2:
								Framing.GetTileSafely(k, l).ClearTile();
								break;	
							case 3:
								Framing.GetTileSafely(k, l).ClearTile();
								break;
						}
					}
				 }
			}
			for (int y = 0; y < BlocksArray.GetLength(0); y++) 
			{
				for (int x = 0; x < BlocksArray.GetLength(1); x++)
				 {
					int k = i - 3 + x;
					int l = j - 6 + y;
					if (WorldGen.InWorld(k, l, 30)){
						Tile tile = Framing.GetTileSafely(k, l);
						switch (BlocksArray[y, x]) {
							case 0:
								break;
							case 1:
								WorldGen.PlaceTile(k, l, 30);
								tile.active(true);
								break;	
							case 2:
								WorldGen.PlaceTile(k, l, 19);
								tile.active(true);
								break;	
							case 3:
								WorldGen.PlaceTile(k, l, 63);
								tile.active(true);
								break;
							case 4:
								WorldGen.PlaceTile(k, l, 51);
								tile.active(true);
								break;				
							case 7:
								WorldGen.PlaceTile(k, l, 64);
								tile.active(true);
								break;	
						}
					}
				}
			}
			for (int y = 0; y < WallsArray.GetLength(0); y++) 
			{
				for (int x = 0; x < WallsArray.GetLength(1); x++)
				 {
					int k = i - 3 + x;
					int l = j - 6 + y;
					if (WorldGen.InWorld(k, l, 30)){
						Tile tile = Framing.GetTileSafely(k, l);
						switch (WallsArray[y, x]) {
							case 0:
								break;
							case 3:
								WorldGen.PlaceWall(k, l, 27);
								break;			
						}
					}
				}
			}
			for (int y = 0; y < LootArray.GetLength(0); y++) 
			{
				for (int x = 0; x < LootArray.GetLength(1); x++)
				 {
					int k = i - 3 + x;
					int l = j - 6 + y;
					if (WorldGen.InWorld(k, l, 30)){
						Tile tile = Framing.GetTileSafely(k, l);
						switch (LootArray[y, x]) {
							case 0:
								break;
							case 4:
								WorldGen.PlaceObject(k, l, 376);  // Crate
								break;
							case 5:
								if (Main.rand.NextFloat(1.32f) == 0);
								{
									WorldGen.PlaceTile(k, l, 28);  // Pot
								}
								tile.active(true);
								break;
							case 6:
								int objects;
								if (Main.rand.Next (3) == 0)
								{
									objects = 105;
								}
								else if (Main.rand.Next(2) == 0)
								{
									objects = 16;
								}
								else if (Main.rand.Next(4) == 0)
								{
									objects = 87;
								}
								else if (Main.rand.Next(4) == 0)
								{
									objects = 18;
								}
								else 
								{
									objects = 28;
								}
								WorldGen.PlaceObject(k, l, (ushort)objects);  // Misc
								break;
							case 7:
								WorldGen.PlaceObject(k, l-1, mod.TileType("GemsPickaxeSapphire"));  // Special Pick		
								break;
							case 8:
								if (Main.rand.Next (3) == 0)
								{
									objects = 105;
								}
								else if (Main.rand.Next(2) == 0)
								{
									objects = 16;
								}
								else if (Main.rand.Next(4) == 0)
								{
									objects = 87;
								}
								else if (Main.rand.Next(4) == 0)
								{
									objects = 18;
								}
								else 
								{
									objects = 28;
								}
								WorldGen.PlaceObject(k, l, (ushort)objects);  // Another Misc Obj
								break;
							case 9:
								WorldGen.PlaceObject(k, l-1, mod.TileType("GemsPickaxeRuby"));  // Special Pick		
								break;							
						}
					}
				}
			}	
		}	
		public void GenerateGemStash()
		{
			
			int[,] StashRoomMain = new int[,]
			{
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,1,2,2,2,1,1,1,1,1,1,1,1,1,1,0,0,1,1,1,0,1,1,1,0,0},
				{0,0,1,4,4,0,0,0,0,0,4,4,4,4,4,4,0,0,0,0,0,4,4,4,1,0,0},
				{0,0,1,4,4,0,0,0,0,0,0,0,0,0,4,4,0,0,0,0,0,0,4,4,1,0,0},
				{0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,4,0,0,0,0,0,0,0,4,1,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,2,0,2,2,2,2,0,0,0},
				{0,0,1,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,3,0},
				{0,0,1,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,3,0},
				{0,0,0,4,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,3,3,3,0},
				{0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0},
			};
			int[,] StashRoomMain1 = new int[,]
			{
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,1,2,2,2,1,1,1,1,1,1,1,1,1,1,0,0,1,1,1,0,1,1,1,0,0},
				{0,0,1,4,4,0,0,0,0,0,4,4,4,4,4,4,0,0,0,0,0,4,4,4,1,0,0},
				{0,0,1,4,4,0,0,0,0,0,0,0,0,0,4,4,0,0,0,0,0,0,4,4,1,0,0},
				{0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,4,0,0,0,0,0,0,0,4,1,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,2,0,2,2,2,2,0,0,0},
				{0,0,1,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,7,0},
				{0,0,1,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,7,7,7,0},
				{0,0,0,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,7,0,7,7,7,7,0},
				{0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0},
			};
			int[,] StashMainWalls = new int[,]
			{
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,1,2,2,2,1,1,1,1,1,1,1,1,1,1,0,0,1,1,1,0,1,1,1,0,0},
				{0,0,1,3,3,3,3,3,3,3,5,3,3,3,3,3,3,3,3,3,3,3,0,3,1,0,0},
				{0,0,1,5,5,3,3,3,3,3,3,5,3,3,3,3,3,3,3,3,3,3,0,3,1,0,0},
				{0,0,1,5,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,1,0,0},
				{0,0,0,3,5,3,3,3,3,3,5,3,3,3,3,3,3,3,3,3,3,3,3,3,0,0,0},
				{0,0,1,3,3,3,3,3,3,5,3,3,3,3,3,3,3,3,3,3,3,0,0,3,1,0,0},
				{0,0,1,5,3,3,3,3,3,3,5,3,3,3,3,3,3,3,3,3,3,0,0,3,1,0,0},
				{0,0,0,3,5,5,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,0,0,0,0,0},
				{0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0},
			};
			int[,] StashMainLoot = new int[,]
			{
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,5,0,5,0,0,0,7,0,0,5,0,0,6,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
			};
			int[,] StashMainLoot1 = new int[,]
			{
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,4,0,5,0,0,0,0,9,0,0,5,0,0,6,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
			};
			int[,] StashRoom1 = new int[,]
			{
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,1,1,1,0,1,1,1,1,2,2,2,2,1,1,1,1,1,1,0,1,0,0,0,0},
				{0,0,0,1,4,4,0,0,0,0,0,0,0,0,0,0,0,0,0,4,4,4,1,0,0,0,0},
				{0,0,1,1,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4,4,1,0,0,0,0},
				{0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4,0,1,0,0,0,0},
				{0,0,0,1,0,0,2,2,2,0,0,0,0,2,2,2,2,2,2,0,0,0,1,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0},
				{0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,1,1,1,1,1,1,1,0,1,2,2,2,2,1,1,1,1,1,0,1,0,0,0,0},
			};
			int[,] Stash1Walls = new int[,]
			{
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,1,2,2,2,1,1,1,1,1,1,1,1,1,1,0,0,1,1,1,0,1,1,1,0,0},
				{0,0,1,3,3,3,3,3,3,3,5,3,3,3,3,3,3,3,3,3,3,3,0,3,3,0,0},
				{0,0,1,0,5,3,3,5,5,3,3,3,3,3,3,5,5,5,3,3,3,3,5,3,3,0,0},
				{0,0,1,0,3,3,3,3,3,3,3,3,3,3,3,3,3,5,5,3,3,3,3,3,1,0,0},
				{0,0,0,3,5,3,3,3,3,3,5,3,3,3,5,3,3,3,3,3,3,3,3,3,0,0,0},
				{0,0,1,3,5,3,3,3,3,5,3,3,3,3,5,3,3,3,3,3,3,5,5,3,3,0,0},
				{0,0,1,0,3,3,3,3,3,3,3,3,3,3,3,3,3,5,5,3,3,5,5,3,3,0,0},
				{0,0,0,3,5,5,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,5,0,3,0,0},
				{0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0},
			};
			int[,] Stash1Loot = new int[,]
			{
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,4,0,0,0,0,0,0,0,0,5,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,5,0,0,8,0,0,0,5,0,5,0,5,0,8,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
			};
			bool placed = false;
			while (!placed)
			{
				int hideoutX = (Main.spawnTileX + Main.rand.Next(-800, 800)); // from 50 since there's a unaccessible area at the world's borders
				int hideoutY = Main.spawnTileY + Main.rand.Next(120, 400);
				// place the hideout
				if (WorldGen.genRand.Next(2) == 0)
				{
					PlaceGemStash(hideoutX, hideoutY, StashRoomMain, StashMainWalls, StashMainLoot);
				}
				else
				{
					PlaceGemStash(hideoutX, hideoutY, StashRoomMain1, StashMainWalls, StashMainLoot1);
				}
				if (WorldGen.genRand.Next(2) == 0)
				{
					PlaceGemStash(hideoutX + (Main.rand.Next(-5, 5)), hideoutY - 8, StashRoom1, Stash1Walls, Stash1Loot);
				}
				placed = true;
			}
		}
		private void PlaceIceHouse(int i, int j, int[,] BlocksArray, int[,] WallsArray) 
		{
			for (int y = 0; y < WallsArray.GetLength(0); y++) 
			{
				for (int x = 0; x < WallsArray.GetLength(1); x++)
				 {
					int k = i - 3 + x;
					int l = j - 6 + y;
					if (WorldGen.InWorld(k, l, 30)){
						Tile tile = Framing.GetTileSafely(k, l);
						switch (WallsArray[y, x]) {
							case 0:
								break;
							case 1:
								WorldGen.KillWall(k, l);
								Framing.GetTileSafely(k, l).ClearTile();
								break;	
							case 2:
								WorldGen.KillWall(k, l);
								Framing.GetTileSafely(k, l).ClearTile();
								WorldGen.PlaceWall(k, l, 150);
								break;	
							case 3:
								WorldGen.KillWall(k, l);
								Framing.GetTileSafely(k, l).ClearTile();
								WorldGen.PlaceWall(k, l, 149);
								break;			
							case 4:
								WorldGen.KillWall(k, l);
								Framing.GetTileSafely(k, l).ClearTile();
								WorldGen.PlaceWall(k, l, 5);
								break;		
							case 5:
								WorldGen.KillWall(k, l);
								Framing.GetTileSafely(k, l).ClearTile();
								break;	
							case 6:
								WorldGen.KillWall(k, l);
								Framing.GetTileSafely(k, l).ClearTile();
								WorldGen.PlaceWall(k, l, 4);
								break;			
							case 7:
								WorldGen.KillWall(k, l);
								Framing.GetTileSafely(k, l).ClearTile();
								WorldGen.PlaceWall(k, l, 21);
								break;			
						}
					}
				}
			}
			for (int y = 0; y < BlocksArray.GetLength(0); y++) 
			{
				for (int x = 0; x < BlocksArray.GetLength(1); x++)
				 {
					int k = i - 3 + x;
					int l = j - 6 + y;
					if (WorldGen.InWorld(k, l, 30)){
						Tile tile = Framing.GetTileSafely(k, l);
						switch (BlocksArray[y, x]) {
							case 0:
								break;
							case 1:
								Framing.GetTileSafely(k, l).ClearTile();
								WorldGen.PlaceTile(k, l, 147);
								tile.active(true);
								break;	
							case 2:
								Framing.GetTileSafely(k, l).ClearTile();
								WorldGen.PlaceTile(k, l, 321);
								tile.active(true);
								break;	
							case 3:
								Framing.GetTileSafely(k, l).ClearTile();
								WorldGen.PlaceTile(k, l, 38);
								tile.active(true);
								break;
							case 12:
								Framing.GetTileSafely(k, l).ClearTile();
								WorldGen.PlaceTile(k, l, 38);
								tile.slope(2);
								tile.active(true);
								break;
							case 11:
								Framing.GetTileSafely(k, l).ClearTile();
								WorldGen.PlaceTile(k, l, 38);
								tile.slope(1);
								tile.active(true);
								break;
							case 4:
								Framing.GetTileSafely(k, l).ClearTile();
								WorldGen.PlaceTile(k, l, 124);
								tile.active(true);
								break;				
							case 7:
								Framing.GetTileSafely(k, l).ClearTile();
								WorldGen.PlaceTile(k, l, 19, true, false, -1, 19); 
								tile.active(true);
								break;	
						}
					}
				}
			}
			for (int y = 0; y < BlocksArray.GetLength(0); y++) 
			{
				for (int x = 0; x < BlocksArray.GetLength(1); x++)
				 {
					int k = i - 3 + x;
					int l = j - 6 + y;
					if (WorldGen.InWorld(k, l, 30)){
						Tile tile = Framing.GetTileSafely(k, l);
						switch (BlocksArray[y, x]) {
						case 5:
							Framing.GetTileSafely(k, l).ClearTile();
							WorldGen.PlaceObject(k, l, 405);
							break;					
						}
					}
				}
			}
			for (int y = 0; y < BlocksArray.GetLength(0); y++) 
			{
				for (int x = 0; x < BlocksArray.GetLength(1); x++)
				 {
					int k = i - 3 + x;
					int l = j - 6 + y;
					if (WorldGen.InWorld(k, l, 30)){
						Tile tile = Framing.GetTileSafely(k, l);
						switch (BlocksArray[y, x]) {
							case 0:
								break;
							case 6:
								Framing.GetTileSafely(k, l).ClearTile();
								WorldGen.PlaceObject(k, l, 50, true, Main.rand.Next(0, 5)); //Books
								break;
							case 8:
								Framing.GetTileSafely(k, l).ClearTile();
								WorldGen.PlaceObject(k, l, 13, true, Main.rand.Next(4, 7));
								break;				
							case 9:
								Framing.GetTileSafely(k, l).ClearTile();
								WorldGen.PlaceObject(k, l, 42, true, 29);
								break;	
							case 10:	
								WorldGen.PlaceObject(k, l, 406); //Chimney
								break;
							case 13:
								Framing.GetTileSafely(k, l).ClearTile();
								WorldGen.Place3x2Wall(k, l, 246, 19);
								break;	
							case 14:
								Framing.GetTileSafely(k, l).ClearTile();
								WorldGen.PlaceTile(k, l, 79, true, false, -1, 24);
								break;
							case 15:
								Framing.GetTileSafely(k, l).ClearTile();
								WorldGen.PlaceTile(k, l, 15, true, false, -1, 30);
								break;
							case 17:
								Framing.GetTileSafely(k, l).ClearTile();
								if (Main.rand.Next(3) == 0)
								{
									WorldGen.PlaceTile(k, l, mod.TileType("FrostTome_Tile"));
								}
								break;
						}
					}
				}
			}	
		}
		public void GenerateIceHouse()
		{
			
			int[,] IceShape1 = new int[,]
			{
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,1,1,0,0,1,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,10,0,1,1,1,0,0,1,0,0,0,0,0,0},
				{0,0,0,0,0,12,3,3,3,3,3,3,0,0,3,11,0,0,0,0,0},
				{0,0,0,0,12,3,3,3,3,3,3,0,0,1,3,3,11,0,0,0,0},
				{0,0,0,12,3,3,3,3,3,3,3,0,0,1,3,3,3,11,0,0,0},
				{0,0,12,3,3,3,3,3,3,3,0,0,0,3,3,3,3,3,11,0,0},
				{0,12,3,3,3,0,0,0,0,0,0,0,0,0,9,0,3,3,3,11,0},
				{0,0,3,2,0,0,13,0,0,0,0,0,0,0,0,0,0,2,3,0,0},
				{0,0,3,2,0,0,0,0,0,0,0,0,0,0,0,0,0,2,3,0,0},
				{0,0,0,2,6,6,17,8,0,0,0,0,0,0,14,0,0,2,0,0,0},
				{0,0,0,2,7,7,7,7,7,7,7,7,7,7,7,7,7,2,0,0,0},
				{0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,4,0,0,0},
				{0,0,0,4,0,0,0,0,0,0,0,0,0,0,0,0,0,4,0,0,0},
				{0,0,0,4,0,0,6,8,6,0,0,1,1,0,0,0,0,4,0,0,0},
				{0,0,0,4,0,0,0,0,0,0,0,1,1,1,1,0,0,4,0,0,0},
				{0,0,0,4,0,0,0,5,0,15,1,1,1,1,1,0,0,4,0,0,0},
				{1,1,1,2,2,2,1,1,2,2,2,1,1,1,2,2,2,2,1,1,1},
				{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
			};
			int[,] IceWalls1 = new int[,]
			{
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,4,4,4,4,4,4,4,4,4,0,0,0,0,0,0},
				{0,0,0,0,0,4,4,4,4,4,4,4,4,4,4,4,0,0,0,0,0},
				{0,0,0,0,4,4,4,4,4,4,4,4,4,4,4,4,4,0,0,0,0},
				{0,0,0,0,3,3,4,4,3,3,3,3,3,3,3,3,3,0,0,0,0},
				{0,0,0,0,3,3,4,4,3,3,3,3,3,3,3,3,3,0,0,0,0},
				{0,0,0,0,3,3,4,4,3,3,3,3,3,3,3,3,3,0,0,0,0},
				{0,0,0,0,3,4,4,4,4,4,4,4,4,4,4,4,3,0,0,0,0},
				{0,0,0,0,3,6,7,7,7,6,3,6,7,7,7,6,3,5,5,5,5},
				{5,5,5,5,3,6,7,7,7,6,3,6,7,7,7,6,3,5,5,5,5},
				{5,5,5,5,3,6,7,7,7,6,3,6,7,7,7,6,3,5,5,5,5},
				{5,5,5,5,3,6,6,6,6,6,3,6,6,6,6,6,3,5,5,5,5},
				{2,5,5,5,3,3,4,4,5,3,3,3,3,3,3,3,3,5,5,5,2},
				{2,2,2,2,3,3,3,3,3,3,3,5,5,5,3,3,3,2,2,2,2},
				{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
				{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
			};
			bool placed = false;
			while (!placed)
			{
				// Select a place in the first 6th of the world
				int towerX = WorldGen.genRand.Next(300, Main.maxTilesX); // from 50 since there's a unaccessible area at the world's borders
				// 50% of choosing the last 6th of the world
				if (WorldGen.genRand.NextBool())
				{
					towerX = Main.maxTilesX - towerX;
				}
				
				int towerY = 0;
				// We go down until we hit a solid tile or go under the world's surface
				while (!WorldGen.SolidTile(towerX, towerY) && towerY <= Main.worldSurface)
				{
					towerY++;
				}
				// If we went under the world's surface, try again
				if (towerY > Main.worldSurface)
				{
					continue;
				}

				Tile tile = Main.tile[towerX, towerY];
				// If the type of the tile we are placing the tower on doesn't match what we want, try again
				if (tile.type != TileID.IceBlock && tile.type != TileID.SnowBlock)
				{
					continue;
				}
				// place the tower
				PlaceIceHouse(towerX, towerY - 9, IceShape1, IceWalls1);
				placed = true;
			}		
		}
		private void PlaceFallenTree(int i, int j, int[,] BlocksArray, int[,] WallsArray) 
		{
			for (int y = 0; y < WallsArray.GetLength(0); y++) 
			{
				for (int x = 0; x < WallsArray.GetLength(1); x++)
				 {
					int k = i - 3 + x;
					int l = j - 6 + y;
					if (WorldGen.InWorld(k, l, 30)){
						Tile tile = Framing.GetTileSafely(k, l);
						switch (WallsArray[y, x]) {
							case 0:
								break;
							case 5:
								WorldGen.KillWall(k, l);
								Framing.GetTileSafely(k, l).ClearTile();
								if (Main.rand.Next(3) == 0)
								{
									WorldGen.PlaceWall(k, l, 78);
								}
								else
								{
									WorldGen.PlaceWall(k, l, 65);
								}
								break;	
							case 6:
								WorldGen.KillWall(k, l);
								Framing.GetTileSafely(k, l).ClearTile();
								if (Main.rand.Next(3) == 0)
								{
									WorldGen.PlaceWall(k, l, 65);
								}
								else
								{
									WorldGen.PlaceWall(k, l, 78);
								}
								break;
							case 7:
								WorldGen.KillWall(k, l);
								Framing.GetTileSafely(k, l).ClearTile();
								break;				
						}
					}
				 }
			}
			for (int y = 0; y < BlocksArray.GetLength(0); y++) 
			{
				for (int x = 0; x < BlocksArray.GetLength(1); x++)
				 {
					int k = i - 3 + x;
					int l = j - 6 + y;
					if (WorldGen.InWorld(k, l, 30)){
						Tile tile = Framing.GetTileSafely(k, l);
						switch (BlocksArray[y, x]) {
							case 1:
								Framing.GetTileSafely(k, l).ClearTile();
								WorldGen.PlaceTile(k, l, 191);
								tile.active(true);
								break;	
							case 2:
								Framing.GetTileSafely(k, l).ClearTile();
								if (Main.rand.Next(3) == 0)
								{
									Framing.GetTileSafely(k, l).ClearTile();
									WorldGen.PlaceTile(k, l, 191);
									tile.active(true);
								}
								else
								{
									WorldGen.PlaceTile(k, l, 0);
									if (!Main.tile[k,l - 1].active())
									{
										WorldGen.PlaceTile(k, l, 2); // Dirt
									}
								}
								tile.active(true);
								break;	
							case 3:
								Framing.GetTileSafely(k, l).ClearTile();
								WorldGen.PlaceTile(k, l, 192);
								tile.active(true);
								break;	
							case 4:
								Framing.GetTileSafely(k, l).ClearTile();
								WorldGen.PlaceTile(k, l, 71, true, false, -1, 0);
								break;	
							case 5:
								Framing.GetTileSafely(k, l).ClearTile();
								WorldGen.PlaceTile(k, l, 227, true, false, -1, 10);
								tile.active(true);	
								break;							
							case 6:
								Framing.GetTileSafely(k, l).ClearTile();
								WorldGen.PlaceTile(k, l, 28);
								tile.active(true);	
								break;							
						}
					}
				 }
			}
		}
		public void GenerateFallenTree()
		{
			int[,] FallenTreeOutline = new int[,]
			{
				{0,0,0,0,0,0,0,0,0,0,0,0,3,3,3,3,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,3,3,3,3,3,3,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,3,3,3,3,3,3,3,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,3,3,3,1,3,3,3,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,3,3,1,1,3,0,0,0,0,0,0,0,0,4,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,3,0,1,0,0,0,0,0,0,0,0,1,1,0,0},
				{0,0,0,0,4,0,0,0,0,0,0,0,0,0,1,1,0,0,4,4,0,4,1,1,0,0,0},
				{0,0,0,2,2,2,0,4,4,0,0,2,2,2,1,1,1,1,1,1,1,1,1,0,1,1,1},
				{0,0,0,1,1,1,2,2,2,1,1,1,1,1,0,0,1,1,1,0,0,0,1,1,1,0,1},
				{0,0,0,0,0,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,6,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,1,1,1,0,0,2,2,2,0,0,1,1,1,1,0,0,5,0,0,0,1,1,0},
				{0,0,0,0,1,1,1,2,2,1,1,1,1,1,2,0,0,0,2,2,1,1,1,1,1,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
			};
			int[,] FallenTreeWalls = new int[,]
			{
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,5,5,7,7,0,5,5,5,0,0,0,0,0},
				{0,0,0,5,5,0,0,0,0,6,5,5,5,5,5,5,5,5,0,5,5,5,5,0,5,0,0},
				{0,0,0,0,5,5,5,5,0,6,6,5,7,7,5,5,5,5,6,6,6,6,6,0,0,0,0},
				{0,0,0,0,6,5,6,6,5,6,6,6,5,5,5,6,7,6,6,6,6,6,5,5,0,0,0},
				{0,0,0,0,5,5,5,6,6,6,0,5,5,5,5,5,7,6,6,5,5,5,5,5,0,0,0},
				{0,0,0,5,5,5,5,5,0,0,0,0,5,5,5,5,5,6,6,5,5,5,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
			};
			bool placed = false;
			while (!placed)
			{
				// Select a place in the first 6th of the world
				int towerX = WorldGen.genRand.Next(300, Main.maxTilesX); // from 50 since there's a unaccessible area at the world's borders
				// 50% of choosing the last 6th of the world
				if (WorldGen.genRand.NextBool())
				{
					towerX = Main.maxTilesX - towerX;
				}
				
				int towerY = 0;
				// We go down until we hit a solid tile or go under the world's surface
				while (!WorldGen.SolidTile(towerX, towerY) && towerY <= Main.worldSurface)
				{
					towerY++;
				}
				// If we went under the world's surface, try again
				if (towerY > Main.worldSurface)
				{
					continue;
				}

				Tile tile = Main.tile[towerX, towerY];
				// If the type of the tile we are placing the tower on doesn't match what we want, try again
				if (tile.type != TileID.Grass)
				{
					continue;
				}
				// place the tower
				PlaceFallenTree(towerX, towerY - 6, FallenTreeOutline, FallenTreeWalls);
				placed = true;
			}
		}
		private void PlaceBoneSpike(int i, int j, int[,] BlocksArray) 
		{
			for (int y = 0; y < BlocksArray.GetLength(0); y++) 
			{
				for (int x = 0; x < BlocksArray.GetLength(1); x++)
				 {
					int k = i - 3 + x;
					int l = j - 6 + y;
					if (WorldGen.InWorld(k, l, 30)){
						Tile tile = Framing.GetTileSafely(k, l);
						switch (BlocksArray[y, x]) {
							case 0:
								break;
							case 1:
								tile.ClearTile();
								tile.type = TileID.FleshBlock;
								tile.active(true);
								break;
							case 2:
								tile.ClearTile();
								tile.type = TileID.BoneBlock;
								tile.active(true);
								break;
							case 3:
								tile.ClearTile();
								tile.type = TileID.BoneBlock;
								tile.slope(2);
								tile.active(true);
								break;
							case 4:
								tile.ClearTile();
								tile.type = TileID.BoneBlock;
								tile.slope(1);
								tile.active(true);
								break;
							case 5:
								tile.ClearTile();
								tile.type = TileID.BoneBlock;
								tile.slope(4);
								tile.active(true);
								break;
							case 6:
								tile.ClearTile();
								tile.type = TileID.Dirt;
								tile.active(true); 
								if (!Main.tile[k,l - 1].active())
								{
									WorldGen.PlaceTile(k, l, TileID.FleshGrass);
								}
								break;
							case 7:
								tile.ClearTile();
								tile.type = TileID.BoneBlock;
								tile.slope(7);
								tile.active(true);
								break;
							case 8:
								tile.ClearTile();
								tile.type = TileID.BoneBlock;
								tile.slope(5);
								tile.active(true);
								break;
							case 9:
								tile.ClearTile();
								WorldGen.PlaceWall(k, l, 77);
								break;
							case 10:
								tile.ClearTile();
								WorldGen.PlaceWall(k, l, 77);
								break;
						}
					}
				}
			}
			for (int y = 0; y < BlocksArray.GetLength(0); y++) 
			{
				for (int x = 0; x < BlocksArray.GetLength(1); x++)
				{
					int k = i - 3 + x;
					int l = j - 6 + y;
					if (WorldGen.InWorld(k, l, 30)){
						Tile tile = Framing.GetTileSafely(k, l);
						switch (BlocksArray[y, x]) {
							case 9:
							tile.ClearTile();
							WorldGen.PlaceChest(k, l, (ushort)mod.TileType("FleshSpikeChest_Tile"), false, 0); 
							break;
						}
					}
				}
			}
		}
		public void GenerateBoneSpike()
		{
			
			int[,] SpikeShape1 = new int[,]
			{
				{0,0,0,0,0,0,0,0,0,0,3,0,0,0,0,0,4,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,3,2,0,0,0,0,0,2,4,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,3,2,2,0,0,0,0,0,2,2,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,3,2,2,0,0,0,0,0,0,2,2,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,2,2,2,0,0,0,0,0,0,2,2,4,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,3,2,2,7,0,0,0,0,0,0,2,2,2,4,0,0,0,0,0,0,0},
				{0,0,0,0,0,3,2,2,2,0,0,0,1,1,0,0,0,2,2,2,0,0,0,0,0,0,0},
				{0,0,0,0,0,2,2,2,2,0,1,1,1,1,1,1,0,2,2,2,2,0,0,0,0,0,0},
				{6,6,1,1,2,2,2,2,2,1,1,1,10,10,1,1,0,0,2,2,2,4,0,0,0,0,0},
				{6,1,1,1,2,2,2,2,1,1,1,1,9,10,1,1,1,1,2,2,2,2,0,0,0,0,0},
				{6,6,1,1,1,2,2,1,1,1,1,1,1,1,1,1,1,1,1,2,2,2,2,1,1,6,6},
				{6,6,6,1,1,1,1,1,1,6,1,1,1,1,1,1,6,6,1,1,1,1,1,6,6,6,6},
				{6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,0},
			};
			int[,] SpikeShape2 = new int[,]
			{
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,0,3,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,0,3,2,0,0,0,0,0,4,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,3,2,2,0,0,0,0,0,2,4,0,0,0,0,0,0},
				{0,0,0,0,0,0,3,2,2,0,0,0,0,0,0,0,2,4,0,0,0,0,0},
				{0,0,0,0,0,0,2,2,2,0,1,1,1,1,0,0,2,2,4,0,0,0,0},
				{0,0,0,0,0,0,2,2,2,2,1,10,10,1,1,0,2,2,2,4,0,1,1},
				{0,0,0,0,0,3,2,2,2,2,1,9,10,1,1,1,2,2,2,2,1,1,1},
				{6,1,1,1,1,2,2,2,1,1,1,1,1,1,1,1,1,1,2,1,1,1,6},
				{6,6,1,1,1,2,1,1,1,6,6,6,6,6,6,6,6,1,1,1,6,6,0},
				{0,6,6,6,1,1,1,1,6,6,6,6,6,6,6,6,6,6,6,6,6,0,0},
				{0,0,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,0,0},
				{0,0,6,6,6,6,0,0,0,0,6,6,6,6,6,6,6,6,0,0,0,0,0},
				
			};
			int[,] SpikeShape3 = new int[,]
			{
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,0,3,0,0,0,0,4,0,0,0,0,0,0,0},
				{0,0,0,0,0,0,3,2,0,0,0,0,2,4,0,0,0,0,0,0},
				{0,0,0,0,3,2,2,7,0,0,0,0,0,2,4,0,0,0,0,0},
				{0,1,1,3,2,2,2,0,0,0,0,0,0,2,2,4,0,0,0,0},
				{6,1,1,2,2,2,2,1,1,6,0,0,0,2,2,2,4,0,1,1},
				{6,6,1,1,2,2,1,1,6,6,6,1,1,2,2,2,2,1,1,1},
				{0,6,6,1,1,1,1,6,6,6,6,1,1,1,1,2,1,1,1,6},
				{0,6,6,6,6,6,6,6,6,6,6,6,6,6,6,1,1,6,6,0},
			};
			
			bool placed = false;
			int counter = 0;
			while (!placed)
			{
				counter++;
				if (counter >= 300)
				{
					placed = true;
				}
				// Select a place in the first 6th of the world
				int towerX = WorldGen.genRand.Next(300, Main.maxTilesX); // from 50 since there's a unaccessible area at the world's borders
				// 50% of choosing the last 6th of the world
				if (WorldGen.genRand.NextBool())
				{
					towerX = Main.maxTilesX - towerX;
				}
				int towerY = 0;
				// We go down until we hit a solid tile or go under the world's surface
				while (!WorldGen.SolidTile(towerX, towerY) && towerY <= Main.worldSurface)
				{
					towerY+= 2;
				}
				// If we went under the world's surface, try again
				if (towerY > Main.worldSurface)
				{
					continue;
				}
				Tile tile = Main.tile[towerX, towerY];
				
				if (tile.type != TileID.FleshGrass)
				{
					continue;
				}
				// place the tower
				if(Main.rand.Next(2) == 0)
				{
					PlaceBoneSpike(towerX, towerY - 4, SpikeShape1);
				}
				else if(Main.rand.Next(3) == 0)
				{
					PlaceBoneSpike(towerX, towerY - 1, SpikeShape3);	
				}
				else 
				{
					PlaceBoneSpike(towerX, towerY - 6, SpikeShape2);
				}
				placed = true;
			}
		}
		private void PlaceCorruptHole(int i, int j, int[,] BlocksArray) 
		{
			for (int y = 0; y < BlocksArray.GetLength(0); y++) 
			{
				for (int x = 0; x < BlocksArray.GetLength(1); x++)
			 	{
					int k = i - 3 + x;
					int l = j - 6 + y;
					if (WorldGen.InWorld(k, l, 30)){
						Tile tile = Framing.GetTileSafely(k, l);
						switch (BlocksArray[y, x]) {
							case 0:
								break;
							case 1:
								tile.ClearTile();
								tile.type = TileID.Ebonstone;
								tile.active(true);
								break;
							case 2:
								tile.ClearTile();
								tile.type = TileID.Ebonsand;
								tile.active(true);
								break;
							case 3:
								tile.ClearTile();
								tile.type = TileID.CorruptSandstone;
								tile.active(true);
								break;
							case 4:
								tile.ClearTile();
								WorldGen.PlaceWall(k, l, 3);
								break;
							case 5:
								tile.ClearTile();
								WorldGen.PlaceWall(k, l, 3);
								break;
						}
					}
				}
			}
			for (int y = 0; y < BlocksArray.GetLength(0); y++) 
			{
				for (int x = 0; x < BlocksArray.GetLength(1); x++)
			 	{
					int k = i - 3 + x;
					int l = j - 6 + y;
					if (WorldGen.InWorld(k, l, 30)){
						Tile tile = Framing.GetTileSafely(k, l);
						switch (BlocksArray[y, x]) {
						case 5:
							WorldGen.PlaceChest(k, l, (ushort)mod.TileType("EbonChest_Tile"), false, 0); 
							break;
						}
					}
				}
			}
		}
		public void GenerateCorruptHole()
		{
			
			int[,] HoleShape1 = new int[,]
			{
				{0,0,0,0,0,3,0,0,0,0,0,0,3,0,0,0,0},
				{0,0,0,3,1,2,1,1,3,3,1,1,2,1,3,0,0},
				{0,3,1,2,2,1,1,1,4,4,1,1,1,2,2,1,3},
				{0,2,2,2,2,2,1,4,4,4,4,1,2,2,2,2,2},
				{2,2,2,2,1,1,1,4,5,4,4,1,1,1,1,2,1},
				{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
				{0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
			};
			int[,] HoleShape2 = new int[,]
			{
				{0,0,0,0,0,0,0,3,0,0,0,0,0,3,0,0,0,0,0,0},
				{0,0,0,0,3,0,1,2,2,2,2,2,2,2,0,3,0,0,0,0},
				{0,0,3,1,2,2,2,2,1,3,3,1,2,2,2,2,0,3,0,0},
				{3,0,2,2,1,1,1,1,1,4,4,1,1,1,1,1,2,2,0,0},
				{2,2,2,2,2,2,2,1,4,4,4,4,1,2,2,2,2,2,2,3},
				{2,2,1,1,1,2,2,1,4,5,4,4,1,2,2,1,1,1,2,2},
				{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
				{0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0},
			};
			int[,] HoleShape3 = new int[,]
			{
				{0,0,0,0,0,3,0,0,0,0,0,0,3,0,0,0,0},
				{0,0,0,3,1,2,1,1,3,3,1,1,2,1,3,0,0},
				{0,3,1,2,2,1,1,1,4,4,1,1,1,2,2,1,3},
				{0,2,2,2,2,2,1,4,4,4,4,1,2,2,2,2,2},
				{2,2,2,2,1,1,1,4,4,4,4,1,1,1,1,2,2},
				{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
				{0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
			};
			bool placed = false;
			int counter = 0;
			while (!placed)
			{
				counter++;
				if (counter >= 300)
				{
					placed = true;
				}
				// Select a place in the first 6th of the world
				int towerX = WorldGen.genRand.Next(300, Main.maxTilesX); // from 50 since there's a unaccessible area at the world's borders
				// 50% of choosing the last 6th of the world
				if (WorldGen.genRand.NextBool())
				{
					towerX = Main.maxTilesX - towerX;
				}
				int towerY = 0;
				// We go down until we hit a solid tile or go under the world's surface
				while (!WorldGen.SolidTile(towerX, towerY) && towerY <= Main.worldSurface)
				{
					towerY+= 2;
				}
				// If we went under the world's surface, try again
				if (towerY > Main.worldSurface)
				{
					continue;
				}
				Tile tile = Main.tile[towerX, towerY];
				
				if (tile.type != TileID.CorruptGrass)
				{
					continue;
				}
				// place the tower
				{
					if(Main.rand.Next(3) == 0)
					{
						PlaceCorruptHole(towerX, towerY + 2, HoleShape3);
					}
					if(Main.rand.Next(3) == 0)
					{
						PlaceCorruptHole(towerX, towerY + 2, HoleShape2);
					}
					else
					{
						PlaceCorruptHole(towerX, towerY + 2, HoleShape1);
					}
				}
				placed = true;
			}
		}
		private void PlaceCampsite(int i, int j, int[,] BlocksArray) 
		{
			for (int y = 0; y < BlocksArray.GetLength(0); y++) 
			{
				for (int x = 0; x < BlocksArray.GetLength(1); x++)
			 	{
					int k = i - 3 + x;
					int l = j - 6 + y;
					if (WorldGen.InWorld(k, l, 30)){
						Tile tile = Framing.GetTileSafely(k, l);
						switch (BlocksArray[y, x]) {
							case 0:
								tile.ClearTile();
								break;
							case 1:
								tile.ClearTile();
								tile.type = 0;
								tile.active(true);
								break;
							case 2:
								tile.ClearTile();
								break;
							case 3:
								tile.ClearTile();
								break;
							case 4:
								tile.ClearTile();
								break;
							case 5: 
								tile.ClearTile();
								break;
							case 6:
								break;
						}
					}
				}
			}
			for (int y = 0; y < BlocksArray.GetLength(0); y++) 
			{
				for (int x = 0; x < BlocksArray.GetLength(1); x++)
			 	{
					int k = i - 3 + x;
					int l = j - 6 + y;
					if (WorldGen.InWorld(k, l, 30)){
						Tile tile = Framing.GetTileSafely(k, l);
						switch (BlocksArray[y, x]) {
							case 0:
								break;
							case 1:
								WorldGen.PlaceTile(k, l, 2);
								break;
							case 2:
								WorldGen.PlaceObject(k, l, 215, true, 0);
								break;	
							case 3:
								WorldGen.PlaceTile(k, l, mod.TileType("TentOpposite"));
								break;
							case 4:
								WorldGen.PlaceObject(k, l, 187, true, 26, 1, -1, -1);
								break;
							case 5:
								WorldGen.PlaceTile(k, l, 28);  // Pot
								tile.active(true);
								break;
						}
					}
				}
			}
		}
		public void GenerateCampsite()
		{
			
			int[,] CampShape1 = new int[,]
			{
				{6,6,6,0,0,0,0,0,0,0,6,6,6,6},
				{6,6,0,0,0,0,0,0,0,0,0,6,6,6},
				{6,0,0,0,0,0,0,0,0,0,0,0,6,6},
				{0,0,0,0,0,0,0,0,0,0,0,0,0,0},
				{0,3,0,0,0,0,2,0,0,0,4,0,5,0},
				{1,1,1,1,1,1,1,1,1,1,1,1,1,1},
			};
			bool placed = false;
			while (!placed)
			{
				// Select a place in the first 6th of the world
				int towerX = WorldGen.genRand.Next(300, Main.maxTilesX); // from 50 since there's a unaccessible area at the world's borders
				// 50% of choosing the last 6th of the world
				if (WorldGen.genRand.NextBool())
				{
					towerX = Main.maxTilesX - towerX;
				}
				int towerY = 0;
				// We go down until we hit a solid tile or go under the world's surface
				while (!WorldGen.SolidTile(towerX, towerY) && towerY <= Main.worldSurface)
				{
					towerY++;
				}
				// If we went under the world's surface, try again
				if (towerY > Main.worldSurface)
				{
					continue;
				}
				Tile tile = Main.tile[towerX, towerY];
				// If the type of the tile we are placing the tower on doesn't match what we want, try again
				if (tile.type != TileID.Dirt && tile.type != TileID.Grass && tile.type != TileID.Stone)
				{
					continue;
				}
				// place the tower
				PlaceCampsite(towerX, towerY, CampShape1);
				placed = true;
			}
		}
		public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
		{
			int GuideIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Granite"));
			if (GuideIndex == -1)
			{
				// Guide pass removed by some other mod.
				return;
			}
			tasks.Insert(GuideIndex + 1, new PassLegacy("Micros1", 
				delegate (GenerationProgress progress)
			{		
				for (int k = 0; k < Main.rand.Next(4, 8); k++)
				{
					GenerateAsteroid();	
				}			
					GenerateIsland();
			}));
			int GenIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Sunflowers"));
			if (GenIndex == -1)
			{
				// Guide pass removed by some other mod.
				return;
			}
			tasks.Insert(GenIndex + 1, new PassLegacy("Micros2", 
				delegate (GenerationProgress progress)
			{		
				if (WorldGen.genRand.Next(2) == 0)
				{
					GenerateGrave();				
				}
				if (WorldGen.genRand.Next(2) == 0)
				{
					GenerateGrave();
				}
				if (WorldGen.genRand.Next(7) == 0)
				{
					GenerateAbandonedHouse();
					GenerateRuin();	
				}
				else if (WorldGen.genRand.Next(3) == 0)
				{
					GenerateRuin();	
				}
				else
				{
					GenerateAbandonedHouse();
				}
		
				if (WorldGen.genRand.Next(2) == 0)
				{
					GeneratePond();	
				}	
				if (WorldGen.genRand.Next(4) == 0)
				{
					GenerateIceHouse();
				}
				else if (WorldGen.genRand.Next(2) == 0)
				{
					GenerateIglooPond();
				}
				GenerateFallenTree();
				if (WorldGen.crimson)
				{
					GenerateBoneSpike();
				}
				else
				{
					GenerateCorruptHole();
				}
				for (int k = 0; k < Main.rand.Next(9, 15); k++)
				{
					GenerateIceCabin();
				}
				for (int k = 0; k < Main.rand.Next(9, 15); k++)
				{
					GenerateGlowGrave();
				}
				for (int k = 0; k < Main.rand.Next(5, 7); k++)
				{
					GenerateGemStash();
				}
				if (WorldGen.genRand.Next(2) == 0)
				{
					GenerateCampsite();				
				}
				if (WorldGen.genRand.Next(3) == 0)
				{
					GenerateCampsite();
				}
				
			}));			
		}
		public override void PostWorldGen()
		{
			int[] mainItem = new int[] { 280, 281, 284, 285, 953, 3084, 3093, 3068, 3069};
			int[] commonItems1 = new int[] {20, 22, 703, 704};
			int[] ammo1 = new int[] {40, 42};
			int[] potions = new int[] {290, 292, 298, 299, 303, 304};
			int[] potionscorrupt = new int[] {2349};
			int[] potionscrim = new int[] {2347,2323};
			int[] recall = new int[] {2350};
			int[] other1 = new int[] {3093, 168};
			int[] other2 = new int[] {31, 8};
			int[] exoticItems = new int[] { 2266, 2267, 2268};
			int[] exoticMain = new int[] { 2278, 2277, 2275}; 	
			int[] crimsonMain = new int[] { mod.ItemType("Spineshot"), mod.ItemType("FleshStick")};	
			int[] corruptMain = new int[] {mod.ItemType("CorruptSpearVariant"), mod.ItemType("Ebonwand")};	
			for (int chestIndex = 0; chestIndex < 1000; chestIndex++)
			{
				Chest chest = Main.chest[chestIndex];
				if (chest != null && Main.tile[chest.x, chest.y].type == mod.TileType("AbandonedChest"))
				{
					chest.item[0].SetDefaults(mainItem[Main.rand.Next(9)], false);
					chest.item[0].stack = 1;
					chest.item[1].SetDefaults(commonItems1[Main.rand.Next(4)], false);
					chest.item[1].stack = WorldGen.genRand.Next(3, 10);
					chest.item[2].SetDefaults(ammo1[Main.rand.Next(2)], false);
					chest.item[2].stack = WorldGen.genRand.Next(20, 50);
					chest.item[3].SetDefaults(potions[Main.rand.Next(6)], false);
					chest.item[3].stack = WorldGen.genRand.Next(2, 3);
					chest.item[4].SetDefaults(recall[Main.rand.Next(1)], false);
					chest.item[4].stack = WorldGen.genRand.Next(2, 3);	
					chest.item[5].SetDefaults(other1[Main.rand.Next(2)], false);
					chest.item[5].stack = WorldGen.genRand.Next(1, 4);	
					chest.item[6].SetDefaults(other1[Main.rand.Next(2)], false);
					chest.item[6].stack = WorldGen.genRand.Next(1, 4);	
					chest.item[7].SetDefaults(72, false);
					chest.item[7].stack = WorldGen.genRand.Next(10, 29);							
					chest.item[8].SetDefaults(mod.ItemType("MapScroll"), false);
					chest.item[8].stack = WorldGen.genRand.Next(0, 2);	
				}
				if (chest != null && Main.tile[chest.x, chest.y].type == mod.TileType("FleshSpikeChest_Tile"))
				{
					chest.item[0].SetDefaults(crimsonMain[Main.rand.Next(2)], false);
					chest.item[0].stack = 1;
					chest.item[1].SetDefaults(commonItems1[Main.rand.Next(4)], false);
					chest.item[1].stack = WorldGen.genRand.Next(3, 10);
					chest.item[2].SetDefaults(ammo1[Main.rand.Next(2)], false);
					chest.item[2].stack = WorldGen.genRand.Next(20, 50);
					chest.item[3].SetDefaults(potionscrim[Main.rand.Next(2)], false);
					chest.item[3].stack = WorldGen.genRand.Next(2, 3);
					chest.item[4].SetDefaults(recall[Main.rand.Next(1)], false);
					chest.item[4].stack = WorldGen.genRand.Next(2, 3);	
					chest.item[5].SetDefaults(other1[Main.rand.Next(2)], false);
					chest.item[5].stack = WorldGen.genRand.Next(1, 4);	
					chest.item[6].SetDefaults(other1[Main.rand.Next(2)], false);
					chest.item[6].stack = WorldGen.genRand.Next(1, 4);	
					chest.item[7].SetDefaults(287, false);
					chest.item[7].stack = WorldGen.genRand.Next(60, 99);
					chest.item[8].SetDefaults(72, false);
					chest.item[8].stack = WorldGen.genRand.Next(10, 29);							
				}
				if (chest != null && Main.tile[chest.x, chest.y].type == mod.TileType("EbonChest_Tile"))
				{
					chest.item[0].SetDefaults(corruptMain[Main.rand.Next(2)], false);
					chest.item[0].stack = 1;
					chest.item[1].SetDefaults(commonItems1[Main.rand.Next(4)], false);
					chest.item[1].stack = WorldGen.genRand.Next(3, 10);
					chest.item[2].SetDefaults(ammo1[Main.rand.Next(2)], false);
					chest.item[2].stack = WorldGen.genRand.Next(20, 50);
					chest.item[3].SetDefaults(potionscorrupt[Main.rand.Next(1)], false);
					chest.item[3].stack = WorldGen.genRand.Next(2, 3);
					chest.item[4].SetDefaults(recall[Main.rand.Next(1)], false);
					chest.item[4].stack = WorldGen.genRand.Next(2, 3);	
					chest.item[5].SetDefaults(other1[Main.rand.Next(2)], false);
					chest.item[5].stack = WorldGen.genRand.Next(1, 4);	
					chest.item[6].SetDefaults(other1[Main.rand.Next(2)], false);
					chest.item[6].stack = WorldGen.genRand.Next(1, 4);	
					chest.item[7].SetDefaults(287, false);
					chest.item[7].stack = WorldGen.genRand.Next(60, 99);
					chest.item[8].SetDefaults(72, false);
					chest.item[8].stack = WorldGen.genRand.Next(10, 29);							
				}
				if (chest != null && Main.tile[chest.x, chest.y].type == mod.TileType("DynastyChestAbandoned"))
				{	
					chest.item[6].SetDefaults(exoticItems[Main.rand.Next(3)], false);
					chest.item[6].stack = WorldGen.genRand.Next(1, 4);			
					chest.item[1].SetDefaults(commonItems1[Main.rand.Next(4)], false);
					chest.item[1].stack = WorldGen.genRand.Next(3, 10);
					chest.item[2].SetDefaults(ammo1[Main.rand.Next(2)], false);
					chest.item[2].stack = WorldGen.genRand.Next(20, 50);
					chest.item[3].SetDefaults(potions[Main.rand.Next(6)], false);
					chest.item[3].stack = WorldGen.genRand.Next(2, 3);
					chest.item[4].SetDefaults(recall[Main.rand.Next(1)], false);
					chest.item[4].stack = WorldGen.genRand.Next(2, 3);	
					chest.item[5].SetDefaults(other1[Main.rand.Next(2)], false);
					chest.item[5].stack = WorldGen.genRand.Next(1, 4);	
					chest.item[7].SetDefaults(72, false);
					chest.item[7].stack = WorldGen.genRand.Next(10, 29);				
					chest.item[0].SetDefaults(exoticMain[Main.rand.Next(3)], false);
					chest.item[0].stack = 1;
					chest.item[8].SetDefaults(2260, false);
					chest.item[8].stack = WorldGen.genRand.Next(50, 100);				
				}
			}
		}				
	}
}