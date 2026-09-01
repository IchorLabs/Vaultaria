using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Items.Materials;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;
using Vaultaria.Content.Items.Weapons.Ammo;
using Vaultaria.Content.Projectiles.Ammo.Legendary.Shotgun.Torgue;
using Vaultaria.Content.Items.Accessories.Relics;

namespace Vaultaria.Content.Items.Weapons.Ranged.Legendary.Shotgun.Torgue
{
    public class SwordSplosion : VaultarianItem
    {
        protected override Sounds[] ItemSounds => new[] { Sounds.TorgueShotgun };

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            // Visual properties
            Item.Size = new Vector2(67, 30);
            Item.scale = 0.95f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useTurn = false;
            Item.rare = ItemRarityID.LightPurple;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 10;
            Item.shoot = ModContent.ProjectileType<SwordSplosionKnife>();
            Item.useAmmo = ModContent.ItemType<ShotgunAmmo>();

            // Combat properties
            Item.knockBack = 2.3f;
            Item.damage = 30;
            Item.crit = 6;
            Item.DamageType = DamageClass.Melee;

            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.reuseDelay = 35;
            Item.autoReuse = true;
            ItemID.Sets.ShimmerTransformToItem[Item.type] = ModContent.ItemType<MysteriousAmulet>();

            // Other properties
            Item.value = Item.buyPrice(gold: 2);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            ItemEffects.CloneShots(player, source, position, velocity, type, damage, knockback, 3, 5, randomizeVelocity: true);
            
            return false;
        }

        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            for (int i = 0; i < 2; i++)
            {
                player.ConsumeItem(ammo.type, false);
            }

            return true;
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            Vector2 aimDirection = Main.MouseWorld - player.MountedCenter;
            player.ChangeDir(aimDirection.X >= 0f ? 1 : -1);

            float aimRotation = aimDirection.ToRotation();
            float itemRotation = (aimDirection * player.direction).ToRotation();

            player.itemRotation = itemRotation;
            player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, aimRotation - MathHelper.PiOver2);
            player.itemLocation.Y -= 6f;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10f, 0f);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            ItemText.MultiShotText(tooltips, Item, 3);
            ItemText.Text(tooltips, Mod, "Tooltip1", "Consumes 3 Shotgun Ammo per shot");
            ItemText.Text(tooltips, Mod, "Tooltip2", "Shoots out swords that explode on contact", ItemText.VaultarianColours.Explosive);
            ItemText.Text(tooltips, Mod, "Tooltip3", "Given after completing 50 Angler quests", ItemText.VaultarianColours.Information);
            ItemText.RedText(tooltips, Mod, "Because Mister Torgue said so.");
        }
    }
}

