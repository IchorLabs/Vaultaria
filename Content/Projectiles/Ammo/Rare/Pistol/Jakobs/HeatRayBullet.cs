using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Vaultaria.Content.Projectiles.Ammo.Rare.Pistol.Jakobs
{
    public class HeatRayBullet : ModProjectile
    {
        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.MagicMissile;

        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 120;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
            Projectile.extraUpdates = 2;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation();

            Dust trail = Dust.NewDustPerfect(
                Projectile.Center,
                DustID.PurpleTorch,
                -Projectile.velocity * 0.05f,
                0,
                new Color(120, 210, 255),
                0.9f
            );
            trail.noGravity = true;

            Lighting.AddLight(Projectile.Center, 0.15f, 0.65f, 1f);
        }

    }
}
