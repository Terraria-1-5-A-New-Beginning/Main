using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Effects;
using Microsoft.Xna.Framework.Graphics;
using ANB.Projectiles.FrostBlade;

namespace ANB.Projectiles.IceWitch
{
    internal class IceWitch : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.timeLeft = 100;
            Projectile.width = 38;
            Projectile.height = 48;
            Projectile.tileCollide = false;
            Projectile.hostile = false;
            Projectile.friendly = true;
            base.SetDefaults();
        }
        public override void AI()
        {
            Projectile.velocity = Vector2.Zero;
            if (Projectile.timeLeft % 20 == 0)
            {
                for (int a =0; a < 10; a++)
                {
                    Projectile.NewProjectile(new EntitySource_Parent(Projectile), Projectile.Center, Vector2.One.RotatedBy(MathHelper.ToRadians(36*a))*5, ModContent.ProjectileType<FrostSnowflake>(), 50, 2, Projectile.owner);
                }
            }
            base.AI();
        }
    }
}
