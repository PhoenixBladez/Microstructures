using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Microstructures.Tiles
{
	public class AsteroidBlock_Tile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[Type] = true;
			Main.tileBlockLight[Type] = true;
			drop = mod.ItemType("AsteroidBlock");
			AddMapEntry(new Color(200, 200, 200));
			dustType = 0;
			soundType = 21;
            minPick = 50;
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}
	}
}