using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Items.Materials;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Content.Items.Weapons.Ranged.Uncommon.AssaultRifle.Vladof
{
    public class BigSucc : VaultarianItem
    {
        protected override Sounds[] ItemSounds => new[] 
        { 
            Sounds.BiggSuccVariation1, 
            Sounds.BiggSuccVariation2, 
            Sounds.BiggSuccVariation3, 
            Sounds.BiggSuccVariation4, 
            Sounds.BiggSuccVariation5 
        };

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
            Item.knockBack = 0f;
            Item.damage = 3;
            Item.crit = 0;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 7;
            Item.useAnimation = 7;
            Item.reuseDelay = 2;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(gold: 2);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-5f, 4f);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            // Offset the spawn position to the barrel of the gun
            position += new Vector2(15f, -3f);
            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
            return false; // Prevent vanilla projectile spawn since we spawned it manually
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            ItemText.Text(tooltips, Mod);
            ItemText.Text(tooltips, Mod, "Tooltip", "Right Click fires an underbarrel grenade launcher", ItemText.VaultarianColours.Information);
            ItemText.RedText(tooltips, Mod, "What dat underbarrel do?");
        }
    }
}
