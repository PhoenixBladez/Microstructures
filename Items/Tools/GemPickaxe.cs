using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Microstructures.Items.Tools
{
    public class GemPickaxe : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Brilliant Harvester");
            Tooltip.SetDefault("Mining stone may also yield gems and ores\nCan mine Demonite and Crimtane");
		}


        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.value = 12000;
            item.rare = 2;

            item.pick = 65;

            item.damage = 8;
            item.knockBack = 3;

            item.useStyle = 1;
            item.useTime = 18;
            item.useAnimation = 22; 

            item.melee = true;
            item.useTurn = true;
            item.autoReuse = true;

            item.UseSound = SoundID.Item1;
        }
 		public override void HoldItem(Player player)
		{
			player.GetModPlayer<MyPlayer>().gemPickaxe = true;
        }       
    }
}
