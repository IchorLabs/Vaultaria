using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Vaultaria.Content.Projectiles.Ammo.Rare.Pistol.Jakobs
{
    public class HeatRayBullet : ModProjectile
    {
        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.HeatRay;

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.HeatRay);
            AIType = ProjectileID.HeatRay;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.LightBlue;
        }
    }
}
