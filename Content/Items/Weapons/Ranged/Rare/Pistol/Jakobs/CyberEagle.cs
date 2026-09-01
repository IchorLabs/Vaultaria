using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;
using Terraria.Audio;
using Vaultaria.Content.Items.Weapons.Ammo;
using Vaultaria.Content.Projectiles.Ammo.Rare.Pistol.Jakobs;

namespace Vaultaria.Content.Items.Weapons.Ranged.Rare.Pistol.Jakobs
{
    public class CyberEagle : VaultarianItem
    {
        protected override Sounds[] ItemSounds => new[] { Sounds.MaliwanLaserSingle };

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            // Visual properties
            Item.Size = new Vector2(66, 30);
            Item.scale = 1f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Blue;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 10f;
            Item.shoot = ModContent.ProjectileType<CyberEagleHeatRay>();
            Item.useAmmo = ModContent.ItemType<PistolAmmo>();

            // Combat properties
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 1f;
            Item.damage = 16;
            Item.crit = 4;
            Item.DamageType = DamageClass.Magic; // Assuming it's a magic weapon, change as needed

            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.reuseDelay = 10;
            Item.autoReuse = false;
            Item.useTurn = false;

            // Other properties
            Item.value = Item.buyPrice(gold: 2);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 firingDirection = velocity.SafeNormalize(Vector2.UnitX);
            float barrelDistance = player.direction == 1 ? 60f : 70f;
            Vector2 barrelPosition = position + firingDirection * barrelDistance;
            barrelPosition += firingDirection.RotatedBy(-MathHelper.PiOver2) * 5f * player.direction;
            Projectile.NewProjectile(source, barrelPosition, velocity, type, damage, knockback, player.whoAmI);
            return false;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            ItemText.Text(tooltips, Mod);
            ItemText.Text(tooltips, Mod, "Tooltip2", "Shoots Shock Lasers");
            ItemText.RedText(tooltips, Mod, "Feel like I'm gonna break this damn thing!");
        }
    } 
}

