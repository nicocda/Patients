# Cambia el PIN. Todos los celulares vinculados deberan ingresar de nuevo (sirve para "cerrar sesion en todos").
param([switch]$Simular)
. "$PSScriptRoot\_comun.ps1"

try {
    if (-not $Simular) { Asegurar-Admin $PSCommandPath }
    Write-Host '=== Cambiar PIN ===' -ForegroundColor Green
    if ($Simular) { Write-Host '(simulacion: no se cambia nada)'; Pausa; return }

    $tarea = Get-ScheduledTask -TaskName $TareaServ -ErrorAction SilentlyContinue
    if ($tarea) { Stop-ScheduledTask -TaskName $TareaServ -ErrorAction SilentlyContinue; Start-Sleep -Seconds 2 }

    do { & $Exe --set-pin } while ($LASTEXITCODE -ne 0)

    if ($tarea) {
        Start-ScheduledTask -TaskName $TareaServ
        Write-Host 'Servicio reiniciado con el PIN nuevo.' -ForegroundColor Green
    } else {
        Write-Host 'PIN guardado. (El servicio no esta instalado: ejecute instalar.bat.)'
    }
}
catch {
    Write-Host "ERROR: $($_.Exception.Message)" -ForegroundColor Red
}
Pausa
