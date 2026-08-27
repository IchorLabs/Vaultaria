using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Vaultaria.Content.Projectiles.Ammo.Rare.Pistol.Jakobs
{
    public class HeatRayBullet : ModProjectile
    {
        private static Texture2D lightBlueHeatRayTexture;

        public override string Texture => "Vaultaria/Content/Projectiles/Ammo/Common/Pistol/Maliwan/AegisBullet";

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.HeatRay);
            AIType = ProjectileID.HeatRay;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            if (lightBlueHeatRayTexture == null)
            {
                Texture2D heatRayTexture = TextureAssets.Projectile[ProjectileID.HeatRay].Value;
                Color[] sourceColors = new Color[heatRayTexture.Width * heatRayTexture.Height];
                Color[] recoloredColors = new Color[sourceColors.Length];

                heatRayTexture.GetData(sourceColors);
                for (int i = 0; i < sourceColors.Length; i++)
                {
                    Color sourceColor = sourceColors[i];
                    byte intensity = Math.Max(sourceColor.R, Math.Max(sourceColor.G, sourceColor.B));
                    recoloredColors[i] = new Color(
                        (byte)(intensity * 0.45f),
                        (byte)(intensity * 0.8f),
                        intensity,
                        sourceColor.A
                    );
                }

                lightBlueHeatRayTexture = new Texture2D(Main.instance.GraphicsDevice, heatRayTexture.Width, heatRayTexture.Height);
                lightBlueHeatRayTexture.SetData(recoloredColors);
            }

            Main.EntitySpriteDraw(
                lightBlueHeatRayTexture,
                Projectile.Center - Main.screenPosition,
                null,
                Color.White * Projectile.Opacity,
                Projectile.rotation,
                lightBlueHeatRayTexture.Size() / 2f,
                Projectile.scale,
                SpriteEffects.None,
                0f
            );

            return false;
        }

        public override void Unload()
        {
            lightBlueHeatRayTexture?.Dispose();
            lightBlueHeatRayTexture = null;
        }

    }
}
