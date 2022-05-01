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
            Projectile.height = 6; //placeholder
            Projectile.width = 6; //placeholder
            Projectile.knockBack = 2;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
			Projectile.aiStyle = 0;
			Projectile.light = 1f;
        }

        public override void AI()
        {
			float maxDetectRadius = 6f;
			float projSpeed = 6f;

			Vector2 target1 = Main.rand.NextVector2CircularEdge(1f, 1f);
			Projectile.velocity = target1 * projSpeed;
			Projectile.rotation = Projectile.velocity.ToRotation();
			
			NPC closestNPC = FindClosestNPC(maxDetectRadius);
			if (closestNPC == null)
				return;

			Projectile.ai[0]++;
			if (Projectile.ai[0] > 30)
            {
				Projectile.velocity = (closestNPC.Center - Projectile.Center).SafeNormalize(Vector2.Zero) * projSpeed;
				Projectile.rotation = Projectile.velocity.ToRotation();

				Projectile.ai[0] = 0;
            }

            
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