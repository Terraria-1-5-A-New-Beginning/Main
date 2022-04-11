using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace ANB.Items.Accesories
{
    public class Vacuum : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Increases pickup range for everything. Does not stack with similar items");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 50;
            Item.height = 34;
            Item.value = Item.sellPrice(0, 10);
            Item.rare = ItemRarityID.White;
            Item.accessory = true;
        }
        public override void UpdateEquip(Player player)
        {
            player.manaMagnet = true;
            player.goldRing = true;
            player.treasureMagnet = true;
            player.lifeMagnet = true;
        }
    }
}
