using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Vaultaria.Common.Utilities;
using System.Collections.Generic;
using Vaultaria.Content.Items.Materials;

namespace Vaultaria.Content.Items.Accessories.Skills
{
    public class Incite : ModSkill
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.Size = new Vector2(30, 30);
            Item.accessory = true;
            Item.value = Item.buyPrice(silver: 1);
            Item.rare = ItemRarityID.White;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int bonusFireRate = SkillUtilities.DisplaySkillBonusText(100f, 0.05f);
            int bonusSpeed = SkillUtilities.DisplaySkillBonusText(85f, 0.05f);

            ItemText.Text(tooltips, Mod, "Tooltip1", "Taking enemy damage increases your Movement Speed and Fire Rate for 5 seconds");
            ItemText.Text(tooltips, Mod, "Tooltip2", "Bonuses increase as you progress", ItemText.VaultarianColours.Information);
            ItemText.Text(tooltips, Mod, "Tooltip3", $"+{bonusSpeed}% Movement Speed");
            ItemText.Text(tooltips, Mod, "Tooltip4", $"+{bonusFireRate}% Fire Rate");
            ItemText.Text(tooltips, Mod, "Tooltip5", "Found in Wooden Chests", ItemText.VaultarianColours.Information);
        }
    }
}