using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Vaultaria.Common.Utilities;
using System.Collections.Generic;
using Vaultaria.Common.Configs;

namespace Vaultaria.Content.Items.Consumables.Bags
{
    public class Milkshake : GlobalItem
    {
        public override void HoldItem(Item item, Player player)
        {
            base.HoldItem(item, player);

            MilkshakeSound(item);
        }

        private static void MilkshakeSound(Item entity)
        {
            VaultariaConfig config = ModContent.GetInstance<VaultariaConfig>();

            if(entity.type == ItemID.Milkshake && config.DisableMilkshakeVoiceLine == false)
            {
                VaultarianItem.SetItemSound(entity, VaultarianItem.Sounds.RolandsMilkshakes, 420);
            }
            else if (entity.type == ItemID.Milkshake)
            {
                entity.UseSound = SoundID.Item3;
            }
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            base.ModifyTooltips(item, tooltips);

            if(item.type == ItemID.Milkshake)
            {
                ItemText.RedText(tooltips, Mod, "Hey buddy, it's me Roland. Lets kill Handsome Jack, and then we'll all go out for milkshakes.");
            }
        }
    }
}