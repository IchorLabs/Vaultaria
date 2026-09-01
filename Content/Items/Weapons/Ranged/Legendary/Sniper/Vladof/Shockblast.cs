using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Items.Materials;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;
using Vaultaria.Content.Projectiles.Ammo.Legendary.Sniper.Vladof;

namespace Vaultaria.Content.Items.Weapons.Ranged.Legendary.Sniper.Vladof
{
    public class Shockblast : VaultarianItem
    {
        protected override Sounds[] ItemSounds => new[] { Sounds.ETechSniperSingle };

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
            ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            // Visual properties
            Item.Size = new Vector2(60, 20);
            Item.scale = 0.9f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.LightPurple;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 10f;
            Item.shoot = ModContent.ProjectileType<ShockblastElBullet>();
            Item.mana = 12;

            // Combat properties
            Item.knockBack = 1f;
            Item.damage = 70;
            Item.crit = 0;
            Item.DamageType = DamageClass.Magic;

            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.reuseDelay = 15;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(gold: 5);
            SetItemSound(Item, Sounds.ETechSniperSingle, 60);
        }

        public override bool AltFunctionUse(Player player)
        {
            ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2) // Right-click
            {
                Item.DamageType = DamageClass.Magic;
                Item.useStyle = ItemUseStyleID.Shoot;
                Item.knockBack = 2.3f;
                Item.noMelee = true;
                Item.shootSpeed = 10f;
                Item.shoot = ModContent.ProjectileType<ShockblastExBullet>();
                Item.mana = 30;
                SetItemSound(Item, Sounds.ETechLauncher, 60);

                Item.damage = 200;
                Item.crit = 0;
                Item.useTime = 30;
                Item.useAnimation = 30;
                Item.reuseDelay = 30;
                Item.autoReuse = true;
                Item.useTurn = false;
            }
            else // Left-click
            {
                Item.DamageType = DamageClass.Magic;
                Item.useStyle = ItemUseStyleID.Shoot;
                Item.noMelee = true;
                Item.shootSpeed = 10f;
                Item.shoot = ModContent.ProjectileType<ShockblastElBullet>();
                Item.mana = 12;
                SetItemSound(Item, Sounds.ETechSniperSingle, 60);

                Item.damage = 70;
                Item.crit = 0;
                Item.useTime = 10;
                Item.useAnimation = 10;
                Item.reuseDelay = 0;
                Item.autoReuse = true;
                Item.useTurn = false;
            }

            return base.CanUseItem(player);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
            return false;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-15f, 0f);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            ItemText.Text(tooltips, Mod, "tooltip1", "Uses 12 mana per left-click or 30 mana per right-click");
            ItemText.Text(tooltips, Mod, "tooltip2", "Left-click to shoot fast Shock e-tech rounds", ItemText.VaultarianColours.Shock);
            ItemText.Text(tooltips, Mod, "tooltip3", "Right-click to shoot more powerful Explosive-Shock rounds", ItemText.VaultarianColours.Explosive);
            ItemText.RedText(tooltips, Mod, "Blast them to smithereens!");
            ItemText.CursedText(tooltips, Mod, "Exodus");
        }
    }
}
