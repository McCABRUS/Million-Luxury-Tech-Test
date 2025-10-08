param(
    [string]$MongoUri = $(if ($env:MONGO_URI) { $env:MONGO_URI } else { "mongodb://localhost:27017/realestate" }),
    [string]$FixturesDir = ".\db\fixtures",
    [switch]$DropCollections
)

function Test-Command {
    param([string]$Cmd)
    $null -ne (Get-Command $Cmd -ErrorAction SilentlyContinue)
}

if (-not (Test-Command -Cmd "mongoimport")) {
    Write-Error "mongoimport is not available in PATH. Install MongoDB Database Tools or add mongoimport to PATH."
    exit 1
}

if (-not (Test-Path $FixturesDir)) {
    Write-Error "Fixtures directory not found: $FixturesDir"
    exit 1
}

$collections = @{
    "realestate.owners"     = "owners"
    "realestate.properties" = "properties"
    "realestate.propertyImages" = "propertyImages"
    "realestate.propertyTraces" = "propertyTraces"
}

foreach ($file in $collections.Keys) {
    $path = Join-Path $FixturesDir $file
    if (-not (Test-Path $path)) {
        Write-Warning "Skipping $file because it was not found at $path"
        continue
    }

    $collection = $collections[$file]
    $dropArg = $DropCollections.IsPresent ? "--drop" : ""
    $args = @("--uri", $MongoUri, "--collection", $collection, "--file", $path, "--jsonArray", $dropArg) | Where-Object { $_ -ne "" }

    Write-Host "Importing $file -> $collection"
    $proc = Start-Process -FilePath "mongoimport" -ArgumentList $args -NoNewWindow -Wait -PassThru
    if ($proc.ExitCode -ne 0) {
        Write-Error "mongoimport failed for $file with exit code $($proc.ExitCode)"
        exit $proc.ExitCode
    }
}

Write-Host "Import completed successfully."