using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Vaultaria.Common.Utilities;
using System.Collections.Generic;
using Vaultaria.Content.Items.Materials;

namespace Vaultaria.Content.Items.Accessories.Skills
{
    public class Redistribution : ModSkill
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.Size = new Vector2(30, 30);
            Item.accessory = true;
            Item.value = Item.buyPrice(gold: 1);
            Item.rare = ItemRarityID.Blue;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            ItemText.Text(tooltips, Mod, "Tooltip1", "Scoring a Critical Hit with a Ranged weapon grants Health and Ammo Regeneration for that weapon for 3 seconds");
            ItemText.Text(tooltips, Mod, "Tooltip2", $"+1% Health Regen");
            ItemText.Text(tooltips, Mod, "Tooltip3", $"Ammo Regen is granted on every shot while the buff is active");
        }
    }
}