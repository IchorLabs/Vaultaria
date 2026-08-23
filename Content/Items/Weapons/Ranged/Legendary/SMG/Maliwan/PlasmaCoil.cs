using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;
using Vaultaria.Content.Items.Weapons.Ammo;
using Vaultaria.Content.Projectiles.Ammo.Legendary.SMG.Maliwan;

namespace Vaultaria.Content.Items.Weapons.Ranged.Legendary.SMG.Maliwan
{
    public class PlasmaCoil : VaultarianItem
    {
        protected override Sounds[] ItemSounds => [];

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            // Visual properties
            Item.Size = new Vector2(68, 32);
            Item.scale = 0.9f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Yellow;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 18;
            Item.shoot = ModContent.ProjectileType<RadiationPlasmaCoilBullet>();
            Item.useAmmo = ModContent.ItemType<SubmachineGunAmmo>();

            // Combat properties
            Item.knockBack = 2.3f;
            Item.damage = 40;
            Item.crit = 6;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 4;
            Item.useAnimation = 32;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(gold: 1);
            SetItemSound(Item, Sounds.PlasmaCoil, 120);
        }

        public override bool AltFunctionUse(Player player)
        {
            ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2) // Shoot Cryo
            {
                // Gun properties
                Item.noMelee = true;
                Item.shootSpeed = 18;
                Item.shoot = ModContent.ProjectileType<CryoPlasmaCoilBullet>();
                Item.useAmmo = ModContent.ItemType<SubmachineGunAmmo>();

                // Combat properties
                Item.knockBack = 2.3f;
                Item.damage = 40;
                Item.crit = 6;
                Item.DamageType = DamageClass.Ranged;

                Item.useTime = 4;
                Item.useAnimation = 32;
                Item.autoReuse = true;

                SetItemSound(Item, Sounds.PlasmaCoil, 120);
            }
            else // Shoot Radiation
            {
                // Gun properties
                Item.noMelee = true;
                Item.shootSpeed = 18;
                Item.shoot = ModContent.ProjectileType<RadiationPlasmaCoilBullet>();
                Item.useAmmo = ModContent.ItemType<SubmachineGunAmmo>();

                // Combat properties
                Item.knockBack = 2.3f;
                Item.damage = 40;
                Item.crit = 6;
                Item.DamageType = DamageClass.Ranged;

                Item.useTime = 4;
                Item.useAnimation = 32;
                Item.autoReuse = true;

                SetItemSound(Item, Sounds.PlasmaCoil, 120);
            }

            return base.CanUseItem(player);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-20f, 5f);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            ItemText.Text(tooltips, Mod, "Tooltip1", "Uses SMG Ammo");
            ItemText.Text(tooltips, Mod, "Tooltip2", "Left-Click to shoot a 16-Round Burst of Radiation orbs", ItemText.VaultarianColours.Radiation);
            ItemText.Text(tooltips, Mod, "Tooltip3", "Right-Click to shoot a 16-Round Burst of Cryo orbs", ItemText.VaultarianColours.Cryo);
            ItemText.RedText(tooltips, Mod, "Harness the 4th state of matter.");   
        }
    }
}