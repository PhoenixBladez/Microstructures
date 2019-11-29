﻿using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Microstructures.Projectiles
{
	public class LunarStaff_Star : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Twilight Star");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;   
		}

		public override void SetDefaults()
		{
			projectile.width = 18;
			projectile.height = 18;
			projectile.timeLeft = 1800;
			projectile.ranged = true;
			projectile.friendly = true;
			projectile.penetrate = 2;
		}
		public override bool PreAI()
		{
			projectile.rotation += .2f;
			Lighting.AddLight(projectile.position, .055f, .054f, .223f);
			if (projectile.ai[0] == 0)
			{
				projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2;
				int num = 5;
				for (int k = 0; k < 5; k++)
					{
						int index2 = Dust.NewDust(projectile.position, 1, 1, 68, 0.0f, 0.0f, 0, new Color(), 1f);
						Main.dust[index2].position = projectile.Center - projectile.velocity / num * (float)k;
						Main.dust[index2].scale = .8f * projectile.scale;
						Main.dust[index2].velocity *= 0f;
						Main.dust[index2].noGravity = true;
						Main.dust[index2].noLight = false;	
					}	
			}
            return false;
		}
		public override void Kill(int timeLeft)
		{
        	Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 14);
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
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }        
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
	}
}
