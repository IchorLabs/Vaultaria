using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Vaultaria.Common.Utilities;
using System.Collections.Generic;
using Vaultaria.Content.Items.Materials;

namespace Vaultaria.Content.Items.Accessories.Skills
{
    public class PersonalSpace : ModSkill
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.Size = new Vector2(30, 30);
            Item.accessory = true;
            Item.value = Item.buyPrice(silver: 50);
            Item.rare = ItemRarityID.Blue;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int bonusMagic = SkillUtilities.DisplayComparativeBonusText(1.5f) + SkillUtilities.DisplaySkillBonusText(120f, 0.1f);

            ItemText.Text(tooltips, Mod, "Tooltip1", "You deal Increased Magic Damage the closer you are to your target");
            ItemText.Text(tooltips, Mod, "Tooltip2", "Bonuses increase as you progress", ItemText.VaultarianColours.Information);
            ItemText.Text(tooltips, Mod, "Tooltip3", $"Up to +{bonusMagic}% Magic Damage");
        }
    }
}