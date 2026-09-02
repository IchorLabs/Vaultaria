using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Vaultaria.Common.Utilities;
using Vaultaria.Content.Projectiles.Melee;

namespace Vaultaria.Content.Items.Weapons.Melee
{
    public class BansheeClaw : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.Size = new Vector2(28, 28);
            Item.scale = 2f;
            Item.DamageType = DamageClass.Melee;
            Item.damage = 50;
            Item.knockBack = 5f;
            Item.crit = 6;
            
            // Left-click swing (default)
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 70;
            Item.useAnimation = 70;
            Item.autoReuse = true;
            Item.useTurn = true;
            
            Item.value = Item.buyPrice(gold: 1);
            Item.rare = ItemRarityID.LightRed;
            Item.UseSound = SoundID.Item1;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-24f, 16f);
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2) // Right-click (shortsword jab with dash)
            {
                Item.useStyle = ItemUseStyleID.Shoot;
                Item.useTime = 12;
                Item.useAnimation = 12;
                Item.noMelee = true;
                Item.noUseGraphic = true;
                Item.shoot = ModContent.ProjectileType<BansheeClawJab>();
                Item.shootSpeed = 12f;
            }
            else // Left-click (native swing)
            {
                Item.useStyle = ItemUseStyleID.Swing;
                Item.useTime = 70;
                Item.useAnimation = 70;
                Item.noMelee = false;
                Item.noUseGraphic = false;
                Item.shoot = ProjectileID.None;
            }

            return base.CanUseItem(player);
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                target.AddBuff(ElementalID.DarkMagicBuff, 300);
            }
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            ItemText.Text(tooltips, Mod, "Tooltip1", "Inflicts Dark Magic", ItemText.VaultarianColours.DarkMagic);
            ItemText.Text(tooltips, Mod, "Tooltip2", "Right-Click to jab and dash toward cursor", ItemText.VaultarianColours.Information);
        }
    }
}