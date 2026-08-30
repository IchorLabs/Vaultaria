using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Items.Materials;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;
using Vaultaria.Content.Projectiles.Ammo.Rare.Shotgun.Hyperion;
using Vaultaria.Content.Items.Weapons.Ammo;
using Vaultaria.Content.Projectiles.Ammo.Rare.Shotgun.Jakobs;

namespace Vaultaria.Content.Items.Weapons.Ranged.Rare.Shotgun.Jakobs
{
    public class TooScoops : VaultarianItem
    {
        protected override Sounds[] ItemSounds => new[] { Sounds.JakobsShotgun };

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            // Visual properties
            Item.Size = new Vector2(109, 30);
            Item.scale = 0.7f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Blue;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 20;
            Item.shoot = ModContent.ProjectileType<TooScoopsBullet>();
            Item.useAmmo = ModContent.ItemType<ShotgunAmmo>();

            // Combat properties
            Item.knockBack = 2.3f;
            Item.damage = 20;
            Item.crit = 16;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.reuseDelay = 7;
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

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 shotPosition = position - new Vector2(0, -20);
            Projectile.NewProjectile(source, shotPosition, ItemEffects.RandomizeProjectileVelocity(velocity), type, damage, knockback, player.whoAmI);
            Projectile.NewProjectile(source, position, ItemEffects.RandomizeProjectileVelocity(velocity), type, damage, knockback, player.whoAmI);

            return false;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-0f, 0f);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            ItemText.MultiShotText(tooltips, Item, 2);
            ItemText.Text(tooltips, Mod, "Tooltip1", "Uses Shotgun Ammo");
            ItemText.Text(tooltips, Mod, "Tooltip2", "Shoots 2 snowballs", ItemText.VaultarianColours.Cryo);

            if(!Main.hardMode)
            {
                ItemText.Text(tooltips, Mod, "Tooltip3", "Can only be used in Hardmode", ItemText.VaultarianColours.Information);
            }

            ItemText.Text(tooltips, Mod, "Tooltip4", "Found in Frozen Chests", ItemText.VaultarianColours.Information);

            ItemText.RedText(tooltips, Mod, "Coz one's never enough!");
        }
    }
}

