using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using ANB.Rarities;

namespace ANB.Items.StoneSet
{
    public class StoneSword : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Just a simple stone sword...or is it?\n Explodes into flying debris after striking an enemy");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

        }
        public override void SetDefaults()
        {
            Item.height = 16;
            Item.width = 16;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 50;
            Item.useAnimation = 50;
            Item.autoReuse = false;
            Item.DamageType = DamageClass.Melee;
            Item.damage = 15;
            Item.knockBack = 9;
            Item.value = 50;
            Item.crit = 10;
            Item.rare = ModContent.RarityType<UpgradedRarity>();
            Item.UseSound = SoundID.DD2_MonkStaffGroundMiss;
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(3))
            {
                Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, ModContent.DustType<StoneSet.StoneDust>());
            }
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            if (target.value > 0f || (target.damage > 0 && !target.friendly))
            {
                pumpkinSword(player, target.whoAmI, (int)(damage * 1.5), knockback);
            }
        }

        private void pumpkinSword(Player player, int i, int dmg, float kb)
        {
            int num = Main.rand.Next(100, 300);
            int num2 = Main.rand.Next(100, 300);
            if (Main.rand.Next(2) == 0)
            {
                num -= Main.maxScreenW / 2 + num;
            }
            else
            {
                num += Main.maxScreenW / 2 - num;
            }
            if (Main.rand.Next(2) == 0)
            {
                num2 -= Main.maxScreenH / 2 + num2;
            }
            else
            {
                num2 += Main.maxScreenH / 2 - num2;
            }
            num += (int)player.position.X;
            num2 += (int)player.position.Y;
            float num3 = 8f;
            Vector2 vector = new Vector2((float)num, (float)num2);
            float num4 = Main.npc[i].position.X - vector.X;
            float num5 = Main.npc[i].position.Y - vector.Y;
            float num6 = (float)System.Math.Sqrt((double)(num4 * num4 + num5 * num5));
            num6 = num3 / num6;
            num4 *= num6;
            num5 *= num6;
            Projectile.NewProjectile((float)num, (float)num2, num4, num5, 321, dmg, kb, player.whoAmI, (float)i, 0f);
        }
    }
}