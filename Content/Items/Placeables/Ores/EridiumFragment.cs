using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using EridiumOreTile = Vaultaria.Content.Items.Tiles.Ores.EridiumOre;

namespace Vaultaria.Content.Items.Placeables.Ores
{
    public class EridiumFragment : ModItem
    {
        public override void SetStaticDefaults() {
            Item.ResearchUnlockCount = 100;
            ItemID.Sets.SortingPriorityMaterials[Type] = 58;
        }

        public override void SetDefaults() {
            Item.DefaultToPlaceableTile(ModContent.TileType<EridiumOreTile>());
            Item.width = 12;
            Item.height = 12;
            Item.value = 3000;
            Item.rare = ItemRarityID.Purple;
        }
    }
}