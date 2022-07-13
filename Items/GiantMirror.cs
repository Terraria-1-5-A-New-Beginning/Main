using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.DataStructures;
using ANB.Projectiles;

namespace ANB.Items
{
    public class GiantMirror : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("A giant mirror\nWhen you hit an enemy, rays of light will explode out in all directions");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.damage = 20;
            Item.height = 20;
            Item.width = 20;//everything above is placeholder
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 3;
            Item.crit = 12;
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item1;
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {

            base.OnHitNPC(player, target, damage, knockBack, crit);
            Vector2 pos = target.Center;
            Vector2 velocity = Vector2.Normalize(player.position - target.position).RotatedByRandom(3.14159);
            IEntitySource source = new EntitySource_OnHit(player, target);
            Projectile.NewProjectile(source, pos, velocity, ModContent.ProjectileType<LightRay>(), damage, knockBack, Main.myPlayer);
        }
    }
}