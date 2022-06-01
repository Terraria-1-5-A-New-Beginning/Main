using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Terraria.Localization;

namespace ANB
{
	public class ANB : Mod
	{
        public override void AddRecipeGroups()
        {
            
            // Fish Group
            RecipeGroup group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + "Fish", new int[]
            {
                ItemID.ArmoredCavefish,
                ItemID.AtlanticCod,
                ItemID.Bass,
                ItemID.ChaosFish,
                ItemID.CrimsonTigerfish,
                ItemID.Damselfish,
                ItemID.DoubleCod,
                ItemID.Ebonkoi,
                ItemID.FlarefinKoi,
                ItemID.Flounder,
                ItemID.FrostMinnow,
                ItemID.GoldenCarp,
                ItemID.Hemopiranha,
                ItemID.Honeyfin,
                ItemID.NeonTetra,
                ItemID.Obsidifish,
                ItemID.PrincessFish,
                ItemID.Prismite,
                ItemID.RedSnapper,
                ItemID.Salmon,
                ItemID.SpecularFish,
                ItemID.Stinkfish,
                ItemID.Trout,
                ItemID.Tuna,
                ItemID.VariegatedLardfish,
            });
            RecipeGroup.RegisterGroup("ANB:Fish", group);
        }
    }
}