using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using ANB.Projectiles;
using ANB.Items;

namespace ANB.Projectiles
{
	public class StoneSwordProj : ModProjectile
	{
		//public override string Texture => "Terraria/Images/NPC_" + NPCID.Pixie; placeholder. because every single time i recompile the mod it tells me about missing texture
		int style;
		int Timer = 0;
		NPC target = null;
		public override void SetDefaults()
		{

			Projectile.height = 7;
			Projectile.penetrate = 2;
			Projectile.aiStyle = 0;
			Projectile.width = 7;
			Projectile.timeLeft = 80;
			AIType = ProjectileID.Bullet;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.tileCollide = true;
			AIType = 16;
			Projectile.ignoreWater = true;
		}
        public override void PostAI()
		{
			Projectile.rotation += 0.4f * (float)Projectile.direction;
			Projectile.velocity += new Vector2(0,0.05f);//a little grabity won't hurt
			base.PostAI();
        }
        public override void Kill(int timeLeft)
        {
			if (timeLeft <= 70)
			{
				for (int i = 0; i < 3; i++)
				{

					Vector2 s = Vector2.One.RotatedByRandom(MathHelper.TwoPi) * 10;
					Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 23, s.X, s.Y);
					s = s.RotatedByRandom(MathHelper.TwoPi) * 10;
					Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<Items.StoneSet.StoneDust>(), s.X, s.Y);
				}
				SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
			}
			base.Kill(timeLeft);
		}
	}
}