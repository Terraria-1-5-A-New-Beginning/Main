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
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 14;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 3;
        }
        public override void SetDefaults()
        {
            Projectile.extraUpdates = 6;
            Projectile.timeLeft = 100;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.width = 4;
            Projectile.height =4;

            base.SetDefaults();
        }
        public override void OnSpawn(IEntitySource source)
        {
            s = new FlameLashDrawer();
            base.OnSpawn(source);
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation();

            base.AI();
        }

        public override void PostDraw(Color lightColor)
        {
            s.Draw(Projectile);
            Main.instance.LoadProjectile(Projectile.type);
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
            Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, Projectile.height * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = (Projectile.oldPos[k] - Main.screenPosition) + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Main.EntitySpriteDraw(texture, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
            }
            base.PostDraw(lightColor);
        }

    }
}
