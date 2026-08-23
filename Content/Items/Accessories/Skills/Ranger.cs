using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Vaultaria.Common.Utilities;
using System.Collections.Generic;
using Vaultaria.Content.Items.Materials;

namespace Vaultaria.Content.Items.Accessories.Skills
{
    public class Ranger : ModSkill
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.Size = new Vector2(30, 30);
            Item.accessory = true;
            Item.value = Item.buyPrice(copper: 0);
            Item.rare = ItemRarityID.White;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int bonusDamage = SkillUtilities.DisplaySkillBonusText(200f, 0.01f);
            int bonusCrit = SkillUtilities.DisplaySkillBonusText(200f, 0.01f);
            int bonusFireRate = SkillUtilities.DisplaySkillBonusText(200f, 0.01f);
            int bonusLife = SkillUtilities.DisplaySkillBonusText(200f, 0.01f);

            ItemText.Text(tooltips, Mod, "Tooltip1", "Increases your Ranged Damage, Crit Damage, Fire Rate, and Maximum Health");
            ItemText.Text(tooltips, Mod, "Tooltip2", "Bonuses increase as you progress", ItemText.VaultarianColours.Information);
            ItemText.Text(tooltips, Mod, "Tooltip3", $"+{bonusDamage}% Ranged Damage");
            ItemText.Text(tooltips, Mod, "Tooltip4", $"+{bonusCrit}% Crit Damage");
            ItemText.Text(tooltips, Mod, "Tooltip5", $"+{bonusFireRate}% Fire Rate");
            ItemText.Text(tooltips, Mod, "Tooltip6", $"+{bonusLife}% Maximum Health");
        }
    }
}