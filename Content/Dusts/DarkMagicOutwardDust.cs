using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Vaultaria.Content.Dusts
{
    // Spawns on the enemy and flies outward, growing larger the further it travels.
    public abstract class DarkMagicOutwardDustBase : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.noGravity = true;
            dust.noLight = true;
            dust.fadeIn = 1f;
            dust.scale = 0.1f;
            dust.frame = new Rectangle(0, Main.rand.Next(3) * 10, 10, 10); // Each sprite is a strip of 3 stacked 10x10 frames.
        }

        public override bool Update(Dust dust)
        {
            dust.position += dust.velocity;
            dust.velocity *= 0.98f;
            dust.scale += 0.015f;
            dust.rotation += 0.1f;
            Lighting.AddLight(dust.position, 0.7f * dust.scale, 0.05f * dust.scale, 0.15f * dust.scale);

            bool tooFarFromSource = dust.customData is NPC npc && (!npc.active || Vector2.Distance(dust.position, npc.Center) > 48f);

            if (dust.scale > 0.7f || tooFarFromSource)
            {
                dust.active = false;
            }

            return false;
        }
    }

    public class DarkMagicOutwardDust1 : DarkMagicOutwardDustBase
    {
        public override string Texture => "Vaultaria/Content/Dusts/DarkmagicDust1";
    }

    public class DarkMagicOutwardDust2 : DarkMagicOutwardDustBase
    {
        public override string Texture => "Vaultaria/Content/Dusts/DarkmagicDust2";
    }

    public class DarkMagicOutwardDust3 : DarkMagicOutwardDustBase
    {
        public override string Texture => "Vaultaria/Content/Dusts/DarkmagicDust3";
    }
}
