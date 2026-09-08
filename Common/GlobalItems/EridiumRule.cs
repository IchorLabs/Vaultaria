using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;
using Vaultaria.Common.Configs;
using Vaultaria.Content.Items.Materials;
using Vaultaria.Content.Items.Placeables.Ores;
using System.Collections.Generic;
using System;

namespace Vaultaria.Common.GlobalItems
{    
    public class EridiumRule : IItemDropRule
    {
        // Not used
        List<IItemDropRuleChainAttempt> IItemDropRule.ChainedRules => new List<IItemDropRuleChainAttempt>();

        public int min;
        public int max;

        // Allows for the drop to be dynamically run on the server so that it isn't just loaded once when the player loads the world on the client side
        public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
        {
            VaultariaConfig config = ModContent.GetInstance<VaultariaConfig>();

            int min = this.min * config.EridiumDropRateMultiplier;
            int max = this.max * config.EridiumDropRateMultiplier;

            int amount = Main.rand.Next(min, max + 1);

            int itemType = info.npc.boss
                ? ModContent.ItemType<Eridium>()
                : ModContent.ItemType<EridiumFragment>();
            CommonCode.DropItem(info, itemType, amount);

            return new ItemDropAttemptResult { State = ItemDropAttemptResultState.Success };
        }

        // Not Used
        public bool CanDrop(DropAttemptInfo info) { return true; }

        // Not Used
        public void ReportDroprates(List<DropRateInfo> a, DropRateInfoChainFeed b) {}
    }

    public class NonBossEridiumCondition : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        public bool CanDrop(DropAttemptInfo info) => info.npc != null && !info.npc.boss;

        public string GetConditionDescription() => "Dropped by non-boss enemies";

        public bool CanShowItemDropInUI() => true;
    }

    public class BossEridiumCondition : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        public bool CanDrop(DropAttemptInfo info) => info.npc != null && info.npc.boss;

        public string GetConditionDescription() => "Dropped by bosses";

        public bool CanShowItemDropInUI() => true;
    }
}