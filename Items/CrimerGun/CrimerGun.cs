using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria;
using Microsoft.Xna.Framework;
using ANB.Projectiles.CrimerGunProj;
using Terraria.DataStructures;

namespace ANB.Items.CrimerGun
{
    internal class CrimerGun : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Uses gel as ammo\r\n33% chance to not consume ammo");
            DisplayName.SetDefault("Blood Spitter");
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            Item.autoReuse = true;
            Item.shootSpeed = 12;
            Item.useTime = 8;
            Item.useAnimation = 8;

            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAmmo = ItemID.Gel;
            Item.DamageType = DamageClass.Ranged;
            Item.damage = 12;
            Item.shoot = ModContent.ProjectileType<CrimerGunProj>();
            base.SetDefaults();
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(5, 2);
        }
        public override bool CanConsumeAmmo(Item ammo, Player player)
        {

            if (Main.rand.NextFloat(1) <= 0.33f)
            {
                return false;
            }
            return base.CanConsumeAmmo(ammo, player);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position, velocity.RotatedByRandom(MathHelper.ToRadians(10)), type, damage, knockback, player.whoAmI);
            velocity = velocity.RotatedByRandom(MathHelper.ToRadians(10));
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }
    }
}
