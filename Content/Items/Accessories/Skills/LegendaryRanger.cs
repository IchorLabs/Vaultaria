using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Vaultaria.Common.Utilities;
using System.Collections.Generic;
using Vaultaria.Content.Items.Materials;

namespace Vaultaria.Content.Items.Accessories.Skills
{
    public class LegendaryRanger : ModSkill
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
            int ranger = SkillUtilities.DisplaySkillBonusText(300f, 0.01f);
            int rangerDamage = SkillUtilities.DisplaySkillBonusText(300f, 0.01f) + SkillUtilities.DisplaySkillBonusText(80f, 0.05f);
            int bonusMelee = SkillUtilities.DisplaySkillBonusText(100f, 0.05f);
            int bonusRegen = (int) (Main.LocalPlayer.statLifeMax2 * 0.01f);
            int bonusDamage = SkillUtilities.DisplaySkillBonusText(50f, 0.05f);
            int bonusSpeed = SkillUtilities.DisplaySkillBonusText(30f, 0.1f);
            int bonusFireRate = SkillUtilities.DisplaySkillBonusText(20f, 0.1f);

            ItemText.Text(tooltips, Mod, "Tooltip1", "Gives all the previous bonuses in one Class Mod");
            ItemText.Text(tooltips, Mod, "Tooltip1", "Increases your Ranged Damage, Melee Damage, Crit Damage, Fire Rate, and Maximum Health");
            ItemText.Text(tooltips, Mod, "Tooltip1", "Killing an enemy grants you Health Regeneration for 7 seconds", ItemText.VaultarianColours.Healing);
            ItemText.Text(tooltips, Mod, "Tooltip1", "Killing an enemy increases your Ranged Damage and Movement Speed for 7 seconds");
            ItemText.Text(tooltips, Mod, "Tooltip1", "Killing an enemy increases your Ranged Fire Rate for 7 seconds");
            ItemText.Text(tooltips, Mod, "Tooltip3", $"+{rangerDamage}% Ranged Damage");
            ItemText.Text(tooltips, Mod, "Tooltip3", $"+{bonusMelee}% Melee Damage");
            ItemText.Text(tooltips, Mod, "Tooltip4", $"+{ranger}% Crit Damage");
            ItemText.Text(tooltips, Mod, "Tooltip5", $"+{ranger}% Fire Rate");
            ItemText.Text(tooltips, Mod, "Tooltip6", $"+{ranger}% Maximum Health");
            ItemText.Text(tooltips, Mod, "Tooltip3", $"+{bonusRegen}% Health Regeneration on kill");
            ItemText.Text(tooltips, Mod, "Tooltip3", $"+{bonusDamage}% Ranged Damage on kill");
            ItemText.Text(tooltips, Mod, "Tooltip4", $"+{bonusSpeed}% Movement Speed on kill");
            ItemText.Text(tooltips, Mod, "Tooltip3", $"+{bonusFireRate}% Fire Rate on kill");
            ItemText.RedText(tooltips, Mod, "Wanna know why I got these scars?");
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Eridium>(100)
                .AddIngredient(ItemID.LunarBar, 50)
                .AddIngredient(ItemID.FragmentVortex, 100)
                .AddIngredient<Ranger>(1)
                .AddIngredient<QuickCharge>(1)
                .AddIngredient<Onslaught>(1)
                .AddIngredient<Impact>(1)
                .AddIngredient<MetalStorm>(1)
                .AddTile(ModContent.TileType<Tiles.VendingMachines.ZedVendingMachine>())
                .Register();
        }
    }
}