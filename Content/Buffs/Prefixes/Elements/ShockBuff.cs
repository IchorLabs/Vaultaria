using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Vaultaria.Content.Buffs.Prefixes.Elements
{
    public class ShockBuff : ModBuff
    {
        public const int Duration = 300;

        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;

            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }

        // For PvE
        public override void Update(NPC npc, ref int buffIndex)
        {
        }

        // For PvP
        public override void Update(Player player, ref int buffIndex)
        {
        }
    }
}