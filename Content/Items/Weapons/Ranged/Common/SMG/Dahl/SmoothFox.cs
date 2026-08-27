using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Items.Materials;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;
using Terraria.Audio;

namespace Vaultaria.Content.Items.Weapons.Ranged.Common.SMG.Dahl
{
    public class SmoothFox : VaultarianItem
    {
        protected override Sounds[] ItemSounds => [];

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            // Visual properties
            Item.Size = new Vector2(60, 34);
            Item.scale = 0.9f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.White;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 5;
            Item.shoot = ProjectileID.Bullet;
            Item.useAmmo = AmmoID.Bullet;

            // Combat properties
            Item.knockBack = 1.5f;
            Item.damage = 8;
            Item.crit = 0;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 4;
            Item.useAnimation = 12;
            Item.reuseDelay = 25;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(silver: 1);
            SetItemSound(Item, Sounds.DahlSMGBurst, 60);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-15f, 5f);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            ItemText.Text(tooltips, Mod);
            ItemText.Text(tooltips, Mod, "ToolTip2", "Fires a basic 3 round burst");
            ItemText.Text(tooltips, Mod, "Tooltip3", "Found in Wooden Chests", ItemText.VaultarianColours.Information);
        }
    }
}