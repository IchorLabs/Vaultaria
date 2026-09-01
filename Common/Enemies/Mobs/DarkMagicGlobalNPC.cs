using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Common.Enemies.Mobs
{
    public class DarkMagicGlobalNPC : GlobalNPC
    {
        private const int TicksPerPayout = 60;
        private const float HealPercent = 0.02f;
        private const float MaxHealPerPayout = 5f; // Matches 2% of the 250 damage cap.

        public override bool InstancePerEntity => true;

        private float pendingDamage;
        private int payoutTimer;

        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            base.DrawEffects(npc, ref drawColor);

            if (npc.HasBuff(ElementalID.DarkMagicBuff))
            {
                float pulse = 0.2f + (float)((System.Math.Sin(Main.GameUpdateCount * 0.08f) + 1f) * 0.075f);
                drawColor = Color.Lerp(drawColor, ItemText.VaultarianColours.DarkMagic.GetVaultarianColor(), pulse);
            }
        }

        public override void OnHitByItem(NPC npc, Player player, Item item, NPC.HitInfo hit, int damageDone)
        {
            if (npc.HasBuff(ElementalID.DarkMagicBuff))
            {
                pendingDamage += damageDone;
            }
        }

        public override void OnHitByProjectile(NPC npc, Projectile projectile, NPC.HitInfo hit, int damageDone)
        {
            if (npc.HasBuff(ElementalID.DarkMagicBuff))
            {
                pendingDamage += damageDone;
            }
        }

        public void TickPayout(NPC npc)
        {
            payoutTimer++;

            if (payoutTimer < TicksPerPayout)
            {
                return;
            }

            payoutTimer = 0;

            if (pendingDamage <= 0f)
            {
                return;
            }

            int closestPlayerIndex = Player.FindClosest(npc.Center, npc.width, npc.height);
            Player closestPlayer = Main.player[closestPlayerIndex];
            float heal = System.Math.Min(pendingDamage * HealPercent, MaxHealPerPayout);

            ItemEffects.Heal(closestPlayer, heal);

            pendingDamage = 0f;
        }
    }
}
