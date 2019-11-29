using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Microstructures.Projectiles
{

	public class LunarStaff_SunStyle : ModProjectile
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
			projectile.timeLeft = 180;
			projectile.ignoreWater = true;
		}
		int timer;
        int counter;
		public override void AI()
		{
			projectile.rotation = projectile.velocity.ToRotation() + 1.57f;
			counter++;
			if (counter >= 1440)
			{
				counter = -1440;
			}
			for (int i = 0; i < 10; i++)
			{
				float x = projectile.Center.X - projectile.velocity.X / 10f * (float)i;
				float y = projectile.Center.Y - projectile.velocity.Y / 10f * (float)i;
				
				int num = Dust.NewDust(projectile.Center + new Vector2(0, (float)Math.Cos(counter/8.2f)*9.2f).RotatedBy(projectile.rotation), 6, 6, 6, 0f, 0f, 0, default(Color), 1f);
				Main.dust[num].velocity *= .1f;
				Main.dust[num].scale *= 1.2f;				
				Main.dust[num].noGravity = true;
			
			}
            projectile.localAI[0] += 1f;
			if (projectile.localAI[0] == 40f)
			{
				projectile.localAI[0] = 0f;
				for (int j = 0; j < 12; j++)
				{
					Vector2 vector2 = Vector2.UnitX * -(float)projectile.width / 2f;
					vector2 += -Utils.RotatedBy(Vector2.UnitY, (double)((float)j * 3.14159274f / 6f), default(Vector2)) * new Vector2(8f, 16f);
					vector2 = Utils.RotatedBy(vector2, (double)(projectile.rotation - 1.57079637f), default(Vector2));
					int num8 = Dust.NewDust(projectile.Center, 0, 0, 6, 0f, 0f, 160, default(Color), 1f);
					Main.dust[num8].scale = 1.1f;
					Main.dust[num8].noGravity = true;
					Main.dust[num8].position = projectile.Center + vector2;
					Main.dust[num8].velocity = projectile.velocity * 0.1f;
					Main.dust[num8].velocity = Vector2.Normalize(projectile.Center - projectile.velocity * 3f - Main.dust[num8].position) * 1.25f;
				}
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, mod.ProjectileType("Explosion"), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);

               for (int j = 0; j < 12; j++)
				{
					Vector2 vector2 = Vector2.UnitX * -(float)projectile.width / 2f;
					vector2 += -Utils.RotatedBy(Vector2.UnitY, (double)((float)j * 3.14159274f / 6f), default(Vector2)) * new Vector2(20f, 26f);
					vector2 = Utils.RotatedBy(vector2, (double)(projectile.rotation - 1.57079637f), default(Vector2));
					int num8 = Dust.NewDust(projectile.Center, 0, 0, 6, 0f, 0f, 160, default(Color), 1f);
					Main.dust[num8].scale = 1.1f;
					Main.dust[num8].noGravity = true;
					Main.dust[num8].position = projectile.Center + vector2;
					Main.dust[num8].velocity = projectile.velocity * 10f;
					Main.dust[num8].velocity = Vector2.Normalize(projectile.Center - projectile.velocity * 3f - Main.dust[num8].position) * 1.25f;
				}
                for (int j = 0; j < 12; j++)
				{
					Vector2 vector2 = Vector2.UnitX * -(float)projectile.width / 2f;
					vector2 += -Utils.RotatedBy(Vector2.UnitY, (double)((float)j * 3.14159274f / 6f), default(Vector2)) * new Vector2(40f, 46f);
					vector2 = Utils.RotatedBy(vector2, (double)(projectile.rotation - 1.57079637f), default(Vector2));
					int num8 = Dust.NewDust(projectile.Center, 0, 0, 6, 0f, 0f, 160, default(Color), 1f);
					Main.dust[num8].scale = 1.1f;
					Main.dust[num8].noGravity = true;
					Main.dust[num8].position = projectile.Center + vector2;
					Main.dust[num8].velocity = projectile.velocity * 10f;
					Main.dust[num8].velocity = Vector2.Normalize(projectile.Center - projectile.velocity * 3f - Main.dust[num8].position) * 1.25f;
				}
               for (int j = 0; j < 24; j++)
				{
					Vector2 vector2 = Vector2.UnitX * -(float)projectile.width / 2f;
					vector2 += -Utils.RotatedBy(Vector2.UnitY, (double)((float)j * 3.14159274f / 12f), default(Vector2)) * new Vector2(0f, 86f);
					vector2 = Utils.RotatedBy(vector2, (double)(projectile.rotation - 1.57079637f), default(Vector2));
					int num8 = Dust.NewDust(projectile.Center, 0, 0, 6, 0f, 0f, 160, default(Color), 1f);
					Main.dust[num8].scale = 1.1f;
					Main.dust[num8].noGravity = true;
					Main.dust[num8].position = projectile.Center + vector2;
					Main.dust[num8].velocity = projectile.velocity * 10f;
					Main.dust[num8].velocity = Vector2.Normalize(projectile.Center - projectile.velocity * 3f - Main.dust[num8].position) * 1.25f;
				}
               for (int j = 0; j < 24; j++)
				{
					Vector2 vector2 = Vector2.UnitX * -(float)projectile.width / 2f;
					vector2 += -Utils.RotatedBy(Vector2.UnitY, (double)((float)j * 3.14159274f / 12f), default(Vector2)) * new Vector2(86f, 0f);
					vector2 = Utils.RotatedBy(vector2, (double)(projectile.rotation - 1.57079637f), default(Vector2));
					int num8 = Dust.NewDust(projectile.Center, 0, 0, 6, 0f, 0f, 160, default(Color), 1f);
					Main.dust[num8].scale = 1.1f;
					Main.dust[num8].noGravity = true;
					Main.dust[num8].position = projectile.Center + vector2;
					Main.dust[num8].velocity = projectile.velocity * 10f;
					Main.dust[num8].velocity = Vector2.Normalize(projectile.Center - projectile.velocity * 3f - Main.dust[num8].position) * 1.25f;
				}
            	Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 74);
			}
		}
		public override Color? GetAlpha(Color lightColor)
		{
			return Color.White;
		}
		public override void Kill(int timeLeft)
		{
			Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 74);
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, mod.ProjectileType("Explosion"), (int)(projectile.damage * 1.4f), projectile.knockBack, projectile.owner, 0f, 0f);

               for (int j = 0; j < 12; j++)
				{
					Vector2 vector2 = Vector2.UnitX * -(float)projectile.width / 2f;
					vector2 += -Utils.RotatedBy(Vector2.UnitY, (double)((float)j * 3.14159274f / 6f), default(Vector2)) * new Vector2(30f, 36f);
					vector2 = Utils.RotatedBy(vector2, (double)(projectile.rotation - 1.57079637f), default(Vector2));
					int num8 = Dust.NewDust(projectile.Center, 0, 0, 6, 0f, 0f, 160, default(Color), 1f);
					Main.dust[num8].scale = 1.1f;
					Main.dust[num8].noGravity = true;
					Main.dust[num8].position = projectile.Center + vector2;
					Main.dust[num8].velocity = projectile.velocity * 10f;
					Main.dust[num8].velocity = Vector2.Normalize(projectile.Center - projectile.velocity * 3f - Main.dust[num8].position) * 1.25f;
				}
                for (int j = 0; j < 12; j++)
				{
					Vector2 vector2 = Vector2.UnitX * -(float)projectile.width / 2f;
					vector2 += -Utils.RotatedBy(Vector2.UnitY, (double)((float)j * 3.14159274f / 6f), default(Vector2)) * new Vector2(60f, 66f);
					vector2 = Utils.RotatedBy(vector2, (double)(projectile.rotation - 1.57079637f), default(Vector2));
					int num8 = Dust.NewDust(projectile.Center, 0, 0, 6, 0f, 0f, 160, default(Color), 1f);
					Main.dust[num8].scale = 1.1f;
					Main.dust[num8].noGravity = true;
					Main.dust[num8].position = projectile.Center + vector2;
					Main.dust[num8].velocity = projectile.velocity * 10f;
					Main.dust[num8].velocity = Vector2.Normalize(projectile.Center - projectile.velocity * 3f - Main.dust[num8].position) * 1.25f;
				}
               for (int j = 0; j < 24; j++)
				{
					Vector2 vector2 = Vector2.UnitX * -(float)projectile.width / 2f;
					vector2 += -Utils.RotatedBy(Vector2.UnitY, (double)((float)j * 3.14159274f / 12f), default(Vector2)) * new Vector2(0f, 126f);
					vector2 = Utils.RotatedBy(vector2, (double)(projectile.rotation - 1.57079637f), default(Vector2));
					int num8 = Dust.NewDust(projectile.Center, 0, 0, 6, 0f, 0f, 160, default(Color), 1f);
					Main.dust[num8].scale = 1.1f;
					Main.dust[num8].noGravity = true;
					Main.dust[num8].position = projectile.Center + vector2;
					Main.dust[num8].velocity = projectile.velocity * 10f;
					Main.dust[num8].velocity = Vector2.Normalize(projectile.Center - projectile.velocity * 3f - Main.dust[num8].position) * 1.25f;
				}
               for (int j = 0; j < 24; j++)
				{
					Vector2 vector2 = Vector2.UnitX * -(float)projectile.width / 2f;
					vector2 += -Utils.RotatedBy(Vector2.UnitY, (double)((float)j * 3.14159274f / 12f), default(Vector2)) * new Vector2(126f, 0f);
					vector2 = Utils.RotatedBy(vector2, (double)(projectile.rotation - 1.57079637f), default(Vector2));
					int num8 = Dust.NewDust(projectile.Center, 0, 0, 6, 0f, 0f, 160, default(Color), 1f);
					Main.dust[num8].scale = 1.1f;
					Main.dust[num8].noGravity = true;
					Main.dust[num8].position = projectile.Center + vector2;
					Main.dust[num8].velocity = projectile.velocity * 10f;
					Main.dust[num8].velocity = Vector2.Normalize(projectile.Center - projectile.velocity * 3f - Main.dust[num8].position) * 1.25f;
				}


			for (int g = 0; g < 2; g++)
			{
				int goreIndex = Gore.NewGore(new Vector2(projectile.position.X + (float)(projectile.width / 2) - 24f, projectile.position.Y + (float)(projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[goreIndex].scale = .5f;
				Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
				Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
				goreIndex = Gore.NewGore(new Vector2(projectile.position.X + (float)(projectile.width / 2) - 24f, projectile.position.Y + (float)(projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[goreIndex].scale = .65f;
				Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
				Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
				goreIndex = Gore.NewGore(new Vector2(projectile.position.X + (float)(projectile.width / 2) - 24f, projectile.position.Y + (float)(projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[goreIndex].scale = .55f;
				Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
				Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
				goreIndex = Gore.NewGore(new Vector2(projectile.position.X + (float)(projectile.width / 2) - 24f, projectile.position.Y + (float)(projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[goreIndex].scale = .25f;
				Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
				Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
			}
		}

	}
}