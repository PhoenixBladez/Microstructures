using Terraria;
using Terraria.Utilities;
using System;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Terraria.ModLoader.IO;
using Terraria.ModLoader;
using Terraria.DataStructures;
using System.Collections.Generic;

namespace Microstructures.Items.Weapons
{
    public class LunarStaff : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Twilight Meridian");
            Tooltip.SetDefault("Switches between Sun Style and Moon Style based on the time of day");
        }
    	public override bool CloneNewInstances => true;


        public override void SetDefaults()
        {
            item.damage = 18;
            Item.staff[item.type] = true;
            item.noMelee = true;
            item.magic = true;
            item.width = 34;
            item.height = 34;
            item.useTime = 34;
            item.mana = 15;
            item.useAnimation = 34;
            item.useStyle = 5;
            item.knockBack = 2f;
            item.value = Terraria.Item.sellPrice(0, 2, 0, 0);
            item.rare = 2;
            item.UseSound = SoundID.Item20;
            item.autoReuse = false;
            item.shootSpeed = 14;
            item.shoot = mod.ProjectileType("LunarStaff_MoonStyle");
        }
        public override void HoldItem (Player player)
         {
             if (Main.dayTime)
			{
				MicrostructuresGlowmask.AddGlowMask(item.type, "Microstructures/Items/Weapons/LunarStaff_SunGlow");
			    item.shootSpeed = 6;
            }
			else
			{
				MicrostructuresGlowmask.AddGlowMask(item.type, "Microstructures/Items/Weapons/LunarStaff_MoonGlow");
			    item.shootSpeed = 12;
            }
         }
        public override bool Shoot(Player player,ref Vector2 position,ref float speedX,ref float speedY,ref int type,ref int damage,ref float knockBack)
		{
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 50f;
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}
            if (Main.dayTime)
            {
                type = mod.ProjectileType("LunarStaff_SunStyle");
            }
            else
            {
                type = mod.ProjectileType("LunarStaff_MoonStyle");
            }
            return true;	
        }
        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            if (Main.dayTime)
            {
			Texture2D texture;
			texture = Main.itemTexture[item.type];
			spriteBatch.Draw
			(
				ModContent.GetTexture("Microstructures/Items/Weapons/LunarStaff_SunGlow"),
				new Vector2
				(
					item.position.X - Main.screenPosition.X + item.width * 0.5f,
					item.position.Y - Main.screenPosition.Y + item.height - texture.Height * 0.5f + 2f
				),
				new Rectangle(0, 0, texture.Width, texture.Height),
				Color.White,
				0,
				texture.Size() * 0.5f,
				scale, 
				SpriteEffects.None, 
				0f
			);
            }
            else
            {
        		Texture2D texture;
			    texture = Main.itemTexture[item.type];
			    spriteBatch.Draw
			    (
				    ModContent.GetTexture("Microstructures/Items/Weapons/LunarStaff_MoonGlow"),
				    new Vector2
				    (
					    item.position.X - Main.screenPosition.X + item.width * 0.5f,
					    item.position.Y - Main.screenPosition.Y + item.height - texture.Height * 0.5f + 2f
				    ),
				    new Rectangle(0, 0, texture.Width, texture.Height),
				    Color.White,
				    0,
				    texture.Size() * 0.5f,
				    scale, 
				    SpriteEffects.None, 
				    0f
		     	);
            }
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
            if (Main.dayTime)
            {
		        var line2 = new TooltipLine(mod, "", "Sun Style: Shoots a slow moving blast that creats a nova on impact");     
                tooltips.Add(line2);                
            }
            else
            {
                var line2 = new TooltipLine(mod, "", "Moon Style: Shoots a fast moving wisp that rains down stars on impact");     
                tooltips.Add(line2); 
            }
        }
    }
}
