using Microsoft.Xna.Framework;
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
            Projectile.height = 30;
            Projectile.width = 30;
            Projectile.tileCollide = true;
            Projectile.penetrate = 1;
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
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
                return false;
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            width  /= 2;
            height /= 2;
            hitboxCenterFrac /=2;
            return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
        }

        public override void AI()
        {
            if (Projectile.penetrate == -1)
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
            Projectile.rotation+=0.2f;
            base.AI();
        }

    }
}
