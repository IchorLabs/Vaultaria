using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Vaultaria.Common.Utilities;
using System.Collections.Generic;
using Vaultaria.Content.Items.Materials;

namespace Vaultaria.Content.Items.Accessories.Skills
{
    public class CloudOfLead : ModSkill
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.Size = new Vector2(30, 30);
            Item.accessory = true;
            Item.value = Item.buyPrice(silver: 1);
            Item.rare = ItemRarityID.Green;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int bonusShot = (int) CloudOfLeadCounter();

            ItemText.Text(tooltips, Mod, "Tooltip1", "Every Nth shot from you shoots an Incendiary bullet and won't consume ammo", ItemText.VaultarianColours.Incendiary);
            ItemText.Text(tooltips, Mod, "Tooltip2", "Bonuses increase as you progress", ItemText.VaultarianColours.Information);
            ItemText.Text(tooltips, Mod, "Tooltip3", $"Triggers every {bonusShot}th shot");
            ItemText.Text(tooltips, Mod, "Tooltip4", "Found in Golden Chests", ItemText.VaultarianColours.Information);
        }
    
        private float CloudOfLeadCounter()
        {
            float numberOfBossesDefeated = SkillUtilities.DownedBossCounter();

            if(numberOfBossesDefeated > 25)
            {
                return 4;
            }
            else if(numberOfBossesDefeated > 19)
            {
                return 5;
            }
            else if(numberOfBossesDefeated > 13)
            {
                return 6;
            }
            else if(numberOfBossesDefeated > 7)
            {
                return 7;
            }
            else if(numberOfBossesDefeated > 1)
            {
                return 8;
            }
            else
            {
                return 9;
            }
        }
    }
}