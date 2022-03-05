using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;

namespace ANB.NPCs.MemoryBoss
{
    internal class MemoryGlobalNPC : GlobalNPC
    {
        public static bool memory=false;

        public override void ResetEffects(NPC npc)
        {
            memory = false;
            for (int a = 0; a < Main.maxNPCs; a++)
            {
                if (Main.npc[a].type == ModContent.NPCType<MemoryBoss>()&& Main.npc[a].active) memory = true;
            }
            base.ResetEffects(npc);
        }
        public override void SetDefaults(NPC npc)
        {
            if (memory)
            {
                npc.damage *= 5;
                npc.lifeMax *= 20;
                npc.GivenName = "Blurred Memory";
                NPC.setNPCName(npc.GivenOrTypeName, npc.type);
            }
            base.SetDefaults(npc);
        }
        public override bool PreDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (memory)
            {
                drawColor = Color.Fuchsia;
            }
                return base.PreDraw(npc, spriteBatch, screenPos, drawColor);
        }
        public override bool CheckDead(NPC npc)
        {if (memory&&npc.type != ModContent.NPCType<MemoryBoss>())
            {
                npc.boss = false;
            }

            return base.CheckDead(npc);
        }
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            NoDropCond mrule = new NoDropCond();

            int limiter = npcLoot.Get().Count;
            //int a = 0;
            List<IItemDropRule> rls = new List<IItemDropRule>();
            foreach (IItemDropRule entry in npcLoot.Get())
            {
                /**
                 * I strongly advice against touching this code.
                 * It works somehow, but when i try to adapt this to *for* cycle
                 * it stops compiling!
                 * wth
                 * **/
                IItemDropRule ruleToChain = new LeadingConditionRule(mrule);
                //a++;
                ruleToChain.OnSuccess(entry);
                //npcLoot.Remove(entry); 
                rls.Add(ruleToChain);
            }
            npcLoot.RemoveWhere(x => true);
            foreach (IItemDropRule entry in rls)
            { npcLoot.Add(entry);
            }
            base.ModifyNPCLoot(npc, npcLoot);
                    return;
        }
    }
    public class NoDropCond : IItemDropRuleCondition
    {
        public bool CanDrop(DropAttemptInfo info)
        {
            return !MemoryGlobalNPC.memory;
        }

        public bool CanShowItemDropInUI()
        {
            return false;
        }

        public string GetConditionDescription()
        {
            return "no drop condition";
        }
    }
}
