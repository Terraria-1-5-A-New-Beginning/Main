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

namespace ANB
{
    internal class ANBModPlayer : ModPlayer
    {
        public int FrostDamage = 0;
        public int FrostSword = 0;
        public int FrostDamageMeter = 500;
        public override void OnRespawn(Player player)
        {
            FrostDamage = 0;
            FrostSword = 0;
            base.OnRespawn(player);
        }
        public void AddFrostMeter(int damage)
        {
            if ((FrostDamage< FrostDamageMeter) && (FrostDamage+damage > FrostDamageMeter))
            {
                CombatText.NewText(Player.getRect(), Color.CadetBlue, "Frost Attack Ready!", true, true);
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
                    Projectile.NewProjectile(new EntitySource_ByItemSourceId(Player, Player.HeldItem.type), Player.Center, vel*10, ModContent.ProjectileType<FrostSlash>(), 100, 2, Player.whoAmI);
                }
                FrostSword = 0;
            }

            base.ResetEffects();
        }

        public void NextAtk()
        {
            CombatText.NewText(Player.getRect(), Color.CadetBlue, FrostSword, true, true);
            FrostSword++;
            if (FrostSword == 9)
            {
                FrostSword = 8;
            }
        }
    }
}
