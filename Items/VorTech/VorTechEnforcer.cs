using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria;
using Microsoft.Xna.Framework;
using ANB.Projectiles.RaterGunProj;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;

namespace ANB.Items.VorTech
{
    internal class VorTechEnforcer : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("VorTech Enforcer Shotgun");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            Item.autoReuse = true;
            Item.shootSpeed = 12;
            Item.useTime = 16;
            Item.useAnimation = 16;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.DamageType = DamageClass.Ranged;
            Item.useAmmo = AmmoID.Bullet;
            Item.damage = 20;
            base.SetDefaults();
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(5, 2);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            player.GetModPlayer<ANBModPlayer>().ApplyShake(10);
            for (int b = 0; b < 6; b++)
            {
                Vector2 ve = velocity.RotatedByRandom(MathHelper.ToRadians(20));
                Projectile.NewProjectile(source, position, ve, type, damage, knockback, player.whoAmI);
            }
            return true;
        }
    }
}
