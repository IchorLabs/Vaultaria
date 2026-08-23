using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Vaultaria.Common.Utilities;
using System.Collections.Generic;
using Vaultaria.Content.Items.Materials;

namespace Vaultaria.Content.Items.Accessories.Skills
{
    public class MetalStorm : ModSkill
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.Size = new Vector2(30, 30);
            Item.accessory = true;
            Item.value = Item.buyPrice(copper: 0);
            Item.rare = ItemRarityID.Green;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int bonusFireRate = SkillUtilities.DisplaySkillBonusText(20f, 0.1f);

            ItemText.Text(tooltips, Mod, "Tooltip1", "Killing an enemy increases your Ranged Fire Rate for 7 seconds");
            ItemText.Text(tooltips, Mod, "Tooltip2", "Bonuses increase as you progress", ItemText.VaultarianColours.Information);
            ItemText.Text(tooltips, Mod, "Tooltip3", $"+{bonusFireRate}% Fire Rate");
        }
    }
}