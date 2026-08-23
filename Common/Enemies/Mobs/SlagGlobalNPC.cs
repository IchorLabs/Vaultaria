using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Buffs.Prefixes.Elements;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Vaultaria.Common.Configs;

namespace Vaultaria.Common.Enemies.Mobs
{
    public class SlagGlobalNPC : GlobalNPC
    {    
        public override void ModifyHitByItem(NPC npc, Player player, Item item, ref NPC.HitModifiers modifiers)
        {
            VaultariaConfig config = ModContent.GetInstance<VaultariaConfig>();

            if (npc.HasBuff(ModContent.BuffType<SlagBuff>()))
            {
                modifiers.SourceDamage *= config.SlagDamageMultiplier;
            }
        }

        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            VaultariaConfig config = ModContent.GetInstance<VaultariaConfig>();

            if (npc.HasBuff(ModContent.BuffType<SlagBuff>()))
            {
                modifiers.SourceDamage *= config.SlagDamageMultiplier;
            }
        }
    }
}