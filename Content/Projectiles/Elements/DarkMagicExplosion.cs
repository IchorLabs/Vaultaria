using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Content.Projectiles.Elements
{
    public class DarkMagicExplosion : ElementalProjectile
    {
        private const int BuffTime = 120;

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 7;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.Size = new Vector2(90, 90);
            Projectile.scale = 1f;
            Projectile.penetrate = -1;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 28;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }

        public override void AI()
        {
            base.AI();
            ItemEffects.FrameRotator(4, Projectile);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ElementalID.DarkMagicBuff, BuffTime);
        }

        public override Vector3 SetProjectileLightColour()
        {
            return new Vector3(196, 30, 58);
        }

        public override List<string> GetElement()
        {
            return new List<string>
            {
                "DarkMagic"
            };
        }
    }
}
