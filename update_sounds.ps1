# Update all weapon files to use ItemSounds arrays instead of direct SetItemSound calls
# This script processes all weapon files and:
# 1. Changes ItemSounds => [] to ItemSounds => new[] { Sounds.X }
# 2. Removes simple SetItemSound calls from SetDefaults

param(
    [switch]$DryRun = $false,  # Set to $true to see what would change without making changes
    [switch]$Execute = $false  # Set to $true to actually make changes
)

$basePath = "C:\Users\jessi\OneDrive\Documents\Coding Projects\Terraria\Mods\Vaultaria"
$weaponPath = "$basePath\Content\Items\Weapons"

# Mapping of files to their primary sound from SetItemSound calls
# Format: "relative/path/to/File.cs" = "SoundName"
$soundMappings = @{
    "Ranged\Common\AssaultRifle\Torgue\LumpyRoot.cs" = "TorgueAR"
    "Ranged\Common\AssaultRifle\Vladof\GearboxRenegade.cs" = "VladofAR"
    "Ranged\Common\Pistol\Dahl\BasicRepeater.cs" = "DahlPistolBurst"
    "Ranged\Common\Pistol\Maliwan\Aegis.cs" = "MaliwanPistol"
    "Ranged\Common\Pistol\Tediore\Handgun.cs" = "TediorePistol"
    "Ranged\Common\Shotgun\Bandit\Skatergun.cs" = "BanditShotgun"
    "Ranged\Common\SMG\Dahl\SmoothFox.cs" = "DahlSMGBurst"
    "Ranged\Common\SMG\Hyperion\GearboxProjectileConvergence.cs" = "HyperionSMG"
    "Ranged\Common\Sniper\Jakobs\GearboxMuckamuck.cs" = "JakobsSniper"
    "Ranged\Effervescent\Launcher\Torgue\WorldBurn.cs" = "TorgueLauncher"
    "Ranged\Effervescent\Pistol\Jakobs\Prototype2599.cs" = "JakobsPistol"
    "Ranged\Eridian\EridianFabricator.cs" = "LegendaryDrop"
    "Ranged\Legendary\AssaultRifle\Torgue\Ogre.cs" = "TorgueAR"
    "Ranged\Legendary\AssaultRifle\Vladof\Blackout.cs" = "ETechARSingle"
    "Ranged\Legendary\AssaultRifle\Vladof\Shredifier.cs" = "VladofAR"
    "Ranged\Legendary\Laser\Dahl\CatONineTails.cs" = "GenericLaser"
    "Ranged\Legendary\Laser\Tediore\LaserDisker.cs" = "LaserDisker"
    "Ranged\Legendary\Launcher\Bandit\Badaboom.cs" = "BanditLauncher"
    "Ranged\Legendary\Launcher\Maliwan\Norfleet.cs" = "Norfleet"
    "Ranged\Legendary\Launcher\Torgue\Nukem.cs" = "TorgueLauncher"
    "Ranged\Legendary\Pistol\Bandit\Gub.cs" = "BanditPistol"
    "Ranged\Legendary\Pistol\Bandit\Zim.cs" = "BanditPistol"
    "Ranged\Legendary\Pistol\Dahl\Hornet.cs" = "DahlPistolBurst"
    "Ranged\Legendary\Pistol\Hyperion\LogansGun.cs" = "HyperionPistol"
    "Ranged\Legendary\Pistol\Jakobs\LuckCannon.cs" = "JakobsPistol"
    "Ranged\Legendary\Pistol\Jakobs\Maggie.cs" = "JakobsPistol"
    "Ranged\Legendary\Pistol\Jakobs\Oracle.cs" = "JakobsPistol"
    "Ranged\Legendary\Pistol\Torgue\UnkemptHarold.cs" = "TorguePistol"
    "Ranged\Legendary\Pistol\Vladof\Infinity.cs" = "VladofPistol"
    "Ranged\Legendary\Pistol\Vladof\LightShow.cs" = "VladofPistol"
    "Ranged\Legendary\Shotgun\Hyperion\FacePuncher.cs" = "HyperionShotgun"
    "Ranged\Legendary\Shotgun\Jakobs\Striker.cs" = "JakobsShotgun"
    "Ranged\Legendary\Shotgun\Tediore\Deliverance.cs" = "TedioreShotgun"
    "Ranged\Legendary\Shotgun\Torgue\Flakker.cs" = "TorgueShotgun"
    "Ranged\Legendary\Shotgun\Torgue\SwordSplosion.cs" = "TorgueShotgun"
    "Ranged\Legendary\SMG\Dahl\NightHawkin.cs" = "DahlSMGBurst"
    "Ranged\Legendary\SMG\Hyperion\AkumasDemise.cs" = "ETechSMGSingle"
    "Ranged\Legendary\SMG\Maliwan\CloudKill.cs" = "MaliwanSMG"
    "Ranged\Legendary\SMG\Maliwan\Hellfire.cs" = "MaliwanSMG"
    "Ranged\Legendary\SMG\Maliwan\PlasmaCoil.cs" = "PlasmaCoil"
    "Ranged\Legendary\SMG\Tediore\BabyMaker.cs" = "TedioreSMG"
    "Ranged\Legendary\Sniper\Jakobs\Skullmasher.cs" = "JakobsSniper"
    "Ranged\Legendary\Sniper\Maliwan\Volcano.cs" = "MaliwanSniper"
    "Ranged\Legendary\Sniper\Vladof\Lyuda.cs" = "VladofSniper"
    "Ranged\Legendary\Sniper\Vladof\Shockblast.cs" = "ETechSniperSingle"
    "Ranged\Pearlescent\AssaultRifle\Bandit\Sawbar.cs" = "BanditAR"
    "Ranged\Pearlescent\Shotgun\Hyperion\Butcher.cs" = "HyperionShotgun"
    "Ranged\Rare\AssaultRifle\Vladof\Hail.cs" = "VladofAR"
    "Ranged\Rare\AssaultRifle\Vladof\OlPainful.cs" = "GenericLaser"
    "Ranged\Rare\AssaultRifle\Vladof\Rapier.cs" = "VladofAR"
    "Ranged\Rare\Launcher\Maliwan\Hive.cs" = "MaliwanLauncher"
    "Ranged\Rare\Pistol\Hyperion\Fibber.cs" = "HyperionPistol"
    "Ranged\Rare\Pistol\Hyperion\LadyFist.cs" = "HyperionPistol"
    "Ranged\Rare\Pistol\Hyperion\Taser.cs" = "GenericLaser"
    "Ranged\Rare\Pistol\Jakobs\CyberEagle.cs" = "MaliwanLaserSingle"
    "Ranged\Rare\Pistol\Jakobs\Law.cs" = "JakobsPistol"
    "Ranged\Rare\Pistol\Maliwan\GrogNozzle.cs" = "MaliwanPistol"
    "Ranged\Rare\Shotgun\Hyperion\HeartBreaker.cs" = "HyperionShotgun"
    "Ranged\Rare\Shotgun\Jakobs\Boomacorn.cs" = "Boomacorn"
    "Ranged\Rare\Shotgun\Jakobs\OrphanMaker.cs" = "JakobsShotgun"
    "Ranged\Rare\Shotgun\Jakobs\Quad.cs" = "JakobsShotgun"
    "Ranged\Rare\Shotgun\Jakobs\TooScoops.cs" = "JakobsShotgun"
    "Ranged\Rare\Shotgun\Torgue\Wombat.cs" = "TorgueShotgun"
    "Ranged\Rare\SMG\Bandit\Orc.cs" = "BanditSMG"
    "Ranged\Rare\SMG\Dahl\Lascaux.cs" = "LascauxBurst"
    "Ranged\Rare\SMG\Hyperion\Bane.cs" = "Bane"
    "Ranged\Rare\SMG\Maliwan\Revenant.cs" = "MaliwanSMG"
    "Ranged\Rare\Sniper\Dahl\NightSniper.cs" = "DahlSniperBurst"
    "Ranged\Rare\Sniper\Hyperion\InspiringTransaction.cs" = "HyperionSniper"
    "Ranged\Rare\Sniper\Jakobs\Cobra.cs" = "JakobsSniper"
    "Ranged\Rare\Sniper\Jakobs\Trespasser.cs" = "JakobsSniper"
    "Ranged\Rare\Sniper\Maliwan\Pimpernel.cs" = "MaliwanSniper"
    "Ranged\Seraph\AssaultRifle\Dahl\Seraphim.cs" = "DahlARBurst"
    "Ranged\Seraph\AssaultRifle\Vladof\LeadStorm.cs" = "VladofAR"
    "Ranged\Seraph\SMG\Hyperion\FirstBlood.cs" = "HyperionSMG"
    "Ranged\Seraph\SMG\Maliwan\Florentine.cs" = "ETechSMGSingle"
    "Ranged\Uncommon\AssaultRifle\Dahl\Carbine.cs" = "DahlARBurst"
    "Ranged\Uncommon\AssaultRifle\Jakobs\FlushRifle.cs" = "JakobsAR"
    "Ranged\Uncommon\Shotgun\Torgue\ThreeWayHulk.cs" = "TorgueShotgun"
    "Ranged\Uncommon\Sniper\Maliwan\Snider.cs" = "MaliwanSniper"
    "Magic\PhaselockSpell.cs" = "PhaselockBase"
    "Melee\ZerosSword.cs" = "Execute"
    "Summoner\Sentry\DigiClone.cs" = "DigiCloneSpawn"
}

