using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Vaultaria.Common.Utilities;
using System.Collections.Generic;
using Vaultaria.Content.Items.Materials;

namespace Vaultaria.Content.Items.Accessories.Skills
{
    public class Killer : ModSkill
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.Size = new Vector2(30, 30);
            Item.accessory = true;
            Item.value = Item.buyPrice(silver: 80);
            Item.rare = ItemRarityID.Blue;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int bonusCrit = SkillUtilities.DisplaySkillBonusText(55f, 0.05f);
            int bonusFireRate = SkillUtilities.DisplaySkillBonusText(40f, 0.05f);

            ItemText.Text(tooltips, Mod, "Tooltip1", "Killing an enemy increases your Projectile Crit Damage and Fire Rate for 7 seconds");
            ItemText.Text(tooltips, Mod, "Tooltip2", "Bonuses increase as you progress", ItemText.VaultarianColours.Information);
            ItemText.Text(tooltips, Mod, "Tooltip3", $"+{bonusCrit}% Crit Damage");
            ItemText.Text(tooltips, Mod, "Tooltip4", $"+{bonusFireRate}% Fire Rate");
            ItemText.Text(tooltips, Mod, "Tooltip5", "Found in Rich Mahogany Chests", ItemText.VaultarianColours.Information);
        }
    }
}