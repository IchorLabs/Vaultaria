using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Vaultaria.Content.Projectiles.Ammo.Rare.AssaultRifle.Vladof;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Content.Items.Weapons.Ranged.Rare.AssaultRifle.Vladof
{
    public class OlPainful : VaultarianItem
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
            Item.Size = new Vector2(77, 30);
            Item.scale = 1.1f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Blue;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 14;
            Item.shoot = ModContent.ProjectileType<OlPainfulHeatRay>();
            Item.mana = 1;

            // Combat properties
            Item.knockBack = 1f;
            Item.damage = 12;
            Item.crit = 1;
            Item.DamageType = DamageClass.Magic;

            Item.useTime = (int)StartingUseTime;
            Item.useAnimation = (int)StartingUseTime;
            Item.reuseDelay = 4;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(silver: 50);
        }

        private const float StartingUseTime = 15f;
        private const float MaximumFireRateUseTime = 5f;
        private const float FireRateGain = 0.5f;
        private const float FireRateDecay = 1f;
        private const float BaseWeaponSpread = 1.5f;
        private const float MaximumInaccuracy = 0.35f;
        private const float InaccuracyGain = 0.005f;
        private const float InaccuracyDecay = 0.02f;

        private float currentUseTime = StartingUseTime;
        private float currentInaccuracy;

        public override void HoldItem(Player player)
        {
            if (player.itemAnimation == 0)
            {
                currentUseTime = MathF.Min(currentUseTime + FireRateDecay, StartingUseTime);
                currentInaccuracy = MathHelper.Clamp(currentInaccuracy - InaccuracyDecay, 0f, MaximumInaccuracy);
            }

            Item.useTime = (int)MathF.Ceiling(currentUseTime);
            Item.useAnimation = Item.useTime;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            currentUseTime = MathF.Max(currentUseTime - FireRateGain, MaximumFireRateUseTime);
            currentInaccuracy = MathHelper.Clamp(currentInaccuracy + InaccuracyGain, 0f, MaximumInaccuracy);

            float spread = BaseWeaponSpread * (1f + currentInaccuracy);
            velocity = velocity.RotatedByRandom(MathHelper.ToRadians(spread));

            Projectile.NewProjectileDirect(
                source,
                position,
                velocity,
                ModContent.ProjectileType<OlPainfulHeatRay>(),
                damage,
                knockback,
                player.whoAmI
            );

            return false;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-6f, 3f);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            ItemText.Text(tooltips, Mod, "ToolTip1", "Shoots a ricocheting laser", ItemText.VaultarianColours.Information);
            ItemText.Text(tooltips, Mod, "Tooltip2", "Found in Skyware Chests", ItemText.VaultarianColours.Information);
            ItemText.RedText(tooltips, Mod, "Come on in... Ol' Painful is waiting.");
        }
    }
}

