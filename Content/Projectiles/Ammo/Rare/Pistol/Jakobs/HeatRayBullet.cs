using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Content.Projectiles.Ammo.Rare.Pistol.Jakobs
{
    public class HeatRayBullet : ModProjectile
    {
        private const float MaximumBeamLength = 2400f;
        private const float BeamWidth = 8f;
        private float beamLength;

        public override string Texture => "Vaultaria/Content/Projectiles/Ammo/Common/Pistol/Maliwan/AegisBullet";

        public override void SetDefaults()
        {
            Projectile.width = 2;
            Projectile.height = 2;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 2;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.hide = true;
        }

        public override void OnSpawn(IEntitySource source)
        {
            Projectile.ai[0] = Projectile.velocity.ToRotation();
            Projectile.velocity = Vector2.Zero;
        }

        public override void AI()
        {
            Vector2 direction = Projectile.ai[0].ToRotationVector2();
            float[] samples = Collision.LaserScan(Projectile.Center, direction, 1f, MaximumBeamLength, 3);
            beamLength = (samples[0] + samples[1] + samples[2]) / samples.Length;

            Lighting.AddLight(Projectile.Center, 0.15f, 0.65f, 1f);
            Projectile.timeLeft = 2;
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            Vector2 direction = Projectile.ai[0].ToRotationVector2();
            Vector2 beamEnd = Projectile.Center + direction * beamLength;
            float collisionPoint = 0f;

            return Collision.CheckAABBvLineCollision(
                targetHitbox.TopLeft(),
                targetHitbox.Size(),
                Projectile.Center,
                beamEnd,
                BeamWidth,
                ref collisionPoint
            );
        }

        public override bool PreDraw(ref Color lightColor)
        {
            if (beamLength <= 0f)
            {
                return false;
            }

            Texture2D texture = TextureAssets.Projectile[Type].Value;
            Vector2 direction = Projectile.ai[0].ToRotationVector2();
            Vector2 start = Projectile.Center - Main.screenPosition;
            Vector2 end = start + direction * beamLength;

            DelegateMethods.f_1 = 1f;
            DelegateMethods.c_1 = new Color(120, 220, 255, 255);
            Utils.LaserLineFraming lineFraming = new Utils.LaserLineFraming(DelegateMethods.RainbowLaserDraw);
            Utils.DrawLaser(Main.spriteBatch, texture, start, end, new Vector2(Projectile.scale), lineFraming);

            return false;
        }

    }
}
