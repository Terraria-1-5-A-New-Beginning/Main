using System;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;

namespace ANB.Projectiles.StarSwordProj
{
    public class StarProj : ModProjectile
    {
        public override void SetDefaults()
        {
			Projectile.damage = 20; //<---- bad
            Projectile.height = 6; //placeholder
            Projectile.width = 6; //placeholder
            Projectile.knockBack = 2;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
			Projectile.aiStyle = 0;
			Projectile.light = 1f;
			Projectile.friendly = true;
			Projectile.hostile = false;
		}

        public override void AI()
        {
			float maxDetectRadius = 600f;
			float projSpeed = 6f;

			//Vector2 target1 = Main.rand.NextVector2CircularEdge(60f, 60f);
			//Projectile.velocity = target1 * projSpeed;

			Projectile.rotation = Projectile.velocity.ToRotation();
			
			NPC closestNPC = FindClosestNPC(maxDetectRadius);
			Projectile.ai[0]++;
			if (Projectile.ai[0] > 60)
            {
				if (closestNPC == null || !closestNPC.active)
				{
					Projectile.Kill();
					return;
				}
				else
				{
					Projectile.velocity = (closestNPC.Center - Projectile.Center).SafeNormalize(Vector2.UnitX) * projSpeed;
					//Projectile.ai[0] = 0;
				}
            }
            else
            {
				Projectile.ai[0]++;
            }
			Projectile.rotation = Projectile.velocity.ToRotation();


			base.AI();
        }
		public NPC FindClosestNPC(float maxDetectDistance)
		{
			NPC closestNPC = null;

			float sqrMaxDetectDistance = maxDetectDistance * maxDetectDistance;

			for (int k = 0; k < Main.maxNPCs; k++)
			{
				NPC target = Main.npc[k];
				if (target.CanBeChasedBy())
				{
					float sqrDistanceToTarget = Vector2.DistanceSquared(target.Center, Projectile.Center);
					if (sqrDistanceToTarget < sqrMaxDetectDistance)
					{
						sqrMaxDetectDistance = sqrDistanceToTarget;
						closestNPC = target;
					}
				}
			}

			return closestNPC;
		}
	}
}