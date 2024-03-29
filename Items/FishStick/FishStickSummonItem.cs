﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.DataStructures;
using ANB.Projectiles;
using ANB.Buffs;

namespace ANB.Items.FishStick
{
    public class FishStickSummonItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fish Stick");
            Tooltip.SetDefault("Summons a fish which shoots homing bubbles to protect you");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true; //pretty sure this lets controllers aim anywhere on the screen
            ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;
        }
        public override void SetDefaults()
        {
            Item.damage = 20; //this is a placeholder-change later with testing, depending on how it is available to get
            Item.knockBack = 3f; //don't even know if this is necessary for minions
            Item.mana = 10;
            Item.width = 46;
            Item.height = 44;
            Item.useTime = 36;
            Item.useAnimation = 36;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.value = Item.sellPrice(gold: 3);
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item44; //pretty sure this is sound of pet summons, need to check

            Item.noMelee = true;
            Item.DamageType = DamageClass.Summon;
            Item.buffType = ModContent.BuffType<FishStickSummonBuff>();
            Item.shoot = ModContent.ProjectileType<Projectiles.FishStickSummon>();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)      
        {
            player.AddBuff(Item.buffType, 2);

            position = Main.MouseWorld;
            return true;
        }//may need but don't want it to spawn on mouse, want it above head, maybe it will fly to above head tho
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddRecipeGroup("IronBar", 5)
                .AddRecipeGroup("Wood", 15)
                .AddRecipeGroup("ANB:Fish", 1)
                .Register();
        }
    }
}
