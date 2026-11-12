# Hace una copia de seguridad de la base ahora y abre la carpeta.
. "$PSScriptRoot\_comun.ps1"
try {
    & $Exe --backup
    $xml = Leer-Config
    $dest = Get-Ajuste $xml 'BackupDir'
    if (-not $dest) { $dest = 'backups' }
    if (-not [IO.Path]::IsPathRooted($dest)) { $dest = Join-Path $Dir $dest }
    if (Test-Path $dest) { Start-Process explorer.exe $dest }
}
catch {
    Write-Host "ERROR: $($_.Exception.Message)" -ForegroundColor Red
}
Pausa
