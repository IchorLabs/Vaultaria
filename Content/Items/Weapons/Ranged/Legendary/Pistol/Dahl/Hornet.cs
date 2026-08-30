using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Items.Materials;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;
using Vaultaria.Content.Items.Weapons.Ammo;
using Vaultaria.Content.Projectiles.Ammo.Legendary.Pistol.Dahl;

namespace Vaultaria.Content.Items.Weapons.Ranged.Legendary.Pistol.Dahl
{
    public class Hornet : VaultarianItem
    {
        protected override Sounds[] ItemSounds => new[] { Sounds.DahlPistolBurst };

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            // Visual properties
            Item.Size = new Vector2(41, 29);
            Item.scale = 0.8f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Yellow;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 10f;
            Item.shoot = ModContent.ProjectileType<HornetBullet>();
            Item.useAmmo = ModContent.ItemType<PistolAmmo>();

            // Combat properties
            Item.knockBack = 1f;
            Item.damage = 17;
            Item.crit = 0;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 4;
            Item.useAnimation = 12;
            Item.reuseDelay = 50;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(gold: 2);
            // Item.UseSound = SoundID.Item31;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(3f, 3f);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            ItemText.Text(tooltips, Mod, "Tooltip1", "Uses Pistol Ammo");
            ItemText.Text(tooltips, Mod, "Tooltip2", "Fires a burst of Corrosive bullets", ItemText.VaultarianColours.Corrosive);
            ItemText.RedText(tooltips, Mod, "Fear the swarm!");
        }
    }
}

