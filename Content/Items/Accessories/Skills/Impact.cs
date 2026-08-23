using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Vaultaria.Common.Utilities;
using System.Collections.Generic;
using Vaultaria.Content.Items.Materials;

namespace Vaultaria.Content.Items.Accessories.Skills
{
    public class Impact : ModSkill
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.Size = new Vector2(30, 30);
            Item.accessory = true;
            Item.value = Item.buyPrice(silver: 30);
            Item.rare = ItemRarityID.Blue;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int bonusRanged = SkillUtilities.DisplaySkillBonusText(80f, 0.05f);
            int bonusMelee = SkillUtilities.DisplaySkillBonusText(100f, 0.05f);

            ItemText.Text(tooltips, Mod, "Tooltip1", "Increases your Ranged and Melee Damage");
            ItemText.Text(tooltips, Mod, "Tooltip2", $"+{bonusRanged}% Ranged Damage");
            ItemText.Text(tooltips, Mod, "Tooltip3", $"+{bonusMelee}% Melee Damage");
            ItemText.Text(tooltips, Mod, "Tooltip4", "Found in Rich Mahogany Chests", ItemText.VaultarianColours.Information);
        }
    }
}