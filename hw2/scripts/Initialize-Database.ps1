[CmdletBinding()]
param(
    [string]$DatabaseName = "Streaming"
)

$ErrorActionPreference = "Stop"

$containerName = "cs409-hw2-sqlserver"
$hw2Root = Split-Path -Parent $PSScriptRoot
$dataPath = Join-Path $hw2Root "data"
$envFile = Join-Path $hw2Root ".env"
$sqlFiles = @("up.sql", "seed_1.sql", "seed_2.sql", "seed_3.sql")

if (-not (Test-Path $envFile)) {
    throw "Copy .env.example to .env first."
}

$passwordLine = Get-Content $envFile |
    Where-Object { $_ -match '^MSSQL_SA_PASSWORD=' } |
    Select-Object -First 1
$password = ($passwordLine -split '=', 2)[1]

$sqlCmdPath = "/opt/mssql-tools18/bin/sqlcmd"
docker exec $containerName test -x $sqlCmdPath 2>$null
if ($LASTEXITCODE -ne 0) {
    $sqlCmdPath = "/opt/mssql-tools/bin/sqlcmd"
}

Write-Host "Waiting for SQL Server to accept connections..."
for ($attempt = 1; $attempt -le 60; $attempt++) {
    docker exec $containerName $sqlCmdPath -S localhost -U sa -P $password -C -Q "SELECT 1" 2>$null | Out-Null
    if ($LASTEXITCODE -eq 0) {
        break
    }
    Start-Sleep -Seconds 2
}

docker exec $containerName $sqlCmdPath -S localhost -U sa -P $password -C -b `
    -Q "IF DB_ID('$DatabaseName') IS NULL CREATE DATABASE [$DatabaseName];"

foreach ($sqlFile in $sqlFiles) {
    $localPath = Join-Path $dataPath $sqlFile
    $containerPath = "/tmp/hw2-$sqlFile"

    Write-Host "Applying $sqlFile..."
    docker cp $localPath "${containerName}:$containerPath" | Out-Null
    docker exec $containerName $sqlCmdPath -S localhost -U sa -P $password -C -b `
        -d $DatabaseName -i $containerPath
}

Write-Host "Database setup complete."

