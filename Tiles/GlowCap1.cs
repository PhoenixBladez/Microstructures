using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Microstructures.Tiles
{
    public class GlowCap1 : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            //TileObjectData.addTile(Type);
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            Main.tileSolid[Type] = false;
            Main.tileMergeDirt[Type] = true;
			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Glowcap");
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            AddMapEntry(new Color(100, 100, 100), name);
			dustType = 187;
            TileObjectData.newTile.CoordinateHeights = new int[]
            {
                16,
                16
            };

            TileObjectData.addTile(Type);
        }
		
        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
			Main.PlaySound(3, i * 16, j * 16, 19);
            for (int k = 0; k < Main.rand.Next(2, 4); k++)
            {
                Item.NewItem(i * 16, j * 16, 48, 48, ItemID.GlowingMushroom);
            } 
        }
        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
		{
			r = .108f;
			g = .456f;
			b = .510f;
		}
        int counter;
        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
		{
			Tile tile = Main.tile[i, j];
			Vector2 zero = new Vector2((float)Main.offScreenRange, (float)Main.offScreenRange);
			if (Main.drawToScreen)
			{
				zero = Vector2.Zero;
			}
			int height = (tile.frameY == 54) ? 18 : 16;
            if (counter >= 255)
            {
                counter--;
            }
            if (counter <= 0)
            {
                counter++;
            }
			Main.spriteBatch.Draw(mod.GetTexture("Tiles/GlowCap1_Glow"), new Vector2((float)(i * 16 - (int)Main.screenPosition.X), (float)(j * 16 - (int)Main.screenPosition.Y)) + zero, new Rectangle?(new Rectangle((int)tile.frameX, (int)tile.frameY, 16, height)), new Color(150, 150, 150, counter), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
		}
    }
}
