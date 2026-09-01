using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Content.Projectiles.Ammo.Legendary.Sniper.Vladof
{
    public class ShockblastElBullet : ElementalProjectile
    {
        public float shockMultiplier = 0.1f;
        private float elementalChance = 30f;
        private short shockProjectile = ElementalID.ShockProjectile;
        private int shockBuff = ElementalID.ShockBuff;
        private int buffTime = 60;

        public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.HeatRay}";

        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.CloneDefaults(ProjectileID.HeatRay);
            Projectile.aiStyle = 0;
            Projectile.DamageType = DamageClass.Magic;
        }
        
        public override void AI()
        {
            base.AI();
            CreateHeatRayTrail();
        }

        private void CreateHeatRayTrail()
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

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (SetElementalChance(elementalChance))
            {
                Player player = Main.player[Projectile.owner];
                SetElementOnNPC(target, hit, shockMultiplier, player, shockProjectile, shockBuff, buffTime);
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            if (SetElementalChance(elementalChance))
            {
                Player player = Main.player[Projectile.owner];
                SetElementOnPlayer(target, info, shockMultiplier, player, shockProjectile, shockBuff, buffTime);
            }
        }
        
        public override Vector3 SetProjectileLightColour()
        {
            return new Vector3(104, 212, 242);
        }
     
        public override List<string> GetElement()
        {
            return new List<string>
            {
                "Shock"
            };
        }
    }
}