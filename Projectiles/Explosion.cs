using System;

using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Microstructures.Projectiles
{
	class Explosion : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Explosion");
		}

		public override void SetDefaults()
		{
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.penetrate = 4;
			projectile.timeLeft = 20;
			projectile.height = 120;
			projectile.width = 120;

			projectile.alpha = 255;
			aiType = ProjectileID.Bullet;
		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(BuffID.OnFire, 120);
		}
	}
}
