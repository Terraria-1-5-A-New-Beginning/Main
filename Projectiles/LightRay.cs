using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.DataStructures;
using ANB.Items;

namespace ANB.Projectiles
{
    public class LightRay : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.damage = 20;
            Projectile.height = 20;
            Projectile.width = 20;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.knockBack = 0.5f;
            Projectile.aiStyle = 36;
        }
    }
}