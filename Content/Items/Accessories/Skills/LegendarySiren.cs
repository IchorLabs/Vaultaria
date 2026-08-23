using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Vaultaria.Common.Utilities;
using System.Collections.Generic;
using Vaultaria.Content.Items.Materials;

namespace Vaultaria.Content.Items.Accessories.Skills
{
    public class LegendarySiren : ModSkill
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.Size = new Vector2(30, 30);
            Item.accessory = true;
            Item.value = Item.buyPrice(gold: 1);
            Item.rare = ItemRarityID.Master;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int bonusDamage = SkillUtilities.DisplaySkillBonusText(100f, 0.05f);
            int bonusProjectileSpeed = SkillUtilities.DisplaySkillBonusText(80f, 0.05f);
            int bonusFireRate = SkillUtilities.DisplaySkillBonusText(45f, 0.05f);
            int bonusPhaselockDamage = SkillUtilities.DisplaySkillBonusText(80f, 0.05f);
            int bonusReuseDelay = SkillUtilities.DisplaySkillBonusText(80f, 0.05f);
            int bonusReaperDamage = SkillUtilities.DisplaySkillBonusText(35f, 0.05f);

            ItemText.Text(tooltips, Mod, "Tooltip1", "Gives all the previous bonuses in one Class Mod");
            ItemText.Text(tooltips, Mod, "Tooltip2", "Increases your Magic Damage and Projectile Speed");
            ItemText.Text(tooltips, Mod, "Tooltip3", "Increases your Magic Fire Rate");
            ItemText.Text(tooltips, Mod, "Tooltip4", "You deal increased Magic Damage to enemies above 50% Health");
            ItemText.Text(tooltips, Mod, "Tooltip5", "While an enemy is Phaselocked you gain increased Fire Rate and Damage for Magic weapons");
            ItemText.Text(tooltips, Mod, "Tooltip6", $"+{bonusDamage}% Magic Damage");
            ItemText.Text(tooltips, Mod, "Tooltip7", $"+{bonusProjectileSpeed}% Projectile Speed");
            ItemText.Text(tooltips, Mod, "Tooltip8", $"+{bonusReuseDelay}% Fire Rate");
            ItemText.Text(tooltips, Mod, "Tooltip9", $"+{bonusReaperDamage}% Magic Damage while your target is above 50% health");
            ItemText.Text(tooltips, Mod, "Tooltip10", $"+{bonusFireRate}% Fire Rate while Phaselock is active");
            ItemText.Text(tooltips, Mod, "Tooltip11", $"+{bonusPhaselockDamage}% Magic Damage while Phaselock is active");
            ItemText.Text(tooltips, Mod, "Tooltip12", $"Gain +45% Movement Speed while Phaselock is active");
            ItemText.RedText(tooltips, Mod, "(giggles) I'm really good at this!");
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Eridium>(100)
                .AddIngredient(ItemID.LunarBar, 50)
                .AddIngredient(ItemID.FragmentNebula, 100)
                .AddIngredient<Accelerate>(1)
                .AddIngredient<Wreck>(1)
                .AddIngredient<Foresight>(1)
                .AddIngredient<Reaper>(1)
                .AddTile(ModContent.TileType<Tiles.VendingMachines.ZedVendingMachine>())
                .Register();
        }
    }
}