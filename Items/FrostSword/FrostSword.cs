using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using ANB.Projectiles.FrostBlade;
using ANB.Projectiles.IceWitch;

namespace ANB.Items.FrostSword
{
    internal class FrostSword : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Avalanche");
            Tooltip.SetDefault("Hybrid weapon. Right click when you deal enough damage to summon ice witch.WIP");
        }

        public override bool AltFunctionUse(Player Player)
        {
            return true;
        }

        public override void SetDefaults()
        {
            Item.autoReuse = true;
            Item.knockBack = 4;
            Item.width = 42;
            Item.height = 42;
            Item.damage = 30;
            Item.DamageType = DamageClass.Melee;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.shoot = 0;
            Item.useTurn = true;//bc of strange bug i specified that player is allowed to turn while using item
            base.SetDefaults();//most vanilla sword allow player to turn around anyway.
        }

        public override bool? CanAutoReuseItem(Player player)
        {

            if (player.altFunctionUse == 2)
            {
                return false;
            }
            else
            {
                return base.CanAutoReuseItem(player);
            }
        }

        public override bool CanUseItem(Player player)
        {
            Item.UseSound = SoundID.Item1;
            Item.knockBack = 4;
            Item.channel = false;
            Item.noMelee = false;
            if (player.altFunctionUse == 2)
			{//IF RIGHT CLICK
                if (player.GetModPlayer<ANBModPlayer>().FrostDamage < player.GetModPlayer<ANBModPlayer>().FrostDamageMeter)
                {
                    return false;
                }
                player.GetModPlayer<ANBModPlayer>().FrostDamage = 0;
                Item.mana = 0;
				Item.shoot = ProjectileID.None;
				Item.useTime = -1;//<--dont touch these are intentional.
                Item.useAnimation = -1;//<--dont touch these are intentional.
                Item.noMelee = true;
				Item.shootSpeed = 0;
				Item.UseSound = SoundID.DD2_SkyDragonsFurySwing;
				int a = Projectile.NewProjectile(player.GetSource_ItemUse(Item), player.Center, Vector2.Zero, ModContent.ProjectileType<IceWitch>(), 0, 0, player.whoAmI);
				
            }
            else
            {

                switch (player.GetModPlayer<ANBModPlayer>().FrostSword) {
                    case >=0 and <=2:
                        Item.damage = 40;
                        Item.useTime = 15;
                        Item.useAnimation = 15;
                        Item.shoot = 0;
                        Item.shootSpeed = 0;
                        break;
                    case >= 3 and <= 5:
                        Item.damage = 45;
                        Item.useTime = 20;
                        Item.useAnimation = 20;
                        Item.shoot = 0;
                        Item.shootSpeed = 0;
                        break;
                    case >= 6 and <= 8:
                            Dust.NewDust(player.Center-new Vector2(6,9), 12, 18, DustID.SnowBlock);

                        Item.damage = 55;
                        Item.useTime = 25;
                        Item.useAnimation = 25;
                        Item.shoot = ModContent.ProjectileType<FrostSnowflake>();
                        Item.shootSpeed = 6;
                        break;
                }
            }
            player.GetModPlayer<ANBModPlayer>().NextAtk();
            return true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (type == ModContent.ProjectileType<FrostSnowflake>())
            {
                Projectile.NewProjectile(source, position, velocity.RotatedBy(MathHelper.ToRadians(30)), type, damage, knockback, player.whoAmI);
                Projectile.NewProjectile(source, position, velocity.RotatedBy(MathHelper.ToRadians(-30)), type, damage, knockback, player.whoAmI);
            }
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            player.GetModPlayer<ANBModPlayer>().AddFrostMeter(damage);
            base.OnHitNPC(player, target, damage, knockBack, crit);
        }
    }
}
