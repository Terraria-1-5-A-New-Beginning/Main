using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace ANB.Projectiles
{
	public class StoneSwordProj : ModProjectile
	{
		//public override string Texture => "Terraria/Images/NPC_" + NPCID.Pixie; placeholder. because every single time i recompile the mod it tells me about missing texture
		int style;
		int Timer = 0;
		public override void SetStaticDefaults()
		{

		}
		public override void SetDefaults()
		{
			Projectile.height = 7;
			Projectile.damage = 15;
			Projectile.penetrate = 1;
			AIType = ProjectileID.ChlorophyteBullet;
			Projectile.width = 7;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
		}
		
	}
}