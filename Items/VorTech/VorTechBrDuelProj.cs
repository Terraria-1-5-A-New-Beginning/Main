using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Graphics;
using Terraria.Graphics.Shaders;


namespace ANB.Items.VorTech
{
    internal class VorTechBrDuelProj : ModProjectile
    {
        FlameLashDrawer s;
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 64;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 3;
        }
        public override void SetDefaults()
        {
            Projectile.extraUpdates = 6;
            Projectile.timeLeft = 600;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.width = 4;
            Projectile.height =4;

            base.SetDefaults();
        }
        public override void OnSpawn(IEntitySource source)
        {
            Projectile.localAI[0] = 1;
            Projectile.localAI[1] = 1;
            s = new FlameLashDrawer();
            base.OnSpawn(source);
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation();

            base.AI();
        }
        public override bool PreDraw(ref Color lightColor)
        {
            s.Draw(Projectile);
            return false;
            return base.PreDraw(ref lightColor);
        }

    }
}
