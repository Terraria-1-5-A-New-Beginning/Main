using System;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Terraria.Graphics;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;

namespace ANB.Projectiles.StarSwordProj
{
    public class StarProj : ModProjectile
    {
		int angle;
		const int rotSpeed = 2;
		public override void SetDefaults()
        {
			Projectile.damage = 20; //<---- bad
            Projectile.height = 24; //placeholder
            Projectile.width = 24; //placeholder
            Projectile.knockBack = 2;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
			Projectile.aiStyle = 0;
			Projectile.light = 1f;
			Projectile.friendly = false;
			Projectile.hostile = false;
			Projectile.timeLeft = 300;
		}
        public override void OnSpawn(IEntitySource source)
        {
			angle = Main.rand.Next(0, 360);
            base.OnSpawn(source);
        }
        public override void AI()
        {
			Projectile.ai[0]++;
			float maxDetectRadius = 600f;

			//Vector2 target1 = Main.rand.NextVector2CircularEdge(60f, 60f);
			//Projectile.velocity = target1 * projSpeed;




			
			NPC closestNPC = FindClosestNPC(maxDetectRadius);
			Projectile.ai[0]++;
			if (Projectile.ai[0] == 61)
            {
				Dust.NewDust(Projectile.position, 1,1, DustID.YellowStarDust);

            }
				if (Projectile.ai[0] > 60)
			{
				Projectile.friendly = true;
				if (closestNPC == null || !closestNPC.active)
				{
					Projectile.Kill();
					return;
				}
				else
				{
					Projectile.velocity = (closestNPC.Center - Projectile.Center).SafeNormalize(Vector2.UnitX) * 6;
					//Projectile.ai[0] = 0;
				}
            }
			//Projectile.rotation = Projectile.velocity.ToRotation();


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
        public override bool PreDraw(ref Color lightColor)
		{
			if (Projectile.ai[0] <= 60)
			{
				angle += rotSpeed;
				if (angle > 360) angle -= 360;
				Main.instance.LoadProjectile(Projectile.type);
				Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
				Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, Projectile.height * 0.5f);
				for (int k = 0; k < 3; k++)
				{
					Vector2 drawPos = (Projectile.position - Main.screenPosition)+Vector2.One.RotatedBy(MathHelper.ToRadians(k*120+angle))*50*MathF.Cos(MathHelper.ToRadians(Projectile.ai[0]*1.5f)) + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
					Main.EntitySpriteDraw(texture, drawPos, null, Color.White, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
				}
					return false;
			}
			Projectile.rotation += rotSpeed * 2;

			return base.PreDraw(ref lightColor);
        }
    }
}