# Instancia de DESARROLLO de Pacientes Web (no toca la instalada en C:\Pacientes).
#   - Sirve wwwroot directamente desde el repo: editar html/css/js y recargar el navegador (F5) alcanza.
#   - Usa datos-prueba\dbPacientes.db (datos inventados), no la base real.
#   - Cambios en C# (Server4 / Pacientes.Api / Pacientes.Web): cerrar y volver a ejecutar dev.bat.
# Uso:   dev.bat            -> solo esta PC:  http://localhost:8081
#        dev.bat -Lan       -> tambien desde el celular (pide administrador la primera vez)
param([switch]$Lan, [int]$Puerto = 8081, [switch]$NoBrowser)
$ErrorActionPreference = 'Stop'
$Repo = $PSScriptRoot
. "$Repo\Pacientes.Web\deploy\_comun.ps1"      # (define Asegurar-Admin; ojo: pisa $Dir/$Exe, se redefinen abajo)
$Proyecto = Join-Path $Repo 'Pacientes.Web'
$Salida   = Join-Path $Proyecto 'bin\Debug\net48'
$Exe      = Join-Path $Salida 'Pacientes.Web.exe'
$Web      = Join-Path $Proyecto 'wwwroot'
$Bd       = Join-Path $Repo 'datos-prueba\dbPacientes.db'

try {
    if ($Lan) { Asegurar-Admin $PSCommandPath @('-Lan', '-Puerto', $Puerto) }

    # Cerrar una instancia de desarrollo anterior (si no, no se puede recompilar)
    Get-Process -Name 'Pacientes.Web' -ErrorAction SilentlyContinue |
        Where-Object { $_.Path -like "$Salida*" } | Stop-Process -Force
    Start-Sleep -Milliseconds 500

    Write-Host 'Compilando...' -ForegroundColor Cyan
    dotnet build (Join-Path $Proyecto 'Pacientes.Web.csproj') -v q --nologo | Out-Host
    if ($LASTEXITCODE -ne 0) { throw 'La compilacion fallo.' }

    if (-not (Test-Path $Bd)) { throw "Falta la base de prueba: $Bd" }
    if (-not (Test-Path (Join-Path $Salida 'auth.dat'))) {
        Write-Host 'Primera vez: defina un PIN para el entorno de desarrollo (min. 6 caracteres).' -ForegroundColor Cyan
        do { & $Exe --set-pin } while ($LASTEXITCODE -ne 0)
    }

    if ($Lan) {
        netsh http delete urlacl url=http://+:$Puerto/ 2>&1 | Out-Null
        netsh http add urlacl url=http://+:$Puerto/ 'sddl=D:(A;;GX;;;WD)' | Out-Null
        Get-NetFirewallRule -DisplayName 'Pacientes Web (dev)' -ErrorAction SilentlyContinue | Remove-NetFirewallRule
        New-NetFirewallRule -DisplayName 'Pacientes Web (dev)' -Direction Inbound -Action Allow -Protocol TCP -LocalPort $Puerto `
            -Profile Private, Domain -RemoteAddress LocalSubnet -Program $Exe | Out-Null
        $url = "http://+:$Puerto/"
    } else {
        $url = "http://localhost:$Puerto/"
    }

    Write-Host ''
    Write-Host "DESARROLLO en  http://localhost:$Puerto" -ForegroundColor Green
    if ($Lan) { Get-DireccionesLan | ForEach-Object { Write-Host "  desde el celular:  http://${_}:$Puerto" } }
    Write-Host "  web:  $Web   (editar y recargar con F5)"
    Write-Host "  base: $Bd"
    Write-Host '  Ctrl+C para detener.'
    Write-Host ''
    if (-not $NoBrowser) { Start-Process "http://localhost:$Puerto/" }

    Set-Location $Salida
    & $Exe "--url=$url" "--db=$Bd" "--wwwroot=$Web"
}
catch {
    Write-Host "ERROR: $($_.Exception.Message)" -ForegroundColor Red
    Read-Host 'Presione Enter para cerrar' | Out-Null
}
