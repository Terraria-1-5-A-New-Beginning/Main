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
        Vector2 FirstStageDestination = Vector2.Zero;
        Vector2 LastFirstStageDestination = Vector2.Zero;
        int timer=1;
        int attacktype=0;
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
        //bool Lunatic = false; NOPE.
        //bool MoonLord = false; NOPE.
        public override void OnSpawn(IEntitySource source)
        {
            PickRandomAI();
            base.OnSpawn(source);
        }
        bool canbehit = true;
        NPC Memory = null;
        NPC Memory2 = null;
        NPC Memory3 = null;
        NPC Memory4 = null;//bad approach but anyway
        //public override string Texture => "Terraria/Images/NPC_" + NPCID.Pixie;
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 4;
            NPCID.Sets.DontDoHardmodeScaling[Type] = true;
            // Enemies can pick up coins, let's prevent it for this NPC
            NPCID.Sets.CantTakeLunchMoney[Type] = true;
            NPC.boss = true;
            NPCDebuffImmunityData debuffData = new NPCDebuffImmunityData
            {
                SpecificallyImmuneTo = new int[] {

                    BuffID.Confused // Most NPCs have this
				}
            };
            NPCID.Sets.DebuffImmunitySets.Add(Type, debuffData);

            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            NPC.damage = 100;
            NPC.height = 52;
            NPC.width = 44;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.npcSlots = 10f;
            NPC.defense = 25;
            NPC.noGravity = true;
            NPC.aiAction = 0;
            NPC.aiStyle = 0;
            NPC.noTileCollide = true;
            NPC.boss = true;
            NPC.knockBackResist = 0f;
            NPC.lifeMax = 100000;
            NPC.life = 100000;
            base.SetDefaults();
        }

        public void PickRandomAI()
        {
            NPC.ai[0] = Main.rand.Next(0, 3);
            NPC.ai[1] = 0;
            timer = 0;
            if (NPC.ai[0] == 0)
            {
                NPC.ai[1]= Main.rand.Next(0, 360);
                timer= Main.rand.NextFromList(-1, 1);
            }
            Main.NewText(NPC.ai[0]);
        }
        private void bossAI()
        {
            switch (NPC.ai[0])
            {
                case 1:
                    {
                        NPC.damage = 50;
                        NPC.alpha = 0;
                        if (timer == 60 || timer == 50 || timer == 70)
                        {
                            Projectile.NewProjectile(NPC.GetBossSpawnSource(NPC.target), NPC.Center,
                                (-NPC.Center + Main.player[NPC.target].Center) / 10, ProjectileID.EyeLaser, 70, 0);
                        }
                        if (NPC.ai[1] == 3)
                        {
                            PickRandomAI();
                            return;
                        }
                        if ((timer == 0))
                        {
                            NPC.ai[1]++;

                            timer = 70;
                            if ((Main.netMode != NetmodeID.MultiplayerClient))
                            {
                                //NPC.velocity *= 0.96f;//slowly stop.
                                FirstStageDestination = -NPC.Center + Main.player[NPC.target].Center;
                                //FirstStageDestination.Normalize();
                                //FirstStageDestination = FirstStageDestination * 400;
                                //FirstStageDestination = NPC.Center+(-NPC.Center + Main.player[NPC.target].Center + FirstStageDestination).RotateRandom(MathHelper.PiOver2);
                                //FirstStageDestination *= 1.2f;
                                Vector2 FFStageDest = FirstStageDestination;
                                FFStageDest.Normalize();
                                FirstStageDestination = NPC.Center + (FFStageDest * 400 + FirstStageDestination).RotateRandom(MathHelper.PiOver4 / 2);
                                //NPC.netUpdate = true;
                            }
                        }
                        Vector2 toDestination = FirstStageDestination - NPC.Center;
                        Vector2 toDestinationNormalized = toDestination.SafeNormalize(Vector2.UnitY);
                        float speed = Math.Min(400, toDestination.Length());
                        NPC.velocity = toDestinationNormalized * speed / 20;
                        if (FirstStageDestination != LastFirstStageDestination)
                        {
                            if (Main.netMode != NetmodeID.Server)
                            {
                                // For visuals regarding NPC position, netOffset has to be concidered to make visuals align properly
                                NPC.position += NPC.netOffset;

                                // Draw a line between the NPC and its destination, represented as dusts every 20 pixels
                                Dust.QuickDustLine(NPC.Center + toDestinationNormalized * NPC.width, FirstStageDestination, toDestination.Length() / 20f, Color.Purple);

                                NPC.position -= NPC.netOffset;
                            }
                        }
                        LastFirstStageDestination = FirstStageDestination;
                    }
                    break;
                case 0:{
                        NPC.ai[1]++;
                        NPC.damage = 100;
                        Vector2 place = NPC.Center;
                        NPC.position = Main.player[NPC.target].Center + new Vector2(100+1 * NPC.ai[1]).RotatedBy(MathHelper.ToRadians(timer*NPC.ai[1]));
                        
                        if ((place-NPC.Center).Length() >= 32)
                        {for (int h = 0; h < 3; h++)
                            {
                                Dust.NewDust(place, 2, 2, 20, newColor: Color.Purple, Scale:2);
                                Dust.NewDust(NPC.Center, 2, 2, 20, newColor: Color.Purple, Scale: 2);
                            }
                            Dust.QuickDustLine(place , NPC.Center, (place - NPC.Center).Length() / 20f, Color.Purple);

                        }

                        if ((NPC.ai[1] % 12)==0 && NPC.ai[1]>30)
                        {
                            Projectile.NewProjectile(NPC.GetBossSpawnSource(NPC.target), NPC.Center,
                                (-NPC.Center + Main.player[NPC.target].Center) / 20, ProjectileID.EyeLaser, 70, 0);
                        }
                        if (NPC.ai[1] == 360)
                        {
                            PickRandomAI();

                        }
                    }
                    
                    break;

                case 2:
                    {
                        if (NPC.ai[1] == 30) { PickRandomAI(); }
                        //timer++;
                        NPC.position = LastFirstStageDestination;
                        if (timer == 0)
                        {
                            Vector2 place = NPC.Center;
                            NPC.position = Main.player[NPC.target].Center + new Vector2(100 + Main.rand.Next(0, 100)).RotatedBy(MathHelper.ToRadians(Main.rand.Next(0, 360)));
                            LastFirstStageDestination = NPC.position;
                            for (int h = 0; h < 3; h++)
                            {
                                Dust.NewDust(place, 2, 2, 20, newColor: Color.Purple, Scale: 2);
                                Dust.NewDust(NPC.Center, 2, 2, 20, newColor: Color.Purple, Scale: 2);
                            }
                            timer = 20;
                            NPC.ai[1]++;
                            Projectile.NewProjectile(NPC.GetBossSpawnSource(NPC.target), NPC.Center,
                                (-NPC.Center + Main.player[NPC.target].Center).SafeNormalize(Vector2.One), ProjectileID.EyeLaser, 70, 0);

                            Dust.QuickDustLine(place, NPC.Center, (place - NPC.Center).Length() / 20f, Color.Purple);


                        }

                    }
                    break;
                default:
                    {
                        PickRandomAI();
                    }
                    break;
            }
        }
        
        private void noAI()
        {
            NPC.damage = 0;
            NPC.alpha = 128;
            if (timer == 0 && attacktype == 1)
            {
                attacktype = 0;
                timer = 180;
            }
            if (timer == 0 && attacktype == 0)
            {
                attacktype = 1;
                timer = 40;
            }

            NPC.ai[0] += 1.1f* NPC.ai[1];
            if (MathF.Abs(NPC.ai[0]) > 360)
            {
                NPC.ai[0] = 0;
            }
            if (attacktype == 0)
            {
                if (!Main.player[NPC.target].dead)
                {

                    Vector2 desiredPos = Main.player[NPC.target].Center;
                    desiredPos += 300 * new Vector2((float)Math.Sin(MathHelper.ToRadians(NPC.ai[0])), (float)Math.Cos(MathHelper.ToRadians(NPC.ai[0])));
                    Vector2 mov = NPC.Center - desiredPos;
                    mov.Normalize();
                    NPC.velocity -= mov * (float)Math.Min((double)(NPC.Center - desiredPos).Length(), 2);
                    NPC.velocity *= 0.9f;
                }
                if (NPC.velocity.Length() >= 30)
                {
                    NPC.velocity.Normalize();
                    NPC.velocity *= 30;
                }
            }
            if (attacktype == 1 && timer == 40)
            {
                NPC.velocity = Vector2.Zero;
                Projectile.NewProjectile(NPC.GetBossSpawnSource(NPC.target), NPC.Center,
                    (-NPC.Center+Main.player[NPC.target].Center)/4, ProjectileID.EyeLaser, 80, 0);
                NPC.ai[1] = Main.rand.NextFromList(-1, 1);
            }

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
        {
            if (++NPC.frameCounter >= 6)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y += 52;
                NPC.frame.Y = NPC.frame.Y % 208;
            }
            if (canbehit) return base.PreDraw(spriteBatch, screenPos, drawColor);
            return base.PreDraw(spriteBatch, screenPos, new Color (255, 255, 255, 64));
        }
        
        public override void AI()
        {
            Main.NewText(timer);
            if ((Main.player[NPC.target].Center - NPC.Center).Length() >= 1000 && !Main.player[NPC.target].dead && !Main.player[NPC.target].active)
            {
                NPC.TargetClosest();
                Vector2 place = NPC.Center;
                NPC.Center = Main.player[NPC.target].Center - new Vector2(0, 100);
                for (int h = 0; h < 3; h++)
                {
                    Dust.NewDust(place, 2, 2, 20, newColor: Color.Purple, Scale: 2);
                    Dust.NewDust(NPC.Center, 2, 2, 20, newColor: Color.Purple, Scale: 2);
                }
                Dust.QuickDustLine(place, NPC.Center, (place - NPC.Center).Length() / 20f, Color.Purple);
            }
            
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                NPC.TargetClosest();
            }
            if (NPC.ai[0] != 0)
            {
                timer--;
                if (timer < 0)
                {
                    timer = 0;
                }
            }
            if (Main.player[NPC.target].dead)
            {
                NPC.velocity += Vector2.UnitY * 2;
            }
            else
            {
                if (first)
                {
                    FirstStageDestination = NPC.position;
                    LastFirstStageDestination = NPC.position;
                    MemoryGlobalNPC.memory = true;
                    // NPC.GetGlobalNPC<MemoryGlobalNPC>().memory = true;
                    Main.NewText("You feel like time has shattered across the entire world.\r\nYour memories are rushing back.\r\nIt's lore time!");
                    first = !first;
                }
                Main.dayTime = false;
                Main.time = 0;
                if (!(Memory == null || !Memory.active) || !(Memory2 == null || !Memory2.active) || !(Memory3 == null || !Memory3.active) || !(Memory4 == null || !Memory4.active))
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
                //CheckBoss(0.02f, ref Lunatic, NPCID.CultistBoss, "Epic Text", Color.Red); nope

                NPC.netUpdate = true;
            }
            base.AI();
        }

    }
}
