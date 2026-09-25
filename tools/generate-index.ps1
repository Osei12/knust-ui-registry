$RegistryRoot = Split-Path -Parent $PSScriptRoot
$ComponentsDirectory = Join-Path $RegistryRoot "components"
$IndexPath = Join-Path $RegistryRoot "index.json"

Write-Host ""
Write-Host "Generating KNUST UI registry index..." -ForegroundColor Cyan
Write-Host "Registry: $RegistryRoot"
Write-Host ""

$components = @()

$directories = Get-ChildItem -Path $ComponentsDirectory -Directory

foreach ($directory in $directories) {

    $manifestPath = Join-Path $directory.FullName "manifest.json"

    if (-not (Test-Path $manifestPath)) {
        Write-Host "Skipping $($directory.Name): manifest.json not found" -ForegroundColor Yellow
        continue
    }

    Write-Host "Reading $($directory.Name)..."

    try {
        $manifestJson = Get-Content -Path $manifestPath -Raw
        $manifest = $manifestJson | ConvertFrom-Json
    }
    catch {
        Write-Host "Invalid JSON: $manifestPath" -ForegroundColor Red
        throw
    }

    $components += [ordered]@{
        name        = $manifest.name
        displayName = $manifest.displayName
        description = $manifest.description
        version     = $manifest.version
        manifest    = "components/$($directory.Name)/manifest.json"
    }

    Write-Host "  $($manifest.name) v$($manifest.version)" -ForegroundColor Green
}

$components = @(
    $components | Sort-Object -Property name
)

$index = [ordered]@{
    name          = "KNUST UI"
    version       = "1.0.0"
    schemaVersion = "1"
    components    = $components
}

$json = $index | ConvertTo-Json -Depth 10

Set-Content -Path $IndexPath -Value $json -Encoding UTF8

Write-Host ""
Write-Host "index.json generated successfully." -ForegroundColor Green
Write-Host "Components: $($components.Count)"
Write-Host ""