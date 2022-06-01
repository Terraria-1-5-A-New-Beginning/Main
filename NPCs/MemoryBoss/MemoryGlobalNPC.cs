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
        public override bool InstancePerEntity => true;
        bool extraai = false;
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
                npc.damage *= 2;
                npc.lifeMax *= 2;
                npc.GivenName = "Blurred Memory";
                NPC.setNPCName(npc.GivenOrTypeName, npc.type);
            }
            base.SetDefaults(npc);
        }
        //public override void AI(NPC npc)
        //{
        //    Main.player[npc.target].ZoneCrimson = true;
        //    Main.player[npc.target].ZoneCorrupt = true;
        //    Main.player[npc.target].ZoneJungle = true;
        //    if (extraai == false)
        //    {
        //        extraai = true;
        //        AI(npc);
        //    }
        //    extraai = false;
        //    base.AI(npc);
        //}
        //public override void PostAI(NPC npc)
        //{
        //    if (extraai == false)
        //    {
        //        extraai = true;
        //        PostAI(npc);
        //    }
        //    extraai = false;
        //    base.PostAI(npc);
        //}
        //public override bool PreAI(NPC npc)
        //{
        //    if (extraai == false)
        //    {
        //        extraai = true;
        //        PreAI(npc);
        //    }
        //    extraai = false;
        //    base.PostAI(npc);
        //    return base.PreAI(npc);
        //}
        
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
