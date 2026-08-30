using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;
using Vaultaria.Content.Items.Weapons.Ammo;
using Vaultaria.Content.Projectiles.Ammo.Legendary.SMG.Maliwan;

namespace Vaultaria.Content.Items.Weapons.Ranged.Legendary.SMG.Dahl
{
    public class NightHawkin : VaultarianItem
    {
        protected override Sounds[] ItemSounds => new[] { Sounds.DahlSMGBurst };

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            // Visual properties
            Item.Size = new Vector2(68, 30);
            Item.scale = 0.9f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Yellow;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 20;
            Item.shoot = ProjectileID.Bullet;
            Item.useAmmo = AmmoID.Bullet;

            // Combat properties
            Item.knockBack = 1.5f;
            Item.damage = 25;
            Item.crit = 6;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 3;
            Item.useAnimation = 12;
            Item.reuseDelay = 12;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(gold: 1);
        }

        public override bool CanUseItem(Player player)
        {
            if(Main.hardMode)
            {
                return true;
            }

            return false;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-20f, 2f);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            ItemText.Text(tooltips, Mod);
            ItemText.Text(tooltips, Mod, "Tooltip2", "Shoots Cryo bullets during the day", ItemText.VaultarianColours.Cryo);
            ItemText.Text(tooltips, Mod, "Tooltip3", "Shoots Incendiary bullets at night", ItemText.VaultarianColours.Incendiary);

            if(!Main.hardMode)
            {
                ItemText.Text(tooltips, Mod, "Tooltip4", "Can only be used in Hardmode", ItemText.VaultarianColours.Information);
            }

            ItemText.Text(tooltips, Mod, "Tooltip4", "Found in Web Covered Chests", ItemText.VaultarianColours.Information);

            ItemText.RedText(tooltips, Mod, "Stranger than things.");
        }
    }
}

