using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Vaultaria.Common.Utilities;
using System.Collections.Generic;
using Vaultaria.Content.Items.Materials;
using Vaultaria.Content.Buffs.SkillEffects;

namespace Vaultaria.Content.Items.Accessories.Skills
{
    public class ViolentSpeed : ModSkill
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
            int bonusSpeed = SkillUtilities.DisplaySkillBonusText(40f, 0.05f);

            ItemText.Text(tooltips, Mod, "Tooltip1", "Killing an enemy increases your Movement Speed for 8 seconds");
            ItemText.Text(tooltips, Mod, "Tooltip2", "Bonuses increase as you progress", ItemText.VaultarianColours.Information);
            ItemText.Text(tooltips, Mod, "Tooltip3", $"+{bonusSpeed}% Movement Speed");
            ItemText.Text(tooltips, Mod, "Tooltip4", "Found in Frozen Chests", ItemText.VaultarianColours.Information);
        }
    }
}