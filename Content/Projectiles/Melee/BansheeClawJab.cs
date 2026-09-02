using Microsoft.Xna.Framework;
using Terraria;
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
            Projectile.timeLeft = 20;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
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