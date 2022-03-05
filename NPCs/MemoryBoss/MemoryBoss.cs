using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace ANB.NPCs.MemoryBoss
{
    internal class MemoryBoss : ModNPC
    {
        bool first = true;
        bool KingSlime = false;
        bool EyeCthulhu = false;
        bool BrainAndWorm = false;
        bool QueenBee = false;
        bool Skeletron = false;
        bool QueenSlime = false;
        bool Mechas = false;
        bool Golem = false;
        bool Fishron = false;
        bool Empress = false;
        bool Lunatic = false;
        //bool MoonLord = false; NOPE.

        bool canbehit = true;
        NPC Memory = null;
        NPC Memory2 = null;
        NPC Memory3 = null;
        NPC Memory4 = null;//bad approach but anyway
        public override string Texture => "Terraria/Images/NPC_" + NPCID.Pixie;
        public override void SetStaticDefaults()
        {
            NPC.boss = true;
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {

            NPC.boss = true;
            NPC.knockBackResist = 0.1f;
            NPC.lifeMax = 200000;
            NPC.life = 200000;
            base.SetDefaults();
        }

        private void noAI()
        {
            NPC.velocity *= 0.96f;//slowly stop.
        }
        private void bossAI()
        {
            //todo
            //i am not sure yet.

        }

        public override void OnKill()
        {
            //NPC.GetGlobalNPC<MemoryGlobalNPC>().memory = false;
            MemoryGlobalNPC.memory = false;
            base.OnKill();
        }
        #region CheckBoss stuff
        /// <summary>
        /// This check help spawn the boss
        /// </summary>
        /// <param name="hpmin"></param>
        /// <param name="bossbool"></param>
        /// <param name="type"></param>
        /// <param name="type2"></param>
        /// <param name="epicText"></param> 
        /// <param name="textColor"></param> 
        private void CheckBoss(float hpmin, ref bool bossbool, int type,int type2, string epicText, Color textColor)
        {
            if (NPC.life <= NPC.lifeMax * hpmin && !bossbool)
            {
                Memory = Main.npc[NPC.NewNPC(new EntitySource_BossSpawn(NPC),(int)NPC.position.X, (int)NPC.position.Y + 60, type)];
                Memory2 = Main.npc[NPC.NewNPC(new EntitySource_BossSpawn(NPC),(int)NPC.position.X, (int)NPC.position.Y + 60, type2)];
                CombatText.NewText(NPC.getRect(), textColor, epicText);//cool line
                Main.NewText(epicText, textColor);
                bossbool = !bossbool;
            }
        }
        private void CheckBoss(float hpmin, ref bool bossbool, int type, int type2, int type3, string epicText, Color textColor)
        {
            if (NPC.life <= NPC.lifeMax * hpmin && !bossbool)
            {
                Memory = Main.npc[NPC.NewNPC(new EntitySource_BossSpawn(NPC), (int)NPC.position.X, (int)NPC.position.Y + 60, type)];
                Memory2 = Main.npc[NPC.NewNPC(new EntitySource_BossSpawn(NPC), (int)NPC.position.X, (int)NPC.position.Y + 60, type2)];
                Memory3 = Main.npc[NPC.NewNPC(new EntitySource_BossSpawn(NPC), (int)NPC.position.X, (int)NPC.position.Y + 60, type3)];
                CombatText.NewText(NPC.getRect(), textColor, epicText);//cool line
                Main.NewText(epicText, textColor);
                bossbool = !bossbool;
            }
        }
        private void CheckBoss(float hpmin, ref bool bossbool, int type, int type2, int type3, int type4, string epicText, Color textColor)
        {
            if (NPC.life <= NPC.lifeMax * hpmin && !bossbool)
            {
                Memory = Main.npc[NPC.NewNPC(new EntitySource_BossSpawn(NPC), (int)NPC.position.X, (int)NPC.position.Y + 60, type)];
                Memory2 = Main.npc[NPC.NewNPC(new EntitySource_BossSpawn(NPC), (int)NPC.position.X, (int)NPC.position.Y + 60, type2)];
                Memory3 = Main.npc[NPC.NewNPC(new EntitySource_BossSpawn(NPC), (int)NPC.position.X, (int)NPC.position.Y + 60, type3)];
                Memory4 = Main.npc[NPC.NewNPC(new EntitySource_BossSpawn(NPC), (int)NPC.position.X, (int)NPC.position.Y + 60, type4)];
                CombatText.NewText(NPC.getRect(), textColor, epicText);//cool line
                Main.NewText(epicText, textColor);
                bossbool = !bossbool;
            }
        }
        private void CheckBoss(float hpmin, ref bool bossbool, int type, string epicText, Color textColor)
        {
            if (NPC.life <= NPC.lifeMax * hpmin && !bossbool)
            {
                Memory = Main.npc[NPC.NewNPC(new EntitySource_BossSpawn(NPC), (int)NPC.position.X, (int)NPC.position.Y + 60, type)];
                CombatText.NewText(NPC.getRect(), textColor, epicText);//cool line
                Main.NewText(epicText, textColor);
                bossbool = !bossbool;
            }
        }
        #endregion
        public override bool? CanBeHitByProjectile(Projectile projectile)
        {
            if (canbehit) return base.CanBeHitByProjectile(projectile);
            return false;
        }
        public override bool? CanBeHitByItem(Player player, Item item)
        {if (canbehit) return base.CanBeHitByItem(player, item);
            return false;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {if (canbehit) return base.PreDraw(spriteBatch, screenPos, drawColor);
            return base.PreDraw(spriteBatch, screenPos, new Color (255, 255, 255, 64));
        }

        public override void AI()
        {
            if (first)
            {
                MemoryGlobalNPC.memory = true;
               // NPC.GetGlobalNPC<MemoryGlobalNPC>().memory = true;
                Main.NewText("You feel like time has shattered across the entire world.\r\nYour memories are rushing back.\r\nIt's lore time!");
                first = !first;
            }
            Main.time = 10000;
            if (!(Memory == null || !Memory.active)|| !(Memory2 == null || !Memory2.active) || !(Memory3 == null || !Memory3.active) || !(Memory4 == null || !Memory4.active))
            {
                noAI();
                canbehit = false;
                base.AI();
                return;//memory mode
            }
            canbehit = true;
            bossAI();


            CheckBoss(0.95f, ref KingSlime, NPCID.KingSlime, "Epic Text", Color.Red);
            CheckBoss(0.85f, ref EyeCthulhu, NPCID.EyeofCthulhu, "Epic Text", Color.Red);
            CheckBoss(0.75f, ref BrainAndWorm, NPCID.BrainofCthulhu, NPCID.EaterofWorldsHead, "Epic Text", Color.Red);
            CheckBoss(0.65f, ref QueenBee, NPCID.QueenBee, "Epic Text", Color.Red);
            CheckBoss(0.55f, ref Skeletron, NPCID.SkeletronHead, "Epic Text", Color.Red);
            CheckBoss(0.45f, ref QueenSlime, NPCID.QueenSlimeBoss, "Epic Text", Color.Red);
            CheckBoss(0.35f, ref Mechas, NPCID.Retinazer, "Epic Text", Color.Red);
            CheckBoss(0.25f, ref Golem, NPCID.Golem, "Epic Text", Color.Red);
            CheckBoss(0.15f, ref Fishron, NPCID.DukeFishron, "Epic Text", Color.Red);
            CheckBoss(0.07f, ref Empress, NPCID.EmpressButterfly, "Epic Text", Color.Red);
            CheckBoss(0.02f, ref Lunatic, NPCID.CultistBoss, "Epic Text", Color.Red);


            base.AI();
        }

    }
}
