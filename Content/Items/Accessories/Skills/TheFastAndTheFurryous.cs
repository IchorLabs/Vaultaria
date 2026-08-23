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
    public class TheFastAndTheFurryous : ModSkill
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
            int bonusWhip = SkillUtilities.DisplaySkillBonusText(150f, 0.05f);
            int bonusSummon = SkillUtilities.DisplaySkillBonusText(120f, 0.05f);
            int bonusSpeed = SkillUtilities.DisplaySkillBonusText(170f, 0.025f);

            ItemText.Text(tooltips, Mod, "Tooltip1", "While above 50% Health, you gain increased Whip Damage, Summon Damage, and Movement Speed");
            ItemText.Text(tooltips, Mod, "Tooltip2", "Bonuses increase as you progress", ItemText.VaultarianColours.Information);
            ItemText.Text(tooltips, Mod, "Tooltip3", $"+{bonusWhip}% Whip Damage");
            ItemText.Text(tooltips, Mod, "Tooltip4", $"+{bonusSummon}% Summon Damage");
            ItemText.Text(tooltips, Mod, "Tooltip5", $"+{bonusSpeed}% Movement Speed");
        }
    }
}