using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Threading.Tasks;
namespace ANB.NPCs.MemoryBoss.MemoryMinions
{
    internal class MemoryCthulhu : ModNPC
    {//MAYBE i should just call the usual boss and make them stronger and not drop loot in GlobalNPC...
        //this would be much easier actually.
        //
        public override string Texture => "Terraria/Images/NPC_" + NPCID.EyeofCthulhu;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Memory Eye of Cthulhu");
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.EyeofCthulhu];
        }
        public override void BossLoot(ref string name, ref int potionType)
        {

        }
        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.EyeofCthulhu);
            NPC.lifeMax *= 20;
            base.SetDefaults();
        }
    }
    internal class MemorySlime : ModNPC
    {
        public override string Texture => "Terraria/Images/NPC_" + NPCID.KingSlime;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Memory King Slime");
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.KingSlime];
        }
        public override void BossLoot(ref string name, ref int potionType)
        {

        }

        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.KingSlime);
            NPC.lifeMax *= 20;
            base.SetDefaults();
        }
    }
    internal class MemoryBrain : ModNPC
    {
        public override string Texture => "Terraria/Images/NPC_" + NPCID.BrainofCthulhu;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Memory Brain Of Cthulhu");
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.BrainofCthulhu];
        }
        public override void BossLoot(ref string name, ref int potionType)
        {

        }

        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.BrainofCthulhu);
            NPC.lifeMax *= 20;
            base.SetDefaults();
        }
    }
    internal class MemoryWorm : ModNPC
    {
        public override string Texture => "Terraria/Images/NPC_" + NPCID.EaterofWorldsHead;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Memory Eater Of Worlds");
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.EaterofWorldsHead];
        }
        public override void BossLoot(ref string name, ref int potionType)
        {

        }

        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.EaterofWorldsHead);
            NPC.lifeMax *= 20;
            base.SetDefaults();
        }
    }
    internal class MemoryBee : ModNPC
    {
        public override string Texture => "Terraria/Images/NPC_" + NPCID.QueenBee;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Memory Queen Bee");
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.QueenBee];
        }
        public override void BossLoot(ref string name, ref int potionType)
        {

        }

        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.QueenBee);
            NPC.lifeMax *= 20;
            base.SetDefaults();
        }
    }
    internal class MemorySkeletron : ModNPC
    {
        public override string Texture => "Terraria/Images/NPC_" + NPCID.SkeletronHead;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Memory Skeletron");
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.SkeletronHead];
        }
        public override void BossLoot(ref string name, ref int potionType)
        {

        }

        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.SkeletronHead);
            NPC.lifeMax *= 20;
            base.SetDefaults();
        }
    }
}
