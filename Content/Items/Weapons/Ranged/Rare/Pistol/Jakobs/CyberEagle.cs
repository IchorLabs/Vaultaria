using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;
using Terraria.Audio;

namespace Vaultaria.Content.Items.Weapons.Ranged.Rare.Pistol.Jakobs
{
    public class CyberEagle : VaultarianItem
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
            Item.Size = new Vector2(66, 30);
            Item.scale = 1f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Blue;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 10f;
            Item.shoot = ProjectileID.MartianTurretBolt;
            Item.useAmmo = AmmoID.Bullet;

            // Combat properties
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 1f;
            Item.damage = 16;
            Item.crit = 4;
            Item.DamageType = DamageClass.Magic; // Assuming it's a magic weapon, change as needed

            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.reuseDelay = 10;
            Item.autoReuse = true;
            Item.useTurn = false;

            // Other properties
            Item.value = Item.buyPrice(gold: 2);
            SetItemSound(Item, Sounds.MaliwanLaserSingle, 60);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            ItemText.Text(tooltips, Mod);
            ItemText.Text(tooltips, Mod, "Tooltip2", "Shoots Shock Lasers");
            ItemText.RedText(tooltips, Mod, "Feel like I'm gonna break this damn thing!");
        }
    } 
}