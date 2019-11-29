﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Microstructures.Tiles
{
	public class GTile : GlobalTile
	{
		int[] TileArray2 = {0, 3, 185, 186, 187, 71, 28};
		public int tremorItem = 0;
		public override void KillTile (int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem)
		{
			Player player = Main.LocalPlayer;
			MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
			if (type == 1 || type == 25 || type == 117 || type == 203 || type == 57)
			{
				if (Main.rand.Next(50) == 1 && modPlayer.gemPickaxe)
				{
					tremorItem = Main.rand.Next(new int[]{11, 12, 13, 14, 699, 700, 701, 702, 999, 182, 178, 179, 177, 180, 181});
					if (Main.hardMode)
					{
						tremorItem = Main.rand.Next(new int[]{11, 12, 13, 14, 699, 700, 701, 702, 999, 182, 178, 179, 177, 180, 181, 364, 365, 366, 1104, 1105, 1106});
					}					
					Item.NewItem(i * 16, j * 16, 64, 48, tremorItem, Main.rand.Next(0, 2));
				}
			}
			if (player.HasItem(mod.ItemType("Spineshot")))
			{
				if (type == 3 || type == 24 || type == 61 || type == 71 || type == 110 || type == 201)
				{
					Item.NewItem(i * 16, j * 16, 64, 48, ItemID.Seed, Main.rand.Next(1, 3));
				}
			} 
		}
	}
}