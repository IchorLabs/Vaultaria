using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Content.Projectiles.Melee
{
    public class BansheeClawJab : ModProjectile
    {
        public override string Texture => "Vaultaria/Content/Items/Weapons/Melee/BansheeClaw";

        public override void SetDefaults()
        {
            Projectile.Size = new Vector2(28, 28);
            Projectile.scale = 2f;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.aiStyle = ProjAIStyleID.Spear;
            Projectile.friendly = true;
            Projectile.ownerHitCheck = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 60;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
        }

        public override void OnSpawn(IEntitySource source)
        {
            Player player = Main.player[Projectile.owner];

            if (player == null || !player.active || Projectile.owner != Main.myPlayer)
            {
                return;
            }

            Vector2 launchDirection = (Main.MouseWorld - player.Center).SafeNormalize(Vector2.UnitX);
            player.velocity = launchDirection * 12f;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Projectile.owner == Main.myPlayer)
            {
                target.AddBuff(ElementalID.DarkMagicBuff, 300);
            }
        }
    }
}