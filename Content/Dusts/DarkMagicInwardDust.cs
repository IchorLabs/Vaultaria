using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Vaultaria.Content.Dusts
{
    // Spawns slightly outside the enemy, then drifts inward and shrinks as it nears them.
    public abstract class DarkMagicInwardDustBase : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.noGravity = true;
            dust.noLight = true;
            dust.fadeIn = 1f;
            dust.scale = 1.2f;
            dust.frame = new Rectangle(0, Main.rand.Next(3) * 10, 10, 10); // Each sprite is a strip of 3 stacked 10x10 frames.
        }

        public override bool Update(Dust dust)
        {
            if (dust.customData is NPC npc && npc.active)
            {
                Vector2 toTarget = npc.Center - dust.position;
                float distance = toTarget.Length();
                float closeness = 1f - MathHelper.Clamp(distance / 48f, 0f, 1f);

                dust.velocity = toTarget.SafeNormalize(Vector2.Zero) * MathHelper.Lerp(0.6f, 3.5f, closeness);
                dust.position += dust.velocity;
                dust.scale = MathHelper.Lerp(1.2f, 0.2f, closeness);

                if (distance < 6f)
                {
                    dust.active = false;
                }
            }
            else
            {
                dust.active = false;
            }

            dust.rotation += 0.1f;
            Lighting.AddLight(dust.position, 0.7f * dust.scale, 0.05f * dust.scale, 0.15f * dust.scale);
            return false;
        }
    }

    public class DarkMagicInwardDust1 : DarkMagicInwardDustBase
    {
        public override string Texture => "Vaultaria/Content/Dusts/DarkmagicDust1";
    }

    public class DarkMagicInwardDust2 : DarkMagicInwardDustBase
    {
        public override string Texture => "Vaultaria/Content/Dusts/DarkmagicDust2";
    }

    public class DarkMagicInwardDust3 : DarkMagicInwardDustBase
    {
        public override string Texture => "Vaultaria/Content/Dusts/DarkmagicDust3";
    }
}
