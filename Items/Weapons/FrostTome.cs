using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Microstructures.Items.Weapons
{
	public class FrostTome : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Shiverbrink");
			Tooltip.SetDefault("Rains ice spikes at the cursor position");
		}


		public override void SetDefaults()
		{
			item.damage = 16;
			item.magic = true;
			item.mana = 6;
			item.width = 2;
			item.height = 2;
			item.useTime = 27;
			item.useAnimation = 27;
			item.useStyle = 5;
			Item.staff[item.type] = true;
			item.noMelee = true; 
			item.knockBack = 1;
            item.value = Terraria.Item.sellPrice(0, 1, 0, 0);
            item.rare = 2;
			item.UseSound = SoundID.Item8;
			item.autoReuse = true;
			item.shoot = 349;
			item.shootSpeed = 6f;
		}
        public override Vector2? HoldoutOffset() => new Vector2(-10,0);
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
    	    Vector2 mouse = Main.MouseWorld;
			{
			    if(Main.myPlayer == player.whoAmI)
                {
				    int p = Projectile.NewProjectile(mouse.X + Main.rand.Next(-10, 10), mouse.Y, 0, Main.rand.Next(60,75), 337, damage, knockBack, player.whoAmI);
                    Main.projectile[p].hostile = false;
                    Main.projectile[p].friendly = true;
                    Main.projectile[p].penetrate = 1;
                    Main.projectile[p].width = 10;
                    Main.projectile[p].height = 18;

                    if (Main.rand.Next(4) == 0)
                    {
                        int p1 = Projectile.NewProjectile(mouse.X + Main.rand.Next(-10, 10), mouse.Y + Main.rand.Next(-10, 10), 0, Main.rand.Next(30,40), 337, damage, knockBack, player.whoAmI);
                        Main.projectile[p1].hostile = false;
                        Main.projectile[p1].friendly = true;
                        Main.projectile[p1].penetrate = 1;
                        Main.projectile[p1].width = 10;
                        Main.projectile[p1].height = 18;
                    }

                }
			}
             for (int k = 0; k < 10; k++)
			{
                int dust = Dust.NewDust(new Vector2(mouse.X, mouse.Y), 40, 40, 68);
				Main.dust[dust].velocity *= -1f;
				Main.dust[dust].noGravity = true;
				Vector2 vector2_1 = new Vector2((float) Main.rand.Next(-100, 101), (float) Main.rand.Next(-100, 101));
				vector2_1.Normalize();
				Vector2 vector2_2 = vector2_1 * ((float) Main.rand.Next(50, 100) * 0.04f);
				Main.dust[dust].velocity = vector2_2;
				vector2_2.Normalize();
				Vector2 vector2_3 = vector2_2 * 34f;
				Main.dust[dust].position = new Vector2(mouse.X, mouse.Y) - vector2_3;
			}
			return false;
        }
	}
}