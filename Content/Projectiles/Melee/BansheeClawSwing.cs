using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Content.Projectiles.Melee
{
    public class BansheeClawSwing : ModProjectile
    {
        public override string Texture => "Vaultaria/Content/Items/Weapons/Melee/BansheeClaw";

        public override void SetDefaults()
        {
            Projectile.Size = new Vector2(28, 28);
            Projectile.scale = 2f;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.aiStyle = 0; // Custom AI for swinging arc
            Projectile.friendly = true;
            Projectile.ownerHitCheck = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 70;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.extraUpdates = 0;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            if (player == null || !player.active)
            {
                Projectile.Kill();
                return;
            }

            // Progress through the swing (0 to 1)
            float swingProgress = 1f - (Projectile.timeLeft / 70f);

            // Arc swing: starts at player, swings out and around, comes back
            float swingRadius = 60f;
            float angle = swingProgress * MathHelper.Pi; // 0 to Pi for half circle swing
            
            // Position the weapon in an arc around the player
            Vector2 offset = Vector2.UnitX.RotatedBy(player.direction > 0 ? angle : MathHelper.Pi - angle) * swingRadius;
            Projectile.Center = player.Center + offset;

            // Rotation follows the swing direction
            Projectile.rotation = angle + (player.direction > 0 ? 0 : MathHelper.Pi);

            // Lighting effect
            Lighting.AddLight(Projectile.Center, 0.3f, 0.1f, 0.3f);
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
