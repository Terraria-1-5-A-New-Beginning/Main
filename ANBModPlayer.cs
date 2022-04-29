using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using ANB.Projectiles.FrostBlade;

namespace ANB//TODO: Vortech laser rifle and pistol maybe?
{
    internal class ANBModPlayer : ModPlayer
    {
        public float ShakeStrength = 0;
        public int FrostDamage = 0;
        public int FrostSword = 0;
        public int FrostDamageMeter = 1200;
        public override void OnRespawn(Player player)
        {
            FrostDamage = 0;
            FrostSword = 0;
            base.OnRespawn(player);
        }
        public void ApplyShake(int strength= 5)
        {
            ShakeStrength = strength;

        }
        public override void ModifyScreenPosition()
        {
            if (ShakeStrength > 0)
            {
                if (Main.myPlayer == Player.whoAmI)
                {
                    Vector2 sh = ShakeStrength * Vector2.UnitX.RotatedByRandom(MathHelper.ToRadians(360));
                    sh.X = (int)sh.X;
                    sh.Y = (int)sh.Y;
                    Main.screenPosition += sh;
                }
                ShakeStrength *= 0.93f;
                if (ShakeStrength < 0.2f) ShakeStrength = 0;
            }
            base.ModifyScreenPosition();
        }



        public void AddFrostMeter(int damage)
        {
            if ((FrostDamage< FrostDamageMeter) && (FrostDamage+damage > FrostDamageMeter))
            { Rectangle s = Player.getRect();
                s.Y -= 6;
                CombatText.NewText(s, Color.CadetBlue, "Frost Attack Ready!", true, true);
            }
            FrostDamage += damage;
        }
        public override void ResetEffects()
        {
            if (Main.mouseLeftRelease && Main.myPlayer == Player.whoAmI)
            {
                if (FrostSword == 8)
                {
                    Vector2 vel = (-Player.Center + Main.MouseWorld);
                    vel.Normalize();
                    Projectile.NewProjectile(new EntitySource_ItemUse(Player, Player.HeldItem), Player.Center-new Vector2(0, 3), vel*10, ModContent.ProjectileType<FrostSlash>(), 100, 2, Player.whoAmI);
                }
                FrostSword = 0;
            }

            base.ResetEffects();
        }

        public void NextAtk()
        {
            Rectangle s = Player.getRect();
            s.Y -= 6;
            CombatText.NewText(s, Color.CadetBlue, FrostSword, true, true);
            FrostSword++;
            if (FrostSword == 9)
            {
                FrostSword = 8;
            }
        }
    }
}
