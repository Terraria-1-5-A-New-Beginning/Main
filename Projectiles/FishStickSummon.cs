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
            Projectile.damage = 8;
            Projectile.width = 18;
            Projectile.height = 11;
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
            position.Y -= 15f;
            float minionPositionOffsetX = Projectile.minionPos * 40 * -player.direction;
            position.X += minionPositionOffsetX;
            Projectile.position = position;
            Vector2 minionOffset = position;

            //this should make it teleport to player after getting too far away, needs testing
            float distanceFromPlayer = Projectile.DistanceSQ(player.Center);

            if(distanceFromPlayer >= 1000*1000f)
            {
                Projectile.Center = minionOffset;
            }

            //lean towards velocity, face towards velocity
            Projectile.rotation = Projectile.velocity.ToRotation();
            Projectile.spriteDirection = Projectile.direction;

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
                Projectile.NewProjectile(source, position, closestNPC.Center, ModContent.ProjectileType<FishStickSummonProj>(), 8, 1, Main.myPlayer);
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
    }
}