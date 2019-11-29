using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Microstructures.Items.Weapons 
{    
    public class CorruptSpearVariant : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wormfang");
        }


        public override void SetDefaults()
        {
            item.useStyle = 5;
            item.width = 24;
            item.height = 24;
            item.noUseGraphic = true;
            item.UseSound = SoundID.Item1;
            item.melee = true;
            item.noMelee = true;
            item.useAnimation = 27;
            item.useTime = 27;
            item.shootSpeed = 3f;
            item.knockBack = 5.4f;
            item.damage = 12;
            item.value = Item.sellPrice(0, 0, 60, 0);
            item.rare = 1;
            item.shoot = mod.ProjectileType("CorruptSpearProj");
        }
    }
}
