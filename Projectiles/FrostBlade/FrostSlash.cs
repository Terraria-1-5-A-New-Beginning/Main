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
            Projectile.height = 59;
            Projectile.width = 59;
            Projectile.tileCollide = false;
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
            Projectile.rotation = Projectile.velocity.ToRotation();
            base.AI();
        }
    }
}
