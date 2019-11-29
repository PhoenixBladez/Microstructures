using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Microstructures.Projectiles
{

	public class LunarStaff_MoonStyle : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Twilight Meridian");
		}

		public override void SetDefaults()
		{
			projectile.width = 20;
			projectile.height = 20;
			projectile.friendly = true;
			projectile.ranged = true;
            projectile.scale = .5f;
			projectile.tileCollide = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 1440;
			projectile.extraUpdates = 2;
            projectile.alpha = 255;
			projectile.ignoreWater = true;
		}

        int counter;
        int d = 0;
		public override void AI()
		{
			counter++;
			if (counter >= 2880)
			{
				counter = -2880;
			}
			for (int f = 0; f < 10; f++)
			{
				float x = projectile.Center.X - projectile.velocity.X / 10f * (float)f;
				float y = projectile.Center.Y - projectile.velocity.Y / 10f * (float)f;
				
				int num = Dust.NewDust(projectile.Center - new Vector2(0, (float)Math.Cos(counter/8.2f)*19.2f).RotatedBy(projectile.rotation), 6, 6, 68, 0f, 0f, 0, default(Color), 1f);
				Main.dust[num].velocity *= .1f;
				Main.dust[num].scale *= .7f;				
				Main.dust[num].noGravity = true;
			
			}
            for (int f = 0; f < 10; f++)
			{
				float x = projectile.Center.X - projectile.velocity.X / 10f * (float)f;
				float y = projectile.Center.Y - projectile.velocity.Y / 10f * (float)f;
				
				int num = Dust.NewDust(projectile.Center - new Vector2(((float)Math.Cos(counter/8.2f)*19.2f), (float)Math.Sin(counter/8.2f)*19.2f), 6, 6, 68, 0f, 0f, 0, default(Color), 1f);
				Main.dust[num].velocity *= .1f;
				Main.dust[num].scale *= .7f;				
				Main.dust[num].noGravity = true;
			
			}
		}
		public override Color? GetAlpha(Color lightColor)
		{
			return Color.White;
		}
		public override void Kill(int timeLeft)
		{
			Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 9);
		    for (int k = 0; k < 2; k++)
			{
				Vector2 vector2_1 = new Vector2((float)((double)projectile.position.X + (double)projectile.width * 0.5 + (double)(Main.rand.Next(201) * -projectile.direction) + ((double)Main.mouseX + (double)Main.screenPosition.X - (double)projectile.position.X)), (float)((double)projectile.position.Y + (double)projectile.height * 0.5 - 600.0));   //this defines the projectile width, direction and position
                vector2_1.X = (float)(((double)vector2_1.X + (double)projectile.Center.X) / 2.0) + (float)Main.rand.Next(-200, 201);
                vector2_1.Y -= (float)(100);
                float num12 = Main.rand.Next(-30, 30);
                float num13 = 100;
                if ((double)num13 < 0.0) num13 *= -1f;
                if ((double)num13 < 20.0) num13 = 20f;
                float num14 = (float)Math.Sqrt((double)num12 * (double)num12 + (double)num13 * (double)num13);
                float num15 = 10 / num14;
                float num16 = num12 * num15;
                float num17 = num13 * num15;
                float SpeedX = num16 + (float)Main.rand.Next(-40, 41) * 0.1f;  //this defines the projectile X position speed and randomnes
                float SpeedY = num17 + (float)Main.rand.Next(0, 41) * 1f;  //this defines the projectile Y position speed and randomnes
                int proj = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y + Main.rand.Next(-800, -600), SpeedX, SpeedY, mod.ProjectileType("LunarStaff_Star"), projectile.damage, 2, Main.myPlayer, 0.0f, 1);
			    Main.projectile[proj].friendly = true;
			    Main.projectile[proj].hostile = false;
			    Main.projectile[proj].magic = true;
			}
			for (int i = 0; i < 16; i++)
			{
				int num = Dust.NewDust(projectile.position, projectile.width, projectile.height, 68, 0f, -2f, 0, default(Color), .8f);
				Main.dust[num].noGravity = true;
				Dust expr_62_cp_0 = Main.dust[num];
				expr_62_cp_0.position.X = expr_62_cp_0.position.X + ((float)(Main.rand.Next(-50, 51) / 20) - 1.5f);
				Dust expr_92_cp_0 = Main.dust[num];
				expr_92_cp_0.position.Y = expr_92_cp_0.position.Y + ((float)(Main.rand.Next(-50, 51) / 20) - 1.5f);
				if (Main.dust[num].position != projectile.Center)
				{
					Main.dust[num].velocity = projectile.DirectionTo(Main.dust[num].position) * 6f;
				}
			}
		}

	}
}