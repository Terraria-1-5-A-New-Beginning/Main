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
            Tooltip.SetDefault("Just a simple stone sword...or is it?\nExplodes into flying debris after striking an enemy");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

        }
        public override void SetDefaults()
        {
            
            Item.height = 16;
            Item.width = 16;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 42;
            Item.useAnimation = 42;
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
            Item.damage = 30;
            Item.knockBack = 4;
            Item.value = 50;
            Item.crit = 10;
            Item.rare = ModContent.RarityType<UpgradedRarity>();
            Item.UseSound = SoundID.Item1;
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
            for (int i = 0; i < 3; i++)
            {
                Vector2 offset = (target.Center - player.Center);
                offset.Normalize();
                Vector2 position = target.Center;//player.Center+offset*40;
                Vector2 velocity = Vector2.Normalize(player.position - target.position).RotatedByRandom(6.28319)*3;
                IEntitySource source = new EntitySource_OnHit(player, target);
                Projectile.NewProjectile(source, position, velocity+new Vector2(0,-3), ModContent.ProjectileType<StoneSwordProj>(), damage, knockback, Main.myPlayer);
            }
        }
    }
}
