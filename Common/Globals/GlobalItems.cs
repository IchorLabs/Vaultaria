using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Vaultaria.Common.Configs;
using Vaultaria.Common.Utilities;
using Vaultaria.Content.Buffs.GunEffects;
using Vaultaria.Content.Buffs.SkillEffects;
using Vaultaria.Content.Items.Accessories.Skills;
using Vaultaria.Content.Items.Weapons.Ammo;
using Vaultaria.Content.Items.Weapons.Ranged.Legendary.Laser.Dahl;
using Vaultaria.Content.Items.Weapons.Ranged.Legendary.Pistol.Jakobs;
using Vaultaria.Content.Items.Weapons.Ranged.Legendary.SMG.Hyperion;
using Vaultaria.Content.Items.Weapons.Ranged.Rare.Pistol.Hyperion;
using Vaultaria.Content.Items.Weapons.Ranged.Rare.Sniper.Jakobs;
using Vaultaria.Content.Items.Weapons.Magic;
using Vaultaria.Content.Prefixes.Weapons;

namespace Vaultaria.Common.Globals
{
    public class GlobalItems : GlobalItem
    {
        public int firedWeaponPrefixID;
        public override bool InstancePerEntity => true;

        public const bool EnableCursorHoldStyle = false;

        private const float HyperionMaximumSpread = 40f; // Starting spread in degrees before any shots are fired.
        private const float HyperionAccuracyGain = 0.075f; // Accuracy gained per shot; 1f is perfectly accurate.
        private const float HyperionAccuracyDecay = 0.0025f; // Accuracy lost each tick after the player stops firing.
        private const float HyperionAccuracyCap = 1f; // Maximum accuracy value, where 1f removes all Hyperion spread.
        private const float BaseWeaponSpread = 1f; // Starting spread in degrees for non-Hyperion projectile weapons.
        private const float WeaponMaximumSpread = 10f; // Maximum spread in degrees for non-Hyperion weapons at full inaccuracy.
        private const float WeaponMaximumInaccuracy = 0.5f; // Maximum inaccuracy value, limiting spread to halfway between base and maximum.
        private const float WeaponInaccuracyGain = 0.015f; // Inaccuracy gained per non-Hyperion shot.
        private const float WeaponInaccuracyDecay = 0.005f; // Inaccuracy removed each tick after firing ends.

        private float hyperionAccuracy;
        private float weaponInaccuracy;
        private float visualRecoil;
        private float visualKickback;
        private int tedioreThrowVisualTimer;
        private ulong visualRecoilStartTick;
        private int visualRecoilDuration;
        private bool visualRecoilActive;
        private int colCounter = 0;
        private ulong lastProjectileShotTick;

        public override void HoldItem(Item item, Player player)
        {
            if (IsTedioreWeapon(item) && tedioreThrowVisualTimer > 0 && player.whoAmI == Main.myPlayer)
            {
                ApplyTedioreThrowVisual(player);
                tedioreThrowVisualTimer--;
            }

            if (ModContent.GetInstance<VaultariaConfig>().DisableWeaponAccuracyGimmicks)
            {
                hyperionAccuracy = 0f;
                weaponInaccuracy = 0f;
                visualRecoil = 0f;
                visualKickback = 0f;
                visualRecoilActive = false;
                return;
            }

            bool firingIntervalElapsed = Main.GameUpdateCount - lastProjectileShotTick >= (ulong)Math.Max(item.useTime, 1);
            if (IsHyperionWeapon(item) && firingIntervalElapsed)
            {
                hyperionAccuracy = MathHelper.Clamp(hyperionAccuracy - HyperionAccuracyDecay, 0f, HyperionAccuracyCap);
            }
            else if (UsesProjectile(item) && firingIntervalElapsed)
            {
                weaponInaccuracy = MathHelper.Clamp(weaponInaccuracy - WeaponInaccuracyDecay, 0f, WeaponMaximumInaccuracy);
            }

            base.HoldItem(item, player);
        }

        public override bool? UseItem(Item item, Player player)
        {
            if (item.ModItem?.Mod == Mod &&
                (item.DamageType == DamageClass.Ranged || item.DamageType == DamageClass.Magic))
            {
                visualRecoil = 0f;
                visualKickback = 0f;
                visualRecoilActive = false;

                if (IsBigSucc(item))
                {
                    StartVisualRecoil(item);
                }
            }

            if(Utilities.Utilities.IsWearing(player, ModContent.ItemType<CloudOfLead>()))
            {
                colCounter++;
            }

            return base.UseItem(item, player);
        }

