using Terraria;

namespace Vaultaria.Common.Utilities
{
    public static class SkillUtilities
    {
        // A helper method that tracks what bosses have been defeated
        public static float DownedBossCounter()
        {
            float counter = 0f;
            
            // --- Pre-Hardmode Bosses ---
            if (NPC.downedSlimeKing) // 1
            {
                counter++;
            }
            if (NPC.downedBoss1) // Eye of Cthulhu 2
            {
                counter++;
            }
            if (NPC.downedBoss2) // EoW / BoC 3
            {
                counter++;
            }
            if (NPC.downedQueenBee) // 4
            {
                counter++;
            }
            if (NPC.downedDeerclops) // 5
            {
                counter++;
            }
            if (NPC.downedBoss3) // Skeletron 6
            {
                counter++;
            }
            if (Main.hardMode) // 7
            {
                counter++; // Wall of Flesh is tracked by Main.hardMode
            }

            // --- Hardmode Bosses ---
            if (NPC.downedQueenSlime) // 8
            {
                counter++;
            }
            if (NPC.downedMechBoss1) // The Destroyer 9
            {
                counter++;
            }
            if (NPC.downedMechBoss2) // The Twins 10 
            {
                counter++;
            }
            if (NPC.downedMechBoss3) // Skeletron Prime 11
            {
                counter++;
            }
            if (NPC.downedPlantBoss) // 12
            {
                counter++;
            }
            if (NPC.downedGolemBoss) // 13
            {
                counter++;
            }
            if (NPC.downedFishron) // 14
            {
                counter++;
            }
            if (NPC.downedEmpressOfLight) // 15
            {
                counter++;
            }

            // --- Lunar and Final Bosses ---
            if (NPC.downedAncientCultist) // 16
            {
                counter++;
            }
            if (NPC.downedTowerSolar) // 17
            {
                counter++;
            }
            if (NPC.downedTowerVortex) // 18
            {
                counter++;
            }
            if (NPC.downedTowerNebula) // 19
            {
                counter++;
            }
            if (NPC.downedTowerStardust) // 20
            {
                counter++;
            }
            if (NPC.downedMoonlord) // 21
            {
                counter++;
            }
            
            // --- Invasions and Events ---
            if (NPC.downedFrost) // 22
            {
                counter++;
            }
            if (NPC.downedGoblins) // 23
            {
                counter++;
            }
            if (NPC.downedMartians) // 24
            {
                counter++;
            }
            if (NPC.downedPirates) // 25
            {
                counter++;
            }
            
            if (NPC.downedChristmasTree) // 26
            {
                counter++;
            }
            if (NPC.downedChristmasSantank) // 27
            {
                counter++;
            }
            if (NPC.downedChristmasIceQueen) // 28
            {
                counter++;
            }
            if (NPC.downedHalloweenTree) // 29
            {
                counter++;
            }
            if (NPC.downedHalloweenKing) // 30
            {
                counter++;
            }

            // --- Additional Flag ---
            if (NPC.downedClown) // 31
            {
                counter++;
            }

            return counter;
        }

        /// <summary>
        /// Calculates a bonus based on the difference between 2 values
        /// <br/> The full formula is:
        /// <br/> (((highestValue - currentValue) / highestValue) / divisor)
        /// <br/> First it gets highest number (Player.statLifeMax2) then it takes away the current value (), and then divides it by the highest number again so that it becomes within the 100% area instead of being 1000%. Then it divides it by the divisor for scaling purposes.
        /// <br/> divisor = The number that balances the item.
        /// </summary>
        public static float ComparativeBonus(float highestValue, float currentValue, float divisor)
        {
            float value = highestValue - currentValue;

            if(value == 0)
            {
                value = 1;
            }
            if(value < 0)
            {
                value *= -1;
            }

            float bonus = (value / highestValue) / divisor;

            if(bonus < 0)
            {
                bonus *= -1;
            }

            return bonus;
        }

        /// <summary>
        /// Calculates a bonus based on the difference between 2 values
        /// <br/> The full formula is:
        /// <br/> (int) (100f * (1 / divisor))
        /// <br/> Since it just shows what it can be UP TO, then for the sake of showing what the highest damage is, the highestValue - currentValue would just be the highestValue, and since it's divided against itself, it's just 1. So to skip all that, 1 is placed in the formula. Then it divides against the divisor and multiplies against 100 to get a percentage.
        /// <br/> divisor = The number that balances the item.
        /// </summary>
        public static int DisplayComparativeBonusText(float divisor)
        {
            return (int) (100f * (1 / divisor));
        }

        /// <summary>
        /// Calculates the bonus effect that a skill should provide
        /// <br/> The full formula is:
        /// <br/> 1f + (DownedBossCounter() / divisor) + baseValue
        /// <br/> First it gets the number of bosses that have been killed in the world, then divides it by the number you provide, and then adds the base value on. Then it adds 1 to it so that it becomes greater than 1, so that it can be used to multiply against values when doing calculations (Player.moveSpeed *= bonusSpeed; == Player.moveSpeed *= 1.4f;).
        /// <br/> divisor = The number that balances the item. For example, if I kill every boss and have the counter at 30, and at most this skill should only provide 100% bonus movement speed, then the divisor should be 30, so that 30 / 30 = 1, and 1 is equal to 100% for Terraria calculations.
        /// <br/> baseValue = A base starting value for when no bosses have been killed, but the item should still give an effect.
        /// </summary>
        public static float SkillBonus(float divisor, float baseValue = 0)
        {
            return 1f + (DownedBossCounter() / divisor) + baseValue;
        }

        /// <summary>
        /// Calculates the bonus effect that a skill should provide
        /// <br/> The full formula is:
        /// <br/> (int) (100 * + ((DownedBossCounter() / divisor) + baseValue))
        /// <br/> First it gets the number of bosses that have been killed in the world, then divides it by the number you provide, and then adds the base value on. Then it multiplies it by 100 to get a percentage and converts it to an int to round it.
        /// <br/> divisor = The number that balances the item. For example, if I kill every boss and have the counter at 30, and at most this skill should only provide 100% bonus movement speed, then the divisor should be 30, so that 30 / 30 = 1, and 1 is equal to 100% for Terraria calculations.
        /// <br/> baseValue = A base starting value for when no bosses have been killed, but the item should still give an effect.
        /// </summary>
        public static int DisplaySkillBonusText(float divisor, float baseValue = 0)
        {
            return (int) (100f * ((DownedBossCounter() / divisor) + baseValue));
        }
    }
}