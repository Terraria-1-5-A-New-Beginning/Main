using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;

namespace ANB.Projectiles.FrostBlade
{
    internal class FrostSlash : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.timeLeft = 120;
            Projectile.height = 20;
            Projectile.width = 20;
            Projectile.penetrate = 1;
            Projectile.tileCollide = false;
            Projectile.hostile = false;
            Projectile.friendly = true;
            base.SetDefaults();
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Main.player[Projectile.owner].GetModPlayer<ANBModPlayer>().AddFrostMeter(damage);
            base.OnHitNPC(target, damage, knockback, crit);
            Projectile.penetrate = -1;
        }
        
        public override void AI()
        {if (Projectile.penetrate == -1)
            {
                Projectile.hostile = false;
                Projectile.friendly = false;
                Projectile.alpha += 25;
                Projectile.velocity *= 0.9f;
                if (Projectile.alpha >= 250)
                {
                    Projectile.Kill();
                }
            }
            Projectile.rotation = Projectile.velocity.ToRotation();
            base.AI();
        }
    }
}
