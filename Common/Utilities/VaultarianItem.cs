using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace Vaultaria.Common.Utilities
{
    public abstract class VaultarianItem : ModItem
    {
        // virtual: This is a "permission" keyword. It tells the compiler: "This property has a default value (null), but child classes are allowed to change it."
        // => null: This is an expression-bodied getter. It is shorthand for get { return null; }. It ensures that by default, an item has no randomized sounds.
        protected virtual Sounds[]? ItemSounds => null;

        public virtual bool UsesCustomMuzzlePosition => false;

        public enum Sounds
        {
            LegendaryDrop,
            DigiCloneSpawn,
            DigiCloneSwap,
            Norfleet,
            Boomacorn,
            Execute,
            RolandsMilkshakes,
            GenericLaser,
            BanditAR,
            BanditARRocket,
            BanditLauncher,
            BanditPistol,
            BanditShotgun,
            BanditSMG,
            Bane,
            DahlARBurst,
            DahlARSingle,
            DahlPistolBurst,
            DahlPistolSingle,
            DahlSMGBurst,
            LascauxBurst,
            DahlSMGSingle,
            DahlSniperBurst,
            DahlSniperSingle,
            Deception,
            ETechARBurst,
            ETechARSingle,
            ETechLauncher,
            ETechPistolBurst,
            ETechPistolSingle,
            ETechSMGBurst,
            ETechSMGSingle,
            ETechShotgun,
            ETechSniperBurst,
            ETechSniperSingle,
            PlasmaCoil,
            HyperionLaser,
            HyperionPistol,
            HyperionShotgun,
            HyperionSMG,
            HyperionSniper,
            JakobsPistol,
            JakobsAR,
            JakobsShotgun,
            JakobsSniper,
            MaliwanContinuousLaser,
            MaliwanLaserSingle,
            MaliwanLauncher,
            MaliwanPistol,
            MaliwanSMG,
            MaliwanSniper,
            PhaselockBase,
            PhaselockRuin,
            TedioreLaser,
            TedioreLaserThrow,
            LaserDisker,
            TedioreLauncher,
            TedioreLauncherThrow,
            TediorePistol,
            TediorePistolThrow,
            TedioreShotgun,
            TedioreShotgunThrow,
            TedioreSMG,
            TedioreSMGThrow,
            TorgueAR,
            TorgueLauncher,
            TorguePistol,
            TorgueShotgun,
            VladofAR,
            VladofARRocket,
            VladofLauncher,
            VladofPistol,
            VladofSniper,
            BiggSuccVariation1,
            BiggSuccVariation2,
            BiggSuccVariation3,
            BiggSuccVariation4,
            BiggSuccVariation5,
        }

        public override bool? UseItem(Player player)
        {
            if(ItemSounds != null) // Needs a null check cause the array itself is null
            {
                SoundVariator(Item, ItemSounds);
            }

            return base.UseItem(player);
        }

        private static void SoundVariator(Item item, Sounds[] sounds, Sounds fallBackSound = Sounds.BanditPistol, int instances = 60)
        {
            if(sounds == null || sounds.Length == 0) // If the array doesn't exist at all (which technically shouldn't happen since its initialized as null immediately above) OR if the array exists and is empty
            {
                if(item.UseSound != null) // If the item's native sound isn't null, then use that sound as the fallback
                {
                    SetItemSound(item, item.UseSound);
                }
                else // If the array is null and the item doesn't have a native sound, then use a fallback sound. The fallback defaults to BanditPistol if it isn't defined
                {
                    SetItemSound(item, fallBackSound, instances);
                }

                return; // Immediately return to not do the next stuff
            }

            int chosenSound = Main.rand.Next(sounds.Length); // If the array does have values, then get a random sound from the array
            SetItemSound(item, sounds[chosenSound], instances); // Now set the item's sound to that random value whenever the item is used
        }

        public static void SetItemSound(Item item, Sounds sound, int instances = 60)
        {
            float pitch = GetRandomPitch(); // Apply slight pitch variation for realistic weapon sound variation
            item.UseSound = new SoundStyle($"Vaultaria/Common/Sounds/{sound}") 
            {
                // Allow up to 60 concurrent instances of the sound to play. 
                // This makes fast firing sound layered and prevents harsh cutoffs.
                MaxInstances = instances,
                Pitch = pitch
            };
        }

        private static float GetRandomPitch()
        {
            // Subtle pitch variations (±7%) for realistic weapon sound variation
            // This mimics how real weapons have slight tonal differences on each shot
            int variation = Main.rand.Next(3); // 0 = normal, 1 = pitch up, 2 = pitch down
            return variation switch
            {
                1 => 0.07f,  // Slightly pitched up
                2 => -0.07f, // Slightly pitched down
                _ => 0f      // Normal pitch
            };
        }

        private static void SetItemSound(Item item, SoundStyle? sound)
        {
            item.UseSound = sound;
        }
    }
}