using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Vaultaria.Common.Utilities;
using System.Collections.Generic;
using Vaultaria.Content.Items.Materials;
using Vaultaria.Common.Configs;

namespace Vaultaria.Content.Items.Accessories.Skills
{
    public class TerminationProtocols : ModSkill
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.Size = new Vector2(30, 30);
            Item.accessory = true;
            Item.value = Item.buyPrice(silver: 1);
            Item.rare = ItemRarityID.Blue;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            VaultariaConfig config = ModContent.GetInstance<VaultariaConfig>();
            Player player = Main.LocalPlayer;
            float explosionDamage = player.statDefense * 4f;

            if(config.VaultHunterMode == 3)
            {
                explosionDamage *= 2f;
            }
            else if(config.VaultHunterMode == 2)
            {
                explosionDamage *= 1.5f;
            }

            if(Main.masterMode)
            {
                explosionDamage *= 3f;
            }
            else if(Main.expertMode)
            {
                explosionDamage *= 2f;
            }

            ItemText.Text(tooltips, Mod, "Tooltip1", "On death, create a large explosion that is equal to your defense * 4", ItemText.VaultarianColours.Explosive);
            ItemText.Text(tooltips, Mod, "Tooltip2", "Damage is also scaled based on chosen difficulty", ItemText.VaultarianColours.Information);
            ItemText.Text(tooltips, Mod, "Tooltip3", $"Current Termination Damage = {explosionDamage}");
            ItemText.RedText(tooltips, Mod, "You Willed Kilhelm...?");
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Eridium>(10)
                .AddIngredient(ItemID.SilverBar, 12)
                .AddIngredient(ItemID.Bomb, 100)
                .AddIngredient(ItemID.Grenade, 100)
                .AddIngredient(ItemID.Dynamite, 100)
                .AddTile(ModContent.TileType<Tiles.VendingMachines.ZedVendingMachine>())
                .Register();

            CreateRecipe()
                .AddIngredient<Eridium>(10)
                .AddIngredient(ItemID.TungstenBar, 12)
                .AddIngredient(ItemID.Bomb, 100)
                .AddIngredient(ItemID.Grenade, 100)
                .AddIngredient(ItemID.Dynamite, 100)
                .AddTile(ModContent.TileType<Tiles.VendingMachines.ZedVendingMachine>())
                .Register();
        }
    }
}