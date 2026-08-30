using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;
using Vaultaria.Content.Items.Weapons.Ammo;
using Vaultaria.Content.Projectiles.Ammo.Legendary.SMG.Maliwan;

namespace Vaultaria.Content.Items.Weapons.Ranged.Legendary.SMG.Hyperion
{
    public class AkumasDemise : VaultarianItem
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
            Item.Size = new Vector2(71, 30);
            Item.scale = 0.95f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.LightPurple;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 20;
            Item.shoot = ProjectileID.HeatRay;
            Item.mana = 10;

            // Combat properties
            Item.knockBack = 1f;
            Item.damage = 60;
            Item.crit = 6;
            Item.DamageType = DamageClass.Magic;

            Item.useTime = 13;
            Item.useAnimation = 13;
            Item.reuseDelay = 3;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(gold: 1);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-20f, 5f);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            ItemText.Text(tooltips, Mod, "Tooltip1", "Shoots an Incendiary laser", ItemText.VaultarianColours.Incendiary);
            ItemText.RedText(tooltips, Mod, "Sun Gun, don't worry its not hot.");
        }
    }
}

