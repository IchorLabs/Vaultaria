using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Vaultaria.Content.Projectiles.Ammo.Rare.Pistol.Hyperion
{
    public class TaserHeatRay : ModProjectile
    {
        public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.HeatRay}";

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.HeatRay);
            Projectile.aiStyle = 0;
            Projectile.DamageType = DamageClass.Magic;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation();
            Vector2 direction = Projectile.velocity.SafeNormalize(Vector2.UnitX);
            Vector2 sideways = direction.RotatedBy(MathHelper.PiOver2);

            for (int i = 0; i < 6; i++)
            {
                int dustType = (i % 3) switch
                {
                    0 => DustID.BlueTorch,
                    1 => DustID.MushroomSpray,
                    _ => DustID.HallowSpray
                };
                Dust dust = Dust.NewDustDirect(Projectile.Center - direction * (i * 2f) + sideways * Main.rand.NextFloat(-0.75f, 0.75f), 0, 0, dustType, 0f, 0f, 0, default, Main.rand.NextFloat(0.8f, 1.2f));
                dust.noGravity = true;
                dust.velocity = -Projectile.velocity * 0.1f + Main.rand.NextVector2Circular(0.25f, 0.25f);
                dust.rotation = Main.rand.NextFloat(6.283185f);
            }
        }
    }
}
