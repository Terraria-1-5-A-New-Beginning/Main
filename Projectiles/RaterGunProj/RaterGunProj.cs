using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria;

namespace ANB.Projectiles.RaterGunProj
{
    
    internal class RaterGunProj : ModProjectile
    {

        public override void SetDefaults()
        {
            Projectile.extraUpdates = 2;
            Projectile.timeLeft = 40;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.width = 2;
            Projectile.height = 2;
            
            base.SetDefaults();
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.CursedInferno, 30);
            base.OnHitNPC(target, damage, knockback, crit);
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.CursedInferno, 30);
            base.OnHitPlayer(target, damage, crit);
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.CursedInferno, 30);
            base.OnHitPvp(target, damage, crit);
        }

        public override void AI()
        {
            Projectile.velocity.Y += 0.14f;
            if (Projectile.timeLeft <= 35)
            {
                Dust.NewDust(Projectile.position, 1, 1, DustID.CursedTorch);
            }
            base.AI();
        }

    }
}
