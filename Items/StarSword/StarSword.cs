using System;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using ANB.Projectiles.StarSwordProj;

namespace ANB.Items.StarSword
{
    public class StarSword : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Comet");
            Tooltip.SetDefault("Summons stars which chase enemies");
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            Item.height = 14; //placeholder
            Item.width = 14; //placeholder
            Item.damage = 20; //depends where in game
            Item.shoot = ModContent.ProjectileType<StarProj>();
            Item.shootSpeed = 0;
            Item.useTime = 20; //change
            Item.useAnimation = 20; //change
            Item.knockBack = 4;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 2;
            Item.value = 300;
            base.SetDefaults();
        }
    }
}