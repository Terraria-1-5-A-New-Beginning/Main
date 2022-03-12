using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using ANB.Projectiles;

namespace ANB.Buffs
{
    public class FishStickSummonBuff : ModBuff
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fish");
			Description.SetDefault("The Fish will fight for you");

			Main.buffNoSave[Type] = true; 
			Main.buffNoTimeDisplay[Type] = true; 
		}

		public override void Update(Player player, ref int buffIndex)
		{
			if (player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.FishStickSummon>()] > 0) //set to fishstick one when made, stonesword so no errors
			{
				player.buffTime[buffIndex] = 18000;
			}
			else
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}
}