# Quita el inicio automatico, la regla de firewall y la reserva de direccion. NO borra la base de datos ni las copias.
param([switch]$Simular)
. "$PSScriptRoot\_comun.ps1"

function Hacer([string]$texto, [scriptblock]$accion) {
    Write-Host " - $texto"
    if (-not $Simular) { & $accion }
}

try {
    if (-not $Simular) { Asegurar-Admin $PSCommandPath }
    Write-Host '=== Desinstalar Pacientes Web ===' -ForegroundColor Green
    $puerto = Get-Puerto (Leer-Config)

    Hacer 'Deteniendo el servicio' {
        Get-ScheduledTask -TaskName $TareaServ -ErrorAction SilentlyContinue | Stop-ScheduledTask -ErrorAction SilentlyContinue
        Get-Process -Name 'Pacientes.Web' -ErrorAction SilentlyContinue | Stop-Process -Force
    }
    Hacer 'Quitando tareas programadas' {
        Unregister-ScheduledTask -TaskName $TareaServ -Confirm:$false -ErrorAction SilentlyContinue
        Unregister-ScheduledTask -TaskName $TareaBackup -Confirm:$false -ErrorAction SilentlyContinue
    }
    Hacer 'Quitando regla de firewall' {
        Get-NetFirewallRule -DisplayName $ReglaFw -ErrorAction SilentlyContinue | Remove-NetFirewallRule
    }
    Hacer 'Quitando reserva de direccion' {
        netsh http delete urlacl url=http://+:$puerto/ 2>&1 | Out-Null
    }

    Write-Host ''
    Write-Host 'Listo. Se conservaron la base de datos, las copias (backups) y el PIN. La carpeta puede borrarse a mano.'
}
catch {
    Write-Host "ERROR: $($_.Exception.Message)" -ForegroundColor Red
}
Pausa
