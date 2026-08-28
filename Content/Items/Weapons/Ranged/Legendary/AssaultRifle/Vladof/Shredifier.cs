using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Items.Materials;
using System.Collections.Generic;
using System;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Content.Items.Weapons.Ranged.Legendary.AssaultRifle.Vladof
{
    public class Shredifier : VaultarianItem
    {
        private const float StartingUseTime = 15f;
        private const float MaximumFireRateUseTime = 4f;
        private const float FireRateGain = 0.5f;
        private const float FireRateDecay = 1f;
        private const float MildSpread = 2f;

        private float currentUseTime = StartingUseTime;

        protected override Sounds[] ItemSounds => [];

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            // Visual properties
            Item.Size = new Vector2(99, 29);
            Item.scale = 1f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Yellow;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 30;
            Item.shoot = ProjectileID.Bullet;
            Item.useAmmo = AmmoID.Bullet;

            // Combat properties
            Item.knockBack = 2.3f;
            Item.damage = 35;
            Item.crit = 21;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = (int)StartingUseTime;
            Item.useAnimation = (int)StartingUseTime;
            Item.reuseDelay = 0;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(gold: 4);
            SetItemSound(Item, Sounds.VladofAR, 60);
        }

        public override void HoldItem(Player player)
        {
            if (player.itemAnimation == 0)
            {
                currentUseTime = MathF.Min(currentUseTime + FireRateDecay, StartingUseTime);
            }

            Item.useTime = (int)MathF.Ceiling(currentUseTime);
            Item.useAnimation = Item.useTime;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            currentUseTime = MathF.Max(currentUseTime - FireRateGain, MaximumFireRateUseTime);

            for (int shot = 0; shot < 2; shot++)
            {
                Vector2 shotVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(MildSpread));

                Projectile.NewProjectile(
                    source,
                    shot == 0
                        ? position - new Vector2(0, -3)
                        : position - new Vector2(0, 4),
                    shotVelocity,
                    type,
                    damage,
                    knockback,
                    player.whoAmI
                );
            }

            return false;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Eridium>(50)
                .AddIngredient(ItemID.ChlorophyteBar, 25)
                .AddIngredient(ItemID.ChainGun, 1)
                .AddIngredient(ItemID.SoulofNight, 25)
                .AddIngredient(ItemID.IllegalGunParts, 2)
                .AddTile(ModContent.TileType<Tiles.VendingMachines.MarcusVendingMachine>())
                .Register();
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-20f, 0f);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            ItemText.MultiShotText(tooltips, Item, 2);
            ItemText.Text(tooltips, Mod);
            ItemText.Text(tooltips, Mod, "Tooltip2", "+100% Fire rate", ItemText.VaultarianColours.Information);
            ItemText.RedText(tooltips, Mod, "Speed kills.");
        }
    }
}