        public override void UseStyle(Item item, Player player, Rectangle heldItemFrame)
        {
            if (item.ModItem?.Mod != Mod || item.ModItem is DestroyersEye or WarriorsTail ||
                item.DamageType == DamageClass.Summon ||
                (item.DamageType != DamageClass.Ranged && item.DamageType != DamageClass.Magic))
            {
                return;
            }

            if (IsBigSucc(item))
            {
                return;
            }

            if (player.altFunctionUse == 2 && IsTedioreWeapon(item))
            {
                return;
            }

            if (player.whoAmI != Main.myPlayer)
            {
                return;
            }

            Vector2 aimDirection = Main.MouseWorld - player.MountedCenter;
            if (aimDirection == Vector2.Zero)
            {
                return;
            }

            player.ChangeDir(aimDirection.X >= 0f ? 1 : -1);
            float aimRotation = aimDirection.ToRotation() - GetVisualRecoil(item) * player.direction;
            Vector2 visualAimDirection = aimRotation.ToRotationVector2();
            player.itemRotation = (visualAimDirection * player.direction).ToRotation();
            player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, aimRotation - MathHelper.PiOver2);
            player.itemLocation -= visualAimDirection * GetVisualKickback(item);
        }

        public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (IsTedioreWeapon(item) && player.altFunctionUse == 2)
            {
                tedioreThrowVisualTimer = 15;
            }

            StartVisualRecoil(item);

            if (UsesProjectile(item))
            {
                lastProjectileShotTick = Main.GameUpdateCount;
            }

            CloudOfLead(item, player, source, position, velocity, damage, knockback);

            Redistribution(item, player);

