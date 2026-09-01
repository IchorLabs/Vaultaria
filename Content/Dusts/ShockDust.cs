using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Vaultaria.Content.Dusts
{
    public abstract class ShockDustBase : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.noGravity = true;
            dust.noLight = false;
            dust.scale = Main.rand.NextFloat(0.8f, 1.2f);
            dust.color = Color.SkyBlue;
            dust.frame = new Rectangle(0, Main.rand.Next(3) * 10, 8, 10);
        }

        public override bool Update(Dust dust)
        {
            dust.velocity += Main.rand.NextVector2Circular(0.45f, 0.45f);
            dust.velocity = Vector2.Clamp(dust.velocity, new Vector2(-3f), new Vector2(3f));
            dust.position += Main.rand.NextVector2Circular(0.8f, 0.8f);
            dust.position += dust.velocity;
            dust.velocity *= 0.9f;
            dust.scale *= 0.97f;
            dust.rotation += Main.rand.NextFloat(-0.3f, 0.3f);
            Lighting.AddLight(dust.position, 0.2f, 0.5f, 1f);

            if (dust.scale < 0.2f)
            {
                dust.active = false;
            }

            return false;
        }
    }

    public class ShockDust1 : ShockDustBase
    {
        public override string Texture => "Vaultaria/Content/Dusts/shockdust1";
    }

    public class ShockDust2 : ShockDustBase
    {
        public override string Texture => "Vaultaria/Content/Dusts/shockdust2";
    }
}
