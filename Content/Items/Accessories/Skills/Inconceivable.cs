using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Vaultaria.Common.Utilities;
using System.Collections.Generic;
using Vaultaria.Content.Items.Materials;

namespace Vaultaria.Content.Items.Accessories.Skills
{
    public class Inconceivable : ModSkill
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
            int bonusShot = SkillUtilities.DisplayComparativeBonusText(1.2f) + SkillUtilities.DisplaySkillBonusText(300f, 0.05f);

            ItemText.Text(tooltips, Mod, "Tooltip1", "You have a chance to not consume any ammo when shooting\nThe lower your health, the greater the bonus");
            ItemText.Text(tooltips, Mod, "Tooltip2", "Bonuses increase as you progress", ItemText.VaultarianColours.Information);
            ItemText.Text(tooltips, Mod, "Tooltip3", $"Up to +{bonusShot}% Free Ammo Chance");
            ItemText.Text(tooltips, Mod, "Tooltip4", "Found in Wooden Chests", ItemText.VaultarianColours.Information);
        }
    }
}