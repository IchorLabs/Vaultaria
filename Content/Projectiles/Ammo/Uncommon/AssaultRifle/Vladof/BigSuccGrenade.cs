using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Vaultaria.Content.Projectiles.Ammo.Uncommon.AssaultRifle.Vladof
{
    public class BigSuccGrenade : ModProjectile
    {
        public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.Grenade}";

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Grenade);
            AIType = ProjectileID.Grenade;
            Projectile.DamageType = DamageClass.Ranged;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.Kill();
            return false;
        }

        public override void OnKill(int timeLeft)
        {
            Projectile.Resize(128, 128);
            Projectile.Damage();
            SoundEngine.PlaySound(SoundID.Item62, Projectile.Center);

            for (int i = 0; i < 20; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, 0f, 0f, 100, default, 1.5f);
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default, 2f);
            }
        }
    }
}