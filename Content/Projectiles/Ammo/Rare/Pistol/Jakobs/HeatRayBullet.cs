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
            Projectile.CloneDefaults(ProjectileID.HeatRay);
            AIType = ProjectileID.HeatRay;
        }

    }
}
