using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Projectiles.Ammo.Rare.Pistol.Hyperion;
using System.Collections.Generic;
using Vaultaria.Content.Prefixes.Weapons;
using Vaultaria.Common.Utilities;
using Vaultaria.Content.Items.Materials;

namespace Vaultaria.Content.Items.Weapons.Ranged.Rare.Pistol.Hyperion
{
    public class Taser : VaultarianItem
    {
        protected override Sounds[] ItemSounds => new[] { Sounds.GenericLaser };

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            // Visual properties
            Item.Size = new Vector2(45, 30);
            Item.scale = 1f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Blue;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 10f;
            Item.shoot = ModContent.ProjectileType<TaserHeatRay>();
            Item.mana = 3;

            // Combat properties
            Item.knockBack = 1f;
            Item.damage = 20;
            Item.crit = 0;
            Item.DamageType = DamageClass.Magic;

            Item.useTime = 6;
            Item.useAnimation = 6;
            Item.reuseDelay = 0;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(gold: 1);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-7f, 2f);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 firingDirection = velocity.SafeNormalize(Vector2.UnitX);
            Vector2 barrelPosition = position + firingDirection * 42f;
            barrelPosition += firingDirection.RotatedBy(-MathHelper.PiOver2) * 4f * player.direction;
            Projectile.NewProjectile(source, barrelPosition, velocity, type, damage, knockback, player.whoAmI);
            return false;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Eridium>(30)
                .AddIngredient(ItemID.SpaceGun, 1)
                .AddIngredient(ItemID.IllegalGunParts, 1)
                .AddIngredient(ItemID.SoulofMight, 25)
                .AddTile(ModContent.TileType<Tiles.VendingMachines.MarcusVendingMachine>())
                .Register();
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            ItemText.Text(tooltips, Mod, "Tooltip1", "Uses 3 mana per shot");
            ItemText.Text(tooltips, Mod, "Tooltip2", "Shoots bullets that are extremely fast");
            ItemText.Text(tooltips, Mod, "Tooltip3", "Increases accuracy with sustained fire!");
            ItemText.RedText(tooltips, Mod, "I politely request you do not T4s-R me, good sir.");
        }
    }
}

