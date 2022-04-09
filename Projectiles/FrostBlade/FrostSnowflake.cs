using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace ANB.Projectiles.FrostBlade
{
    internal class FrostSnowflake : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.timeLeft = 60;
            Projectile.height = 32;
            Projectile.width = 32;
            Projectile.tileCollide = true;
            Projectile.hostile = false;
            Projectile.friendly = true;
            base.SetDefaults();
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Main.player[Projectile.owner].GetModPlayer<ANBModPlayer>().AddFrostMeter(damage);
            base.OnHitNPC(target, damage, knockback, crit);
        }

        public override void AI()
        {
            Projectile.rotation+=0.2f;
            base.AI();
        }

    }
}
