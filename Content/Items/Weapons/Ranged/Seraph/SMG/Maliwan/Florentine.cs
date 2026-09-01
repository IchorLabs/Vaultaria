using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Projectiles.Ammo.Seraph.SMG.Maliwan;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Content.Items.Weapons.Ranged.Seraph.SMG.Maliwan
{
    public class Florentine : VaultarianItem
    {
        protected override Sounds[] ItemSounds => new[] { Sounds.ETechSMGSingle };

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            // Visual properties
            Item.Size = new Vector2(70, 29);
            Item.scale = 0.95f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Pink;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 9f;
            Item.shoot = ModContent.ProjectileType<FlorentineBullet>();
            Item.mana = 4;

            // Combat properties
            Item.knockBack = 2.3f;
            Item.damage = 28;
            Item.crit = 0;
            Item.DamageType = DamageClass.Magic;

            Item.useTime = 7;
            Item.useAnimation = 7;
            Item.reuseDelay = 1;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(gold: 5);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile projectile = Projectile.NewProjectileDirect(
                source,
                position,
                velocity,
                ModContent.ProjectileType<FlorentineBullet>(),
                damage,
                knockback,
                player.whoAmI
            );

            if (projectile.ModProjectile is FlorentineBullet bullet)
            {
                bullet.shockMultiplier = 0.6f;
                bullet.slagMultiplier = 0.6f;
            }

            return false;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-20f, 5f);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            ItemText.Text(tooltips, Mod, "Tooltip1", "Uses 4 mana per shot");
            ItemText.Text(tooltips, Mod, "Tooltip2", "Has a chance to deal bonus Slag & Shock damage", ItemText.VaultarianColours.Slag);
            ItemText.RedText(tooltips, Mod, "Double trouble.");
        }
    }
}

