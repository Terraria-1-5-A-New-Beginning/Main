using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.DataStructures;
using ANB.Rarities;
using ANB.Projectiles;

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
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.autoReuse = true;
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
            Vector2 position = player.Center;
            Vector2 velocity = Vector2.Normalize(player.position - target.position).RotatedByRandom(MathHelper.PiOver2);
            IEntitySource source = new EntitySource_OnHit_ByItemSourceID(player, target, Item.whoAmI);
            Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<Projectiles.StoneSwordProj>(), damage, knockback);
        }
    }
}
