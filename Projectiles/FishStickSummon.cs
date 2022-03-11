using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using ANB.Buffs;

namespace ANB.Projectiles
{
    public class FishStickSummon : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projPet[Projectile.type] = true;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
        }
        public override void SetDefaults()
        {
            Projectile.damage = 8;
            Projectile.width = 18;
            Projectile.height = 11;
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


            if (++Projectile.frameCounter >= 12)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= 2)
                {
                    Projectile.frame = 0;
                }
            }

            Vector2 position = player.Center;
            Vector2 direction = player.Center;
            position.Y -= 15f;
            float minionPositionOffsetX = Projectile.minionPos * 40 * -player.direction;
            position.X += minionPositionOffsetX;
            Projectile.position = position;

            float distanceFromPlayer = (player.Center - Projectile.Center).Length();

            if(distanceFromPlayer >= 1000f)
            {
                //try to find how they do it for suspicious tentacle or something
            }
        }
    }
}