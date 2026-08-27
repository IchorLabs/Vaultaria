using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.GameContent;
using Terraria.Graphics;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace Vaultaria.Content.Projectiles.Ammo.Rare.Pistol.Jakobs
{
    public class HeatRayBullet : ModProjectile
    {
        private VertexStrip vertexStrip = new VertexStrip();

        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.MagicMissile;

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Type] = 24;
            ProjectileID.Sets.TrailingMode[Type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 120;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
            Projectile.extraUpdates = 2;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation();
            Lighting.AddLight(Projectile.Center, 0.15f, 0.65f, 1f);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Vector2[] positions = new Vector2[Projectile.oldPos.Length];
            float[] rotations = new float[Projectile.oldRot.Length];

            for (int i = 0; i < positions.Length; i++)
            {
                positions[i] = Projectile.oldPos[i] + Projectile.Size / 2f;
                rotations[i] = Projectile.oldRot[i];
            }

            MiscShaderData shader = GameShaders.Misc["MagicMissile"];
            shader.UseImage0(TextureAssets.Projectile[194]);
            shader.UseImage1(TextureAssets.Projectile[192]);
            shader.UseImage2(TextureAssets.Projectile[193]);
            shader.UseColor(new Color(120, 210, 255));
            shader.UseShaderSpecificData(new Vector4(
                (float)(Main.GameUpdateCount % 30) / 30f,
                0f,
                0f,
                0f
            ));
            shader.Apply();

            vertexStrip.PrepareStrip(
                positions,
                rotations,
                progress => Color.Lerp(new Color(120, 210, 255), Color.White, 1f - progress),
                progress => MathHelper.Lerp(8f, 0f, progress),
                -Main.screenPosition,
                positions.Length,
                true
            );
            vertexStrip.DrawTrail();

            Main.pixelShader.CurrentTechnique.Passes[0].Apply();
            return false;
        }

    }
}
