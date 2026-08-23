using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Vaultaria.Common.Utilities;
using System.Collections.Generic;
using Vaultaria.Content.Items.Materials;

namespace Vaultaria.Content.Items.Accessories.Skills
{
    public class HiddenMachine : ModSkill
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
            int bonusDamage = SkillUtilities.DisplaySkillBonusText(60f, 0.05f);
            int number = !Main.hardMode ? 1 : 2; // if not hardmode, then 1, else 2

            ItemText.Text(tooltips, Mod, "Tooltip1", "Your Summons deal increased damage to enemies that you are behind");
            ItemText.Text(tooltips, Mod, "Tooltip2", "Bonuses increase as you progress", ItemText.VaultarianColours.Information);
            ItemText.Text(tooltips, Mod, "Tooltip3", $"+{bonusDamage}% Summon Damage");
            ItemText.Text(tooltips, Mod, "Tooltip4", $"Increases your max number of minions by {number}");
        }
    }
}