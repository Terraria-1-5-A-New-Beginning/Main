using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria;
namespace ANB.Projectiles.CrimerGunProj
{
    internal class CrimerGunProj : ModProjectile
    {
        public override void SetDefaults()
        {
            //Projectile.extraUpdates = 1;
            Projectile.timeLeft = 40;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.width = 8;
            Projectile.height = 8;

            base.SetDefaults();
        }

        public override void AI()
        {
            Projectile.velocity.Y += 0.14f;
            if (Projectile.timeLeft <= 35)
            {
                Dust.NewDust(Projectile.position, 1, 1, DustID.BloodWater);
            }
            base.AI();
        }
    }
}
