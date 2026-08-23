using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace Vaultaria.Common.Utilities
{
    public static class ItemText
    {
        // An enum of colours so that there's a centralized space for the below colours so that I don't have to keep making new Color(1,1,1), etc
        public enum VaultarianColours
        {
            Incendiary,
            Shock,
            Corrosive,
            Explosive,
            Slag,
            Cryo,
            Radiation,
            Healing,
            RedText,
            CursedText,
            Information,
            Master,
        }

        // A private dictionary that maps the enum to actual colours
        private static Dictionary<VaultarianColours, Color> vaultarianColours = new Dictionary<VaultarianColours, Color>()
        {
            { VaultarianColours.Incendiary, new Color(231, 92, 22) }, // Orange
            { VaultarianColours.Shock, new Color(46, 153, 228) }, // Blue
            { VaultarianColours.Corrosive, new Color(136, 235, 94) }, // Light Green
            { VaultarianColours.Explosive, new Color(228, 227, 105) }, // Light Yellow
            { VaultarianColours.Slag, new Color(207, 164, 245) }, // Purple
            { VaultarianColours.Cryo, new Color(131, 235, 228) }, // Light Blue
            { VaultarianColours.Radiation, new Color(227, 205, 109) }, // Light Yellow
            { VaultarianColours.Healing, new Color(245, 201, 239) }, // Pink
            { VaultarianColours.RedText, Color.Red }, // Red
            { VaultarianColours.CursedText, new Color(0, 249, 199) }, // Cyan
            { VaultarianColours.Information, new Color(224, 224, 224) }, // Light Grey
            { VaultarianColours.Master, new Color(168, 69, 95) }, // Master
        };

        // The 'this' keyword in the signature allows for it to be used as an extension
        // Turns it from the 1st style below into the 2nd style
        // ItemText.GetVaultarianColor(ItemText.VaultarianColours.Slag);
        // ItemText.VaultarianColours.Shock.GetVaultarianColor();
        public static Color GetVaultarianColor(this VaultarianColours colour)
        {
            if(vaultarianColours.TryGetValue(colour, out Color value))
            {
                return value; // Return the correct colour if the enum colour exists
            }

            return Color.White; // Otherwise return white
        }

        public static void Text(List<TooltipLine> tooltips, Mod mod, string name = "Tooltip1", string tooltip = "Uses any normal bullet type as ammo")
        {
            tooltips.Add(new TooltipLine(mod, name, tooltip));
        }

        public static void Text(List<TooltipLine> tooltips, Mod mod, string name, string tooltip, VaultarianColours colour)
        {
            tooltips.Add(new TooltipLine(mod, name, tooltip)
            {
                OverrideColor = colour.GetVaultarianColor()
            });
        }

        public static void RedText(List<TooltipLine> tooltips, Mod mod, string tooltip)
        {
            tooltips.Add(new TooltipLine(mod, "Red Text", tooltip)
            {
                OverrideColor = VaultarianColours.RedText.GetVaultarianColor()
            });
        }

        public static void CursedText(List<TooltipLine> tooltips, Mod mod, string tooltip)
        {
            tooltips.Add(new TooltipLine(mod, "Curse", tooltip)
            {
                OverrideColor = VaultarianColours.CursedText.GetVaultarianColor()
            });
        }

        public static void MultiShotText(List<TooltipLine> tooltips, Item item, int number)
        {
            TooltipLine damageLine = tooltips.Find(tip => tip.Name == "Damage");

            if (damageLine != null)
            {
                Player player = Main.LocalPlayer;
                int finalDamage = (int)player.GetTotalDamage(item.DamageType).ApplyTo(item.damage);
                damageLine.Text = finalDamage + $" x {number}{item.DamageType.DisplayName}";
            }
        }
    }
}