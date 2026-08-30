# Remove SetItemSound calls from SetDefaults methods since sounds are now handled by UseItem override
# This prevents the sound from being set without pitch variation

param(
    [switch]$Execute = $false
)

$basePath = "C:\Users\jessi\OneDrive\Documents\Coding Projects\Terraria\Mods\Vaultaria"

# Files that have simple SetItemSound calls that can be safely removed from SetDefaults
# These are the ones that just have a single SetItemSound(Item, Sounds.X, 60) in SetDefaults
$filesToUpdate = @(
    "Content\Items\Weapons\Ranged\Common\AssaultRifle\Torgue\LumpyRoot.cs",
    "Content\Items\Weapons\Ranged\Common\AssaultRifle\Vladof\GearboxRenegade.cs",
    "Content\Items\Weapons\Ranged\Common\Pistol\Dahl\BasicRepeater.cs",
    "Content\Items\Weapons\Ranged\Common\Pistol\Maliwan\Aegis.cs",
    "Content\Items\Weapons\Ranged\Common\Shotgun\Bandit\Skatergun.cs",
    "Content\Items\Weapons\Ranged\Common\SMG\Dahl\SmoothFox.cs",
    "Content\Items\Weapons\Ranged\Common\SMG\Hyperion\GearboxProjectileConvergence.cs",
    "Content\Items\Weapons\Ranged\Common\Sniper\Jakobs\GearboxMuckamuck.cs",
    "Content\Items\Weapons\Ranged\Effervescent\Launcher\Torgue\WorldBurn.cs",
    "Content\Items\Weapons\Ranged\Effervescent\Pistol\Jakobs\Prototype2599.cs",
    "Content\Items\Weapons\Ranged\Eridian\EridianFabricator.cs",
    "Content\Items\Weapons\Ranged\Legendary\AssaultRifle\Torgue\Ogre.cs",
    "Content\Items\Weapons\Ranged\Legendary\AssaultRifle\Vladof\Blackout.cs",
    "Content\Items\Weapons\Ranged\Legendary\AssaultRifle\Vladof\Shredifier.cs",
    "Content\Items\Weapons\Ranged\Legendary\Laser\Dahl\CatONineTails.cs",
    "Content\Items\Weapons\Ranged\Legendary\Launcher\Bandit\Badaboom.cs",
    "Content\Items\Weapons\Ranged\Legendary\Launcher\Maliwan\Norfleet.cs",
    "Content\Items\Weapons\Ranged\Legendary\Launcher\Torgue\Nukem.cs",
    "Content\Items\Weapons\Ranged\Legendary\Pistol\Bandit\Gub.cs",
    "Content\Items\Weapons\Ranged\Legendary\Pistol\Bandit\Zim.cs",
    "Content\Items\Weapons\Ranged\Legendary\Pistol\Dahl\Hornet.cs",
    "Content\Items\Weapons\Ranged\Legendary\Pistol\Hyperion\LogansGun.cs",
    "Content\Items\Weapons\Ranged\Legendary\Pistol\Jakobs\LuckCannon.cs",
    "Content\Items\Weapons\Ranged\Legendary\Pistol\Jakobs\Maggie.cs",
    "Content\Items\Weapons\Ranged\Legendary\Pistol\Jakobs\Oracle.cs",
    "Content\Items\Weapons\Ranged\Legendary\Pistol\Torgue\UnkemptHarold.cs",
    "Content\Items\Weapons\Ranged\Legendary\Pistol\Vladof\Infinity.cs",
    "Content\Items\Weapons\Ranged\Legendary\Pistol\Vladof\LightShow.cs",
    "Content\Items\Weapons\Ranged\Legendary\Shotgun\Hyperion\FacePuncher.cs",
    "Content\Items\Weapons\Ranged\Legendary\Shotgun\Jakobs\Striker.cs",
    "Content\Items\Weapons\Ranged\Legendary\Shotgun\Torgue\SwordSplosion.cs",
    "Content\Items\Weapons\Ranged\Legendary\SMG\Dahl\NightHawkin.cs",
    "Content\Items\Weapons\Ranged\Legendary\SMG\Hyperion\AkumasDemise.cs",
    "Content\Items\Weapons\Ranged\Legendary\SMG\Maliwan\CloudKill.cs",
    "Content\Items\Weapons\Ranged\Legendary\SMG\Maliwan\Hellfire.cs",
    "Content\Items\Weapons\Ranged\Legendary\Sniper\Jakobs\Skullmasher.cs",
    "Content\Items\Weapons\Ranged\Legendary\Sniper\Maliwan\Volcano.cs",
    "Content\Items\Weapons\Ranged\Legendary\Sniper\Vladof\Lyuda.cs",
    "Content\Items\Weapons\Ranged\Pearlescent\AssaultRifle\Bandit\Sawbar.cs",
    "Content\Items\Weapons\Ranged\Pearlescent\Shotgun\Hyperion\Butcher.cs",
    "Content\Items\Weapons\Ranged\Rare\AssaultRifle\Vladof\Hail.cs",
    "Content\Items\Weapons\Ranged\Rare\AssaultRifle\Vladof\OlPainful.cs",
    "Content\Items\Weapons\Ranged\Rare\Launcher\Maliwan\Hive.cs",
    "Content\Items\Weapons\Ranged\Rare\Pistol\Hyperion\Fibber.cs",
    "Content\Items\Weapons\Ranged\Rare\Pistol\Hyperion\LadyFist.cs",
    "Content\Items\Weapons\Ranged\Rare\Pistol\Hyperion\Taser.cs",
    "Content\Items\Weapons\Ranged\Rare\Pistol\Jakobs\CyberEagle.cs",
    "Content\Items\Weapons\Ranged\Rare\Pistol\Jakobs\Law.cs",
    "Content\Items\Weapons\Ranged\Rare\Pistol\Maliwan\GrogNozzle.cs",
    "Content\Items\Weapons\Ranged\Rare\Shotgun\Hyperion\HeartBreaker.cs",
    "Content\Items\Weapons\Ranged\Rare\Shotgun\Jakobs\Boomacorn.cs",
    "Content\Items\Weapons\Ranged\Rare\Shotgun\Jakobs\OrphanMaker.cs",
    "Content\Items\Weapons\Ranged\Rare\Shotgun\Jakobs\Quad.cs",
    "Content\Items\Weapons\Ranged\Rare\Shotgun\Jakobs\TooScoops.cs",
    "Content\Items\Weapons\Ranged\Rare\Shotgun\Torgue\Wombat.cs",
    "Content\Items\Weapons\Ranged\Rare\SMG\Bandit\Orc.cs",
    "Content\Items\Weapons\Ranged\Rare\SMG\Dahl\Lascaux.cs",
    "Content\Items\Weapons\Ranged\Rare\SMG\Hyperion\Bane.cs",
    "Content\Items\Weapons\Ranged\Rare\SMG\Maliwan\Revenant.cs",
    "Content\Items\Weapons\Ranged\Rare\Sniper\Dahl\NightSniper.cs",
    "Content\Items\Weapons\Ranged\Rare\Sniper\Hyperion\InspiringTransaction.cs",
    "Content\Items\Weapons\Ranged\Rare\Sniper\Jakobs\Cobra.cs",
    "Content\Items\Weapons\Ranged\Rare\Sniper\Jakobs\Trespasser.cs",
    "Content\Items\Weapons\Ranged\Rare\Sniper\Maliwan\Pimpernel.cs",
    "Content\Items\Weapons\Ranged\Seraph\AssaultRifle\Dahl\Seraphim.cs",
    "Content\Items\Weapons\Ranged\Seraph\AssaultRifle\Vladof\LeadStorm.cs",
    "Content\Items\Weapons\Ranged\Seraph\SMG\Hyperion\FirstBlood.cs",
    "Content\Items\Weapons\Ranged\Seraph\SMG\Maliwan\Florentine.cs",
    "Content\Items\Weapons\Ranged\Uncommon\AssaultRifle\Dahl\Carbine.cs",
    "Content\Items\Weapons\Ranged\Uncommon\AssaultRifle\Jakobs\FlushRifle.cs",
    "Content\Items\Weapons\Ranged\Uncommon\Shotgun\Torgue\ThreeWayHulk.cs",
    "Content\Items\Weapons\Ranged\Uncommon\Sniper\Maliwan\Snider.cs"
)

$updatedCount = 0

foreach ($relativeFile in $filesToUpdate) {
    $filePath = Join-Path -Path $basePath -ChildPath $relativeFile
    
    if (-not (Test-Path $filePath)) {
        Write-Host "[WARN] File not found: $relativeFile"
        continue
    }
    
    $content = Get-Content $filePath -Raw
    
    # Remove lines like: SetItemSound(Item, Sounds.X, 60);
    # But keep it on same line (don't want to accidentally remove comments or other code)
    $newContent = $content -replace '\s*SetItemSound\(Item, Sounds\.\w+, \d+\);\s*\n', "`n"
    
    if ($newContent -eq $content) {
        Write-Host "[OK] No SetItemSound to remove: $relativeFile"
        continue
    }
    
    if ($Execute) {
        Set-Content $filePath $newContent -Encoding UTF8
        Write-Host "[OK] Removed SetItemSound: $relativeFile"
    } else {
        Write-Host "[DRY-RUN] Would remove SetItemSound from: $relativeFile"
    }
    
    $updatedCount++
}

Write-Host ""
Write-Host "Summary: Processed $updatedCount files"
if (-not $Execute) {
    Write-Host "[DRY-RUN MODE] Run with -Execute to remove SetItemSound calls"
}
