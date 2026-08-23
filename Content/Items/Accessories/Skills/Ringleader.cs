using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Vaultaria.Common.Utilities;
using System.Collections.Generic;
using Vaultaria.Content.Items.Materials;

namespace Vaultaria.Content.Items.Accessories.Skills
{
    public class Ringleader : ModSkill
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.Size = new Vector2(30, 30);
            Item.accessory = true;
            Item.value = Item.buyPrice(gold: 1);
            Item.rare = ItemRarityID.Purple;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int bonusFreeze = SkillUtilities.DisplaySkillBonusText(40f, 0.05f);
            int bonusHeal = SkillUtilities.DisplaySkillBonusText(170f, 0.02f);

            ItemText.Text(tooltips, Mod, "Tooltip1", "Gives all the previous bonuses in one Class Mod");
            ItemText.Text(tooltips, Mod, "Tooltip2", "Scoring a Critical Hit has a chance to freeze the enemy for 4 seconds", ItemText.VaultarianColours.Cryo);
            ItemText.Text(tooltips, Mod, "Tooltip3", "Dealing Damage to Frozen enemies heals you", ItemText.VaultarianColours.Healing);
            ItemText.Text(tooltips, Mod, "Tooltip4", $"+{bonusFreeze}% Freeze Chance");
            ItemText.Text(tooltips, Mod, "Tooltip3", $"+{bonusHeal}% Lifesteal");
            ItemText.RedText(tooltips, Mod, "There's a spark between us. Can you feel it?");
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Eridium>(100)
                .AddIngredient(ItemID.ShroomiteBar, 20)
                .AddIngredient(ItemID.SoulofMight, 10)
                .AddIngredient(ItemID.SoulofSight, 10)
                .AddIngredient(ItemID.SoulofFright, 10)
                .AddIngredient<BrainFreeze>(1)
                .AddIngredient<Refreshment>(1)
                .AddTile(ModContent.TileType<Tiles.VendingMachines.ZedVendingMachine>())
                .Register();
        }
    }
}