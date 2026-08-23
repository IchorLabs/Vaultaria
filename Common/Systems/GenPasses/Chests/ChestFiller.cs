using Terraria;
using Terraria.ID;

namespace Vaultaria.Common.Systems.GenPasses.Chests
{
    public static class ChestFiller
    {
        public static void PlaceItemsInChest(int[] itemsToPlaceInChest, int itemsToPlaceInChestChoice, int itemsPlaced, int maxItems, int internalChestID)
        {
			// Loop over all the chests
			for (int chestIndex = 0; chestIndex < Main.maxChests; chestIndex++)
            {
				Chest chest = Main.chest[chestIndex];

				if (chest == null)
                {
					continue;
				}

				Tile chestTile = Main.tile[chest.x, chest.y];
				// We need to check if the current chest is the Frozen Chest. We need to check that it exists and has the TileType and TileFrameX values corresponding to the Frozen Chest.
				// If you look at the sprite for Chests by extracting Tiles_21.xnb, you'll see that the 12th chest is the Frozen Chest. Since we are counting from 0, this is where 11 comes from. 36 comes from the width of each tile including padding. An alternate approach is to check the wiki and looking for the "Internal Tile ID" section in the infobox: https://terraria.wiki.gg/wiki/Frozen_Chest
				if (chestTile.TileType == TileID.Containers && chestTile.TileFrameX == internalChestID * 36)
                {
					// We have found a Frozen Chest
					// If we don't want to add one of the items to every Frozen Chest, we can randomly skip this chest with a 33% chance.
					// if (WorldGen.genRand.NextBool(3))
                    // {
					// 	continue;
                    // }

					// Next we need to find the first empty slot for our item
					for (int inventoryIndex = 0; inventoryIndex < Chest.maxItems; inventoryIndex++)
                    {
						if (chest.item[inventoryIndex].type == ItemID.None)
                        {
							// Place the item
							chest.item[inventoryIndex].SetDefaults(itemsToPlaceInChest[itemsToPlaceInChestChoice]);
							// Decide on the next item that will be placed.
							itemsToPlaceInChestChoice = (itemsToPlaceInChestChoice + 1) % itemsToPlaceInChest.Length;
							// Alternate approach: Random instead of cyclical: chest.item[inventoryIndex].SetDefaults(WorldGen.genRand.Next(itemsToPlaceInChest));
							itemsPlaced++;
							break;
						}
					}
				}

				// Once we've placed as many items as we wanted, break out of the loop
				if (itemsPlaced >= maxItems)
                {
					break;
				}
			}
        }
    }
}