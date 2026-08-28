using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Vaultaria.Content.Projectiles.Ammo.Rare.AssaultRifle.Vladof
{
    public class OlPainfulHeatRay : ModProjectile
    {
        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.HeatRay;

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.HeatRay);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.localAI[0] == 0f)
            {
                if (oldVelocity.X != Projectile.velocity.X)
                {
                    Projectile.velocity.X = -oldVelocity.X;
                }

                if (oldVelocity.Y != Projectile.velocity.Y)
                {
                    Projectile.velocity.Y = -oldVelocity.Y;
                }

                Projectile.localAI[0] = 1f;
                Projectile.netUpdate = true;
                return false;
            }

            return true;
        }
    }
}