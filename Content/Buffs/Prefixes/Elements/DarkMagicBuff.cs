using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Vaultaria.Common.Enemies.Mobs;
using Vaultaria.Content.Dusts;

namespace Vaultaria.Content.Buffs.Prefixes.Elements
{
    public class DarkMagicBuff : ModBuff
    {
        private static readonly int[] InwardDustTypes =
        {
            ModContent.DustType<DarkMagicInwardDust1>(),
            ModContent.DustType<DarkMagicInwardDust2>(),
            ModContent.DustType<DarkMagicInwardDust3>(),
        };

        private static readonly int[] OutwardDustTypes =
        {
            ModContent.DustType<DarkMagicOutwardDust1>(),
            ModContent.DustType<DarkMagicOutwardDust2>(),
            ModContent.DustType<DarkMagicOutwardDust3>(),
        };

        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;

            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<DarkMagicGlobalNPC>().TickPayout(npc);

            SpawnDust(npc);
        }

        private void SpawnDust(NPC npc)
        {
            if (Main.rand.NextBool(4))
            {
                Vector2 spawnOffset = Main.rand.NextVector2CircularEdge(npc.width * 0.7f, npc.height * 0.7f);
                Dust inward = Dust.NewDustPerfect(npc.Center + spawnOffset, InwardDustTypes[Main.rand.Next(InwardDustTypes.Length)]);
                inward.customData = npc;
            }

            if (Main.rand.NextBool(4))
            {
                Vector2 outwardVelocity = Main.rand.NextVector2CircularEdge(1f, 1f);
                Dust outward = Dust.NewDustPerfect(npc.Center, OutwardDustTypes[Main.rand.Next(OutwardDustTypes.Length)], outwardVelocity);
                outward.customData = npc;
            }
        }
    }
}
