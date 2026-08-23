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
    public class DesperateMeasures : ModSkill
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
            int bonusWhip = SkillUtilities.DisplayComparativeBonusText(2.7f) + SkillUtilities.DisplaySkillBonusText(46f, 0.05f);
            int bonusSummon = SkillUtilities.DisplayComparativeBonusText(2.7f) + SkillUtilities.DisplaySkillBonusText(46f, 0.05f);

            ItemText.Text(tooltips, Mod, "Tooltip1", "Increases your Whip and Summon Damage. The lower your Health the greater this bonus");
            ItemText.Text(tooltips, Mod, "Tooltip2", "Bonuses increase as you progress", ItemText.VaultarianColours.Information);
            ItemText.Text(tooltips, Mod, "Tooltip3", $"Up to +{bonusWhip}% Whip Damage");
            ItemText.Text(tooltips, Mod, "Tooltip4", $"Up to +{bonusSummon}% Summon Damage");
        }
    }
}