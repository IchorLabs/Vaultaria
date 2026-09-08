//using Terraria;
//using Terraria.ID;
//using Terraria.ModLoader;
//using MoonstoneDepositTile = Vaultaria.Content.Items.Tiles.Ores.MoonstoneDeposit;

//namespace Vaultaria.Content.Items.Placeables.Ores
//{
//   public class MoonstoneShard : ModItem
//    {
//        public override void SetStaticDefaults() {
//            Item.ResearchUnlockCount = 100;
//            ItemID.Sets.SortingPriorityMaterials[Type] = 58;
//        }
//
//        public override void SetDefaults() {
//            Item.DefaultToPlaceableTile(ModContent.TileType<MoonstoneDepositTile>());
//            Item.width = 12;
//            Item.height = 12;
//            Item.value = 3000;
//        }
//    }
//}