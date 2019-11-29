using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Microstructures.Tiles
{
	public class WallScroll_Tile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileLavaDeath[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.StyleWrapLimit = 36;
			TileObjectData.addTile(Type);
			dustType = 7;
			disableSmartCursor = true;
			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Display Scroll");
			AddMapEntry(new Color(125, 125, 125), name);
		}
		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Terraria.Item.NewItem(i * 16, j * 16, 64, 32, mod.TileType("WallScroll"));
        }
	}
}