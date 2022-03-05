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
		public override void SetStaticDefaults()
		{

		}
		public override void SetDefaults()
		{
			Projectile.height = 7;
			Projectile.penetrate = 2;
			Projectile.aiStyle = 0;
			Projectile.width = 7;
			Projectile.timeLeft = 720;
			AIType = ProjectileID.Bullet;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.tileCollide = true;
			Projectile.ignoreWater = true;
		}
        public override void AI()
        {
			Projectile.rotation += 0.4f * (float)Projectile.direction;
		}
        public override void Kill(int timeLeft)
        {
			if (Main.rand.NextBool(3))
            {
				Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<Items.StoneSet.StoneDust>());
            }
			Projectile.Kill();
			Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
			SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
		}
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
			SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
			return false;
        }
	}
}