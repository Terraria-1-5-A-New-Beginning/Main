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
    internal class VorTechBrDuelPistol : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("VorTech Broken Dueling Pistol");
            Tooltip.SetDefault("Has 1/3 chance of actually shooting");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            Item.autoReuse = false;
            Item.shootSpeed = 6;
            Item.useTime = 60;
            Item.useAnimation = 60;
            Item.shoot = ModContent.ProjectileType<VorTechBrDuelProj>();
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.DamageType = DamageClass.Ranged;
            Item.damage = 90;
            base.SetDefaults();
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(5, 2);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int rn= Main.rand.Next(2);
            Main.NewText(rn);
            if (rn == 0)
            {
                player.GetModPlayer<ANBModPlayer>().ApplyShake();
                return true;
            }
            return false;
        }
    }
}