            return base.Shoot(item, player, source, position, velocity, type, damage, knockback);
        }

        public override void ModifyShootStats(Item item, Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (ShouldApplyUniversalBarrelOffset(item, player))
            {
                position = ItemEffects.GetGunShootPosition(item, player, position, velocity);
            }

            if (ModContent.GetInstance<VaultariaConfig>().DisableWeaponAccuracyGimmicks)
            {
                base.ModifyShootStats(item, player, ref position, ref velocity, ref type, ref damage, ref knockback);
                return;
            }

            RecoverAccuracyBeforeShot(item);

            if (IsHyperionWeapon(item) && UsesProjectile(item))
            {
                float spread = HyperionMaximumSpread * (1f - hyperionAccuracy);
                if (spread > 0f)
                {
                    velocity = velocity.RotatedByRandom(MathHelper.ToRadians(spread));
                }

                hyperionAccuracy = MathHelper.Clamp(hyperionAccuracy + HyperionAccuracyGain, 0f, HyperionAccuracyCap);
            }
            else if (UsesProjectile(item))
            {
                float spread = MathHelper.Lerp(BaseWeaponSpread, WeaponMaximumSpread, weaponInaccuracy);
                velocity = velocity.RotatedByRandom(MathHelper.ToRadians(spread));
                weaponInaccuracy = MathHelper.Clamp(weaponInaccuracy + WeaponInaccuracyGain, 0f, WeaponMaximumInaccuracy);
            }

            base.ModifyShootStats(item, player, ref position, ref velocity, ref type, ref damage, ref knockback);
        }

        public override bool? CanChooseAmmo(Item weapon, Item ammo, Player player)
        {
            if (weapon.ModItem is not VaultarianItem || !IsVaultariaRangedWeapon(weapon) || IsMultiFunctionWeapon(weapon))
            {
                return null;
            }

            if (IsLauncherWeapon(weapon))
            {
                return ammo.ammo == AmmoID.Rocket || ammo.type == ModContent.ItemType<LauncherAmmo>() ? true : null;
            }

            int? categoryAmmoType = GetCategoryAmmoType(weapon);
            if (categoryAmmoType == null)
            {
                return null;
            }

            return ammo.ammo == AmmoID.Bullet || ammo.type == categoryAmmoType.Value ? true : null;
        }

        public override bool CanConsumeAmmo(Item weapon, Item ammo, Player player)
        {
            if(Utilities.Utilities.IsWearing(player, ModContent.ItemType<CloudOfLead>()))
            {
                if(colCounter == CloudOfLeadCounter())
                {
                    colCounter = 0;
                    return false;
                }
            }

            if(Utilities.Utilities.IsWearing(player, ModContent.ItemType<Inconceivable>()))
            {
                float bonusShot = Utilities.SkillUtilities.ComparativeBonus(player.statLifeMax2, player.statLife, 1.2f) + Utilities.SkillUtilities.SkillBonus(300f, 0.05f);
                float chance = 100 * (bonusShot - 1);

                if(Utilities.Utilities.Randomizer(chance) && weapon.DamageType == DamageClass.Ranged)
                {
                    return false;
                }
            }

            return base.CanConsumeAmmo(weapon, ammo, player);
        }

        private void CloudOfLead(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int damage, float knockback)
        {
            if(Utilities.Utilities.IsWearing(player, ModContent.ItemType<CloudOfLead>()) && colCounter == CloudOfLeadCounter())
            {
                Projectile.NewProjectile(source, position, velocity * 2, ElementalID.IncendiaryProjectile, damage, knockback);
            }
        }

        private float CloudOfLeadCounter()
        {
            float numberOfBossesDefeated = SkillUtilities.DownedBossCounter();

            if(numberOfBossesDefeated > 25)
            {
                return 4;
            }
            else if(numberOfBossesDefeated > 19)
            {
                return 5;
            }
            else if(numberOfBossesDefeated > 13)
            {
                return 6;
            }
            else if(numberOfBossesDefeated > 7)
            {
                return 7;
            }
            else if(numberOfBossesDefeated > 1)
            {
                return 8;
            }
            else
            {
                return 9;
            }
        }

        private void Redistribution(Item item, Player player)
        {
            if(player.HasBuff(ModContent.BuffType<RedistributionPassiveSkill>()))
            {
                Item ammo = player.ChooseAmmo(item);

                if(ammo != null)
                {
                    if(ammo.stack < 9999)
                    {
                        ammo.stack++;
                    }
                }
            }
        }

        private bool IsHyperionWeapon(Item item)
        {
            return item.ModItem?.GetType().Namespace?.Contains(".Hyperion", StringComparison.Ordinal) == true;
        }

        private bool UsesProjectile(Item item)
        {
            return item.damage > 0 && item.shoot != ProjectileID.None;
        }

        private bool IsVaultariaRangedWeapon(Item item)
        {
            return item.ModItem?.GetType().Namespace?.Contains(".Content.Items.Weapons.Ranged.", StringComparison.Ordinal) == true;
        }

        private bool IsLauncherWeapon(Item item)
        {
            return item.ModItem?.GetType().Namespace?.Contains(".Launcher", StringComparison.Ordinal) == true;
        }

        private bool IsMultiFunctionWeapon(Item item)
        {
            if (item.ModItem == null)
            {
                return false;
            }

            // Any weapon that overrides AltFunctionUse has a distinct right-click behaviour and is excluded from dual ammo.
            return item.ModItem.GetType().GetMethod(nameof(ModItem.AltFunctionUse))?.DeclaringType != typeof(ModItem);
        }

        private int? GetCategoryAmmoType(Item item)
        {
            string? ns = item.ModItem?.GetType().Namespace;
            if (ns == null)
            {
                return null;
            }

            if (ns.Contains(".Pistol", StringComparison.Ordinal)) return ModContent.ItemType<PistolAmmo>();
            if (ns.Contains(".AssaultRifle", StringComparison.Ordinal)) return ModContent.ItemType<AssaultRifleAmmo>();
            if (ns.Contains(".Shotgun", StringComparison.Ordinal)) return ModContent.ItemType<ShotgunAmmo>();
            if (ns.Contains(".SMG", StringComparison.Ordinal)) return ModContent.ItemType<SubmachineGunAmmo>();
            if (ns.Contains(".Sniper", StringComparison.Ordinal)) return ModContent.ItemType<SniperAmmo>();

            return null;
        }

        public static float GetHyperionAccuracy(Item item)
        {
            return item.GetGlobalItem<GlobalItems>().hyperionAccuracy;
        }

        public float GetVisualRecoil(Item item)
        {
            float recoilProgress = GetVisualRecoilProgress(item);
            bool hasBarrelKick = IsShotgunWeapon(item) || IsSniperWeapon(item) || IsJakobsPistol(item);
            if (!hasBarrelKick || IsDahlSniper(item) || IsCyberEagle(item))
            {
                return 0f;
            }

            float inaccuracy = IsHyperionWeapon(item)
                ? 1f - hyperionAccuracy
                : weaponInaccuracy / WeaponMaximumInaccuracy;
            return MathHelper.ToRadians(2f + 6f * inaccuracy) * recoilProgress;
        }

        private float GetVisualKickback(Item item)
        {
            float recoilProgress = GetVisualRecoilProgress(item);
            if (IsCyberEagle(item))
            {
                return 0f;
            }

            bool isBurstWeapon = item.useAnimation > item.useTime;
            bool hasBarrelKick = (IsShotgunWeapon(item) || IsSniperWeapon(item) || IsJakobsPistol(item))
                && !IsDahlSniper(item)
                && !IsCyberEagle(item);

            if (isBurstWeapon || hasBarrelKick)
            {
                return 8f * recoilProgress;
            }

            bool isStandardWeapon = IsVaultariaRangedWeapon(item) || item.DamageType == DamageClass.Magic;
            return isStandardWeapon ? 8f * recoilProgress : 0f;
        }

        private void StartVisualRecoil(Item item)
        {
            visualRecoilStartTick = Main.GameUpdateCount;
            visualRecoilDuration = Math.Max(item.useTime, 1);
            visualRecoilActive = true;
            visualRecoil = GetVisualRecoil(item);
            visualKickback = GetVisualKickback(item);
            visualRecoilActive = visualRecoil != 0f || visualKickback != 0f;
        }

        private float GetVisualRecoilProgress(Item item)
        {
            if (!visualRecoilActive || visualRecoilDuration <= 0)
            {
                return 0f;
            }

            ulong elapsedTicks = Main.GameUpdateCount - visualRecoilStartTick;
            if (elapsedTicks >= (ulong)visualRecoilDuration)
            {
                visualRecoilActive = false;
                return 0f;
            }

            float progress = elapsedTicks / (float)visualRecoilDuration;
            return 1f - MathHelper.SmoothStep(0f, 1f, progress);
        }

        private bool IsShotgunWeapon(Item item)
        {
            return item.ModItem?.GetType().Namespace?.Contains(".Shotgun", StringComparison.Ordinal) == true;
        }

        private bool IsSniperWeapon(Item item)
        {
            return item.ModItem?.GetType().Namespace?.Contains(".Sniper", StringComparison.Ordinal) == true;
        }

        private bool IsDahlSniper(Item item)
        {
            return item.ModItem?.GetType().Namespace?.Contains(".Sniper.Dahl", StringComparison.Ordinal) == true;
        }

        private bool IsJakobsPistol(Item item)
        {
            return item.ModItem?.GetType().Namespace?.Contains(".Pistol.Jakobs", StringComparison.Ordinal) == true;
        }

        private bool IsCyberEagle(Item item)
        {
            return item.ModItem?.GetType().Name == "CyberEagle";
        }

        private bool IsTedioreWeapon(Item item)
        {
            return item.ModItem?.GetType().Namespace?.Contains(".Tediore", StringComparison.Ordinal) == true;
        }

        private bool ShouldApplyUniversalBarrelOffset(Item item, Player player)
        {
            return item.ModItem?.Mod == Mod &&
                   item.DamageType != DamageClass.Summon &&
                   !(IsTedioreWeapon(item) && player.altFunctionUse == 2);
        }

        private void ApplyTedioreThrowVisual(Player player)
        {
            Vector2 aimDirection = Main.MouseWorld - player.MountedCenter;
            if (aimDirection == Vector2.Zero)
            {
                return;
            }

            player.ChangeDir(aimDirection.X >= 0f ? 1 : -1);
            Vector2 visualAimDirection = aimDirection.SafeNormalize(Vector2.UnitX);
            float progress = 1f - tedioreThrowVisualTimer / 15f;
            float throwRotation = aimDirection.ToRotation() - MathHelper.Lerp(0.8f, 0f, progress) * player.direction;

            player.itemRotation = (throwRotation.ToRotationVector2() * player.direction).ToRotation();
            player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, throwRotation - MathHelper.PiOver2);
            player.itemLocation -= visualAimDirection * MathHelper.Lerp(8f, 0f, progress);
        }

        private bool IsBigSucc(Item item)
        {
            return item.ModItem?.GetType().Name == "BigSucc";
        }

        private void RecoverAccuracyBeforeShot(Item item)
        {
            ulong elapsedTicks = Main.GameUpdateCount - lastProjectileShotTick;
            ulong recoveryTicks = elapsedTicks > (ulong)item.useTime
                ? elapsedTicks - (ulong)item.useTime
                : 0;

            if (IsHyperionWeapon(item))
            {
                hyperionAccuracy = MathHelper.Clamp(hyperionAccuracy - HyperionAccuracyDecay * recoveryTicks, 0f, HyperionAccuracyCap);
            }
            else
            {
                weaponInaccuracy = MathHelper.Clamp(weaponInaccuracy - WeaponInaccuracyDecay * recoveryTicks, 0f, WeaponMaximumInaccuracy);
            }

            lastProjectileShotTick = Main.GameUpdateCount;
        }
    }
}