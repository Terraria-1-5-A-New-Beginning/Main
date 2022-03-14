using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using ANB.Buffs;
using ANB.Projectiles;

namespace ANB.Projectiles
{
    public class FishStickSummon : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projPet[Projectile.type] = true;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
            Main.projFrames[Projectile.type] = 3;
        }
        public override void SetDefaults()
        {
            //Projectile.gfxOffY = -10;
            Projectile.damage = 8;
            Projectile.width = 38;
            Projectile.height = 24;
            Projectile.knockBack = 1f;
            Projectile.tileCollide = false;
            Projectile.friendly = true;
            Projectile.minion = true;
            Projectile.minionSlots = 1f;
            Projectile.penetrate = -1;
                
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (player.dead || !player.active)
            {
                player.ClearBuff(ModContent.BuffType<FishStickSummonBuff>());
            }
            if (player.HasBuff(ModContent.BuffType<FishStickSummonBuff>()))
            {
                Projectile.timeLeft = 2;
            }

            //animation
            if (++Projectile.frameCounter >= 12)
            {
                Projectile.frameCounter = 0;
                Projectile.frame = ++Projectile.frame % Main.projFrames[Projectile.type];
            }

            //position above player, edit with testing
            Vector2 position = player.Center;
            Vector2 direction = player.Center;
            position.Y -= 35f;
            float minionPositionOffsetX = Projectile.minionPos * 50 * -player.direction;
            position.X += minionPositionOffsetX+50 * -player.direction;
            //Projectile.position = position;
            //Vector2 minionOffset = position;

            Projectile.velocity *= 0.7f;//for braking
            Projectile.velocity += 0.02f * (-Projectile.Center + position);

            //this should make it teleport to player after getting too far away, needs testing

            float distanceFromPlayer = Projectile.DistanceSQ(player.Center);
            if (distanceFromPlayer >= 10000000f)
            {
                Projectile.Center = Main.player[Projectile.owner].Center;
            }

            //lean towards velocity, face towards velocity
            Projectile.rotation = Projectile.velocity.ToRotation();
            //Projectile.spriteDirection = Projectile.direction;<--ikee, spritedirection is used for reversion of proj. sprite, not for rotating it 
            
            if (Projectile.velocity.X < 0)
            {
                Projectile.spriteDirection = -1;
                Projectile.rotation += MathHelper.Pi;

            }
            else
            {
                Projectile.spriteDirection = 1;
            }
            if (Projectile.velocity.LengthSquared() < 0.6f)
            {
                Projectile.rotation = (Main.player[Projectile.owner].direction == 1 ? MathHelper.TwoPi : 0);
                Projectile.spriteDirection = Main.player[Projectile.owner].direction;//possibly buggy code
            }
            float maxDetectRadius = 360f;
            //find closest NPC
            NPC closestNPC = FindClosestNPC(maxDetectRadius);
            if (closestNPC == null)
                return;
            Vector2 velocity = closestNPC.Center - Projectile.Center;
            IEntitySource source = new EntitySource_OnHit_ByItemSourceID(player, closestNPC, Projectile.whoAmI);
            
            Projectile.ai[0]++;
            if (Projectile.ai[0] > 15)
            {
                Projectile.NewProjectile(source, Projectile.Center, closestNPC.Center, ModContent.ProjectileType<FishStickSummonProj>(), 8, 1, Main.myPlayer);
                Projectile.ai[0] = 0;
            }
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
        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            hitbox = new Rectangle(hitbox.X, hitbox.Y, 18, 11);
            base.ModifyDamageHitbox(ref hitbox);
        }
    }
}