using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;
using Vaultaria.Common.Configs;
using System.Collections.Generic;

namespace Vaultaria.Common.GlobalItems
{
    public class AmmoDropRule : IItemDropRule
    {
        // Not used
        List<IItemDropRuleChainAttempt> IItemDropRule.ChainedRules => new List<IItemDropRuleChainAttempt>();

        public int itemType;
        public int chanceDenominator;
        public int min;
        public int max;

        public AmmoDropRule(int itemType, int chanceDenominator, int min, int max)
        {
            this.itemType = itemType;
            this.chanceDenominator = chanceDenominator;
            this.min = min;
            this.max = max;
        }

        // Reads AmmoDropRateMultiplier live so the drop rate reacts to config changes instead of being baked in at load
        public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
        {
            ItemDropAttemptResult result = default(ItemDropAttemptResult);

            VaultariaConfig config = ModContent.GetInstance<VaultariaConfig>();

            if (config.AmmoDropRateMultiplier <= 0f)
            {
                result.State = ItemDropAttemptResultState.FailedRandomRoll;
                return result;
            }

            float adjustedChance = (1f / chanceDenominator) * config.AmmoDropRateMultiplier;

            if (info.rng.Next(1000000) < adjustedChance * 1000000)
            {
                int amount = info.rng.Next(min, max + 1);
                CommonCode.DropItem(info, itemType, amount);

                result.State = ItemDropAttemptResultState.Success;
                return result;
            }

            result.State = ItemDropAttemptResultState.FailedRandomRoll;
            return result;
        }

        // Not Used
        public bool CanDrop(DropAttemptInfo info) { return true; }

        // Not Used
        public void ReportDroprates(List<DropRateInfo> a, DropRateInfoChainFeed b) {}
    }
}
