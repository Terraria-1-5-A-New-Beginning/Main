using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria.Localization;
using Terraria.UI.Chat;

namespace ANB
{
    public class LootNPC : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if(npc.type == NPCID.EaterofWorldsHead)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.RaterGun.RaterGun>(),1, 1, 1));
            }
        }
    }
   
}