$updatedCount = 0
$errorCount = 0

foreach ($relativePath in $soundMappings.Keys) {
    $filePath = Join-Path -Path $basePath -ChildPath "Content\Items\Weapons\$relativePath"
    $sound = $soundMappings[$relativePath]
    
    if (-not (Test-Path $filePath)) {
        Write-Host "[WARN] File not found: $filePath"
        $errorCount++
        continue
    }
    
    $content = Get-Content $filePath -Raw
    
    # Check if file already has been updated
    if ($content -match "ItemSounds => new\[\].*Sounds\.$sound") {
        Write-Host "[OK] Already updated: $relativePath"
        continue
    }
    
    # Replace ItemSounds => [] with ItemSounds => new[] { Sounds.X }
    # Regex pattern to match: protected override Sounds[] ItemSounds => [];
    $oldPattern = "protected override Sounds\[\] ItemSounds => \[\];"
    $newValue = "protected override Sounds[] ItemSounds => new[] { Sounds.$sound };"
    $newContent = $content -replace $oldPattern, $newValue
    
    if ($newContent -eq $content) {
        Write-Host "[WARN] No changes made to: $relativePath"
        $errorCount++
        continue
    }
    
    if ($Execute) {
        Set-Content $filePath $newContent -Encoding UTF8
        Write-Host "[OK] Updated: $relativePath"
    } else {
        Write-Host "[DRY-RUN] Would update: $relativePath to use Sounds.$sound"
    }
    
    $updatedCount++
}

Write-Host ""
Write-Host "Summary: Updated $updatedCount files, $errorCount errors"
if (-not $Execute) {
    Write-Host "[DRY-RUN MODE] Run with -DryRun to preview changes"
} else {
    Write-Host "[CHANGES APPLIED]"
}
