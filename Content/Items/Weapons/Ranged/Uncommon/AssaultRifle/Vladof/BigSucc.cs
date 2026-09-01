using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Items.Materials;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;
using Vaultaria.Content.Items.Weapons.Ammo;
using Vaultaria.Content.Projectiles.Ammo.Uncommon.AssaultRifle.Vladof;

namespace Vaultaria.Content.Items.Weapons.Ranged.Uncommon.AssaultRifle.Vladof
{
    public class BigSucc : VaultarianItem
    {
        private static readonly Sounds[] PrimaryFireSounds =
        {
            Sounds.BiggSuccVariation1,
            Sounds.BiggSuccVariation2,
            Sounds.BiggSuccVariation3,
            Sounds.BiggSuccVariation4,
            Sounds.BiggSuccVariation5
        };

        private bool altFireMode;

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            // Visual properties
            Item.Size = new Vector2(-10f, 4f);
            Item.scale = 2f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Green;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 10;
            Item.shoot = ProjectileID.Bullet;
            Item.useAmmo = AmmoID.Bullet;

            // Combat properties
            Item.knockBack = 0.2f;
            Item.damage = 3;
            Item.crit = 0;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 1;
            Item.useAnimation = 1;
            Item.reuseDelay = 15;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(gold: 2);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-7f, 5f);
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            altFireMode = player.altFunctionUse == 2;

            if (altFireMode)
            {
                Item.damage = 45;
                Item.shoot = ModContent.ProjectileType<BigSuccGrenade>();
                Item.shootSpeed = 7.8f;
                Item.UseSound = SoundID.Item61;
                Item.useAmmo = ModContent.ItemType<AssaultRifleAmmo>();
            }
            else
            {
                Item.damage = 3;
                Item.shoot = ProjectileID.Bullet;
                Item.shootSpeed = 10f;
                Item.useAmmo = AmmoID.Bullet;
                SetItemSound(Item, PrimaryFireSounds[Main.rand.Next(PrimaryFireSounds.Length)]);
            }

            return base.CanUseItem(player);
        }

        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            if (altFireMode)
            {
                for (int i = 0; i < 29; i++)
                {
                    player.ConsumeItem(ammo.type, false);
                }
            }

            return true;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 barrelOffset = altFireMode
                ? velocity.SafeNormalize(Vector2.UnitX) * 18f + new Vector2(0f, -5f)
                : velocity.SafeNormalize(Vector2.UnitX) * 60f + new Vector2(0f, -8f);
            position += barrelOffset;
            int projectileType = altFireMode ? ModContent.ProjectileType<BigSuccGrenade>() : type;
            int projectileDamage = altFireMode ? 45 : damage;
            Projectile.NewProjectile(source, position, velocity, projectileType, projectileDamage, knockback, player.whoAmI);
            return false;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            ItemText.Text(tooltips, Mod);
            ItemText.Text(tooltips, Mod, "Tooltip", "Left Click fires regular bullets", ItemText.VaultarianColours.Information);
            ItemText.Text(tooltips, Mod, "Tooltip", "Right Click fires an underbarrel grenade launcher that consumes 30 Assault Rifle Ammo", ItemText.VaultarianColours.Information);
            ItemText.RedText(tooltips, Mod, "What dat underbarrel do?");
        }
    }
}
