using Terraria.ModLoader;
using Terraria;
using Vaultaria.Common.Utilities;
using System.Linq;
using Terraria.Graphics.Effects;

namespace Vaultaria.Common.Systems
{
    public class ContentLoadingSystem : ModSystem
    {
        // This hook runs after all content (including items) is fully loaded.
        public override void PostSetupContent()
        {
            var filteredItems = Mod.GetContent<ModItem>().Where(modItem => modItem.GetType().Namespace.Contains("Vaultaria.Content.Items.Weapons.Ranged.Legendary"));

            // Get all items registered specifically in this mod.
            // GetContent<ModItem>() returns an IEnumerable<ModItem>.
            foreach (ModItem modItem in filteredItems)
            {
                Utilities.Utilities.gunGunItemArray.Add(modItem);
            }
        }
    }
}