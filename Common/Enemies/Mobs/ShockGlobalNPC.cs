using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Vaultaria.Common.Utilities;
using Vaultaria.Content.Buffs.Prefixes.Elements;
using Vaultaria.Content.Dusts;

namespace Vaultaria.Common.Enemies.Mobs
{
    public class ShockGlobalNPC : GlobalNPC
    {
        private const int StunDuration = 72;
        private const int StunCooldown = 300;
        private const int DamageOverTime = 3;
        private const int DamageTicksPerSecond = 4;

        private static readonly int[] ShockDustTypes =
        {
            ModContent.DustType<ShockDust1>(),
            ModContent.DustType<ShockDust2>(),
        };

        public override bool InstancePerEntity => true;

        private int stunTimer;
        private int stunCooldownTimer;

        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            base.DrawEffects(npc, ref drawColor);

            if (npc.HasBuff(ElementalID.ShockBuff))
            {
                if (stunTimer > 0)
                {
                    float flicker = 0.45f + (float)((System.Math.Sin(Main.GameUpdateCount * 0.8f) + 1f) * 0.2f);
                    drawColor = Color.Lerp(drawColor, Color.Cyan, flicker);
                    Lighting.AddLight(npc.Center, 0.1f, 0.4f, 1f);
                }
                else
                {
                    float pulse = 0.2f + (float)((System.Math.Sin(Main.GameUpdateCount * 0.15f) + 1f) * 0.1f);
                    drawColor = Color.Lerp(drawColor, new Color(80, 170, 255), pulse);
                }
            }
        }

        public override bool PreAI(NPC npc)
        {
            if (stunCooldownTimer > 0)
            {
                stunCooldownTimer--;
            }

            if (stunTimer <= 0)
            {
                return base.PreAI(npc);
            }

            stunTimer--;
            npc.velocity = Vector2.Zero;
            npc.netUpdate = true;
            return false;
        }

        public override void AI(NPC npc)
        {
            if (!npc.HasBuff(ElementalID.ShockBuff))
            {
                ResetEffectState();
                return;
            }

            SpawnShockDust(npc);

            if (stunCooldownTimer == 0 && !IsBossOrMiniboss(npc))
            {
                stunTimer = StunDuration;
                stunCooldownTimer = StunCooldown;
                npc.netUpdate = true;
            }

        }

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (!npc.HasBuff(ElementalID.ShockBuff))
            {
                return;
            }

            npc.lifeRegen -= DamageOverTime * DamageTicksPerSecond * 2;
            damage = System.Math.Max(damage, DamageOverTime);
        }

        private void SpawnShockDust(NPC npc)
        {
            if (Main.rand.NextBool(3))
            {
                SpawnShockBurst(npc, 2);
            }
        }

        public static void SpawnShockBurst(NPC npc, int amount = 12)
        {
            if (Main.dedServ)
            {
                return;
            }

            for (int i = 0; i < amount; i++)
            {
                Vector2 position = new Vector2(
                    Main.rand.NextFloat(npc.position.X, npc.position.X + npc.width),
                    Main.rand.NextFloat(npc.position.Y, npc.position.Y + npc.height));
                Vector2 velocity = Main.rand.NextVector2CircularEdge(3f, 3f) + Main.rand.NextVector2Circular(0.8f, 0.8f);
                Dust dust = Dust.NewDustPerfect(position, ShockDustTypes[Main.rand.Next(ShockDustTypes.Length)], velocity);
                dust.noGravity = true;
            }
        }

        private static bool IsBossOrMiniboss(NPC npc)
        {
            return npc.boss || NPCID.Sets.ShouldBeCountedAsBoss[npc.type];
        }

        private void ResetEffectState()
        {
            stunTimer = 0;
        }
    }
}
