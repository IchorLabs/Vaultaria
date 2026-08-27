using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Vaultaria.Content.Projectiles.Ammo.Rare.Pistol.Jakobs
{
    public class HeatRayBullet : ModProjectile
    {
        public override string Texture => "Vaultaria/Content/Projectiles/Ammo/Common/Pistol/Maliwan/AegisBullet";

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation();
            Lighting.AddLight(Projectile.Center, 0.15f, 0.65f, 1f);
        }

    }
}
