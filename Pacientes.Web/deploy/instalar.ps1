# Instala Pacientes Web en esta PC: inicio automatico, firewall (solo red local) y copias de seguridad.
# Se puede ejecutar de nuevo sin problema (actualiza lo que ya existe). Use -Simular para ver los pasos sin aplicar nada.
param([switch]$Simular)
. "$PSScriptRoot\_comun.ps1"

function Hacer([string]$texto, [scriptblock]$accion) {
    Write-Host " - $texto"
    if (-not $Simular) { & $accion }
}

try {
    if (-not $Simular) { Asegurar-Admin $PSCommandPath }
    # Registro de todo lo que se muestra (sin incluir el PIN, que se escribe oculto).
    try { Start-Transcript -Path (Join-Path $Dir 'instalar.log') -Force | Out-Null } catch { }
    Write-Host ''
    Write-Host '=== Instalacion de Pacientes Web ===' -ForegroundColor Green
    if ($Simular) { Write-Host '(MODO SIMULACION: no se cambia nada en el sistema)' -ForegroundColor Yellow }

    if (-not (Test-Path $Exe)) { throw "No se encontro Pacientes.Web.exe en $Dir" }
    $xml    = Leer-Config
    $puerto = Get-Puerto $xml

    # --- 1. Base de datos ---
    $bd = Get-RutaBd $xml
    while (-not (Test-Path -LiteralPath $bd)) {
        Write-Host ''
        Write-Host "No se encontro la base de datos: $bd" -ForegroundColor Yellow
        $r = (Read-Host 'Escriba la ruta completa de su dbPacientes.db (Enter para cancelar)').Trim().Trim('"')
        if (-not $r) { throw 'Instalacion cancelada: falta la base de datos.' }
        if (Test-Path -LiteralPath $r -PathType Leaf) {
            $bd = $r
            Set-Ajuste $xml 'DatabasePath' $bd
            if (-not $Simular) { Guardar-Config $xml }
        }
    }
    Write-Host "Base de datos: $bd"

    # --- 2. Carpeta extra para copias (opcional) ---
    if (-not (Get-Ajuste $xml 'BackupDir')) {
        Write-Host ''
        Write-Host 'Las copias de seguridad diarias se guardan en la carpeta "backups" junto al programa.'
        Write-Host 'Conviene guardarlas tambien en OTRO disco o carpeta sincronizada (disco externo, OneDrive, etc.).'
        $extra = (Read-Host 'Ruta de esa carpeta (Enter para usar solo "backups")').Trim().Trim('"')
        if ($extra) {
            Set-Ajuste $xml 'BackupDir' $extra
            if (-not $Simular) { Guardar-Config $xml }
        }
    }

    # --- 3. PIN ---
    if (-not (Test-Path (Join-Path $Dir 'auth.dat'))) {
        Write-Host ''
        Write-Host 'Defina el PIN de acceso (minimo 6 caracteres). Es la clave que protege los datos de los pacientes.' -ForegroundColor Cyan
        if ($Simular) { Write-Host ' - (simulacion) se pediria el PIN' }
        else {
            do { & $Exe --set-pin } while ($LASTEXITCODE -ne 0)
        }
    } else {
        Write-Host 'PIN: ya configurado (use cambiar-pin.bat para modificarlo).'
    }

    Write-Host ''
    Write-Host 'Aplicando configuracion...' -ForegroundColor Cyan

    # --- 4. Copia inicial de la base ---
    Hacer 'Copia de seguridad inicial' {
        & $Exe --backup
    }

    # --- 5. Reserva de direccion HTTP (permite escuchar en la red sin ser administrador) ---
    Hacer "Reserva de http://+:$puerto/ para el servicio" {
        netsh http delete urlacl url=http://+:$puerto/ 2>&1 | Out-Null
        netsh http add urlacl url=http://+:$puerto/ 'sddl=D:(A;;GX;;;WD)' | Out-Null
        if ($LASTEXITCODE -ne 0) { throw 'No se pudo reservar la direccion HTTP.' }
    }

    # --- 6. Firewall: solo redes privadas y solo la subred local ---
    Hacer "Firewall: permitir el puerto $puerto solo en red privada y subred local" {
        Get-NetFirewallRule -DisplayName $ReglaFw -ErrorAction SilentlyContinue | Remove-NetFirewallRule
        New-NetFirewallRule -DisplayName $ReglaFw -Direction Inbound -Action Allow -Protocol TCP -LocalPort $puerto `
            -Profile Private, Domain -RemoteAddress LocalSubnet -Program $Exe | Out-Null
    }

    # --- 7. La red Wi-Fi no debe estar como "Publica" (Windows bloquea todo lo entrante) ---
    $publicas = @(Get-NetConnectionProfile -ErrorAction SilentlyContinue | Where-Object { $_.NetworkCategory -eq 'Public' })
    foreach ($n in $publicas) {
        Write-Host ''
        Write-Host "La red '$($n.Name)' esta marcada como PUBLICA: el celular no podria conectarse." -ForegroundColor Yellow
        $resp = Read-Host 'Es la red de su consultorio/casa? Cambiarla a PRIVADA (S/N)'
        if ($resp -match '^[sSyY]') {
            Hacer "Red '$($n.Name)' -> Privada" { Set-NetConnectionProfile -InterfaceIndex $n.InterfaceIndex -NetworkCategory Private }
        }
    }

    # --- 8. Inicio automatico (al arrancar Windows, sin necesidad de iniciar sesion) ---
    Hacer "Tarea '$TareaServ': inicia con Windows y se reinicia sola si falla" {
        Get-ScheduledTask -TaskName $TareaServ -ErrorAction SilentlyContinue | Stop-ScheduledTask -ErrorAction SilentlyContinue
        # Ruta completa (y entre comillas): no depende de la carpeta actual ni de espacios en la ruta.
        $accion = New-ScheduledTaskAction -Execute 'cmd.exe' -Argument "/c `"`"$Dir\iniciar.bat`"`"" -WorkingDirectory $Dir
        $disp   = New-ScheduledTaskTrigger -AtStartup
        $princ  = New-ScheduledTaskPrincipal -UserId 'SYSTEM' -LogonType ServiceAccount -RunLevel Highest
        $conf   = New-ScheduledTaskSettingsSet -AllowStartIfOnBatteries -DontStopIfGoingOnBatteries -StartWhenAvailable `
                    -RestartCount 999 -RestartInterval (New-TimeSpan -Minutes 1) -ExecutionTimeLimit ([TimeSpan]::Zero) -Hidden
        Register-ScheduledTask -TaskName $TareaServ -Action $accion -Trigger $disp -Principal $princ -Settings $conf -Force | Out-Null
    }

    # --- 9. Copia diaria ---
    Hacer "Tarea '$TareaBackup': copia diaria a las 22:00" {
        $accion = New-ScheduledTaskAction -Execute $Exe -Argument '--backup' -WorkingDirectory $Dir
        $disp   = New-ScheduledTaskTrigger -Daily -At '22:00'
        $princ  = New-ScheduledTaskPrincipal -UserId 'SYSTEM' -LogonType ServiceAccount -RunLevel Highest
        $conf   = New-ScheduledTaskSettingsSet -StartWhenAvailable -AllowStartIfOnBatteries -DontStopIfGoingOnBatteries -Hidden
        Register-ScheduledTask -TaskName $TareaBackup -Action $accion -Trigger $disp -Principal $princ -Settings $conf -Force | Out-Null
    }

    # --- 10. Arrancar y verificar ---
    Hacer 'Iniciando el servicio' {
        Start-ScheduledTask -TaskName $TareaServ
        $ok = $false
        for ($i = 0; $i -lt 25; $i++) {
            try { Invoke-RestMethod "http://localhost:$puerto/api/ping" -TimeoutSec 2 | Out-Null; $ok = $true; break }
            catch { Start-Sleep -Seconds 1 }
        }
        if (-not $ok) {
            Write-Host 'El servicio no respondio. Ultimas lineas del registro:' -ForegroundColor Red
            $log = Join-Path $Dir 'logs\pacientes.log'
            if (Test-Path $log) { Get-Content $log -Tail 15 }
            throw 'No se pudo iniciar Pacientes Web.'
        }
    }

    Write-Host ''
    Write-Host '=== Listo ===' -ForegroundColor Green
    Write-Host 'Pacientes Web funciona y se inicia solo cada vez que se enciende la PC.'
    $ips = @(Get-DireccionesLan)
    foreach ($ip in $ips) { Write-Host "  Direccion para el celular:  http://${ip}:$puerto" }
    Write-Host ''
    Write-Host 'Recomendado: en el router, reserve esa IP para esta PC (reserva DHCP) para que no cambie.'
    Write-Host 'Si Avast u otro antivirus lo bloquea, agregue esta carpeta a las exclusiones:'
    Write-Host "  $Dir"
    Write-Host ''
    if (-not $Simular) {
        $resp = Read-Host 'Abrir ahora la pantalla para vincular un celular con QR (S/N)'
        if ($resp -match '^[sSyY]') { Start-Process "http://localhost:$puerto/vincular.html" }
    }
}
catch {
    Write-Host ''
    Write-Host "ERROR: $($_.Exception.Message)" -ForegroundColor Red
    Write-Host $_.ScriptStackTrace
}
try { Stop-Transcript | Out-Null } catch { }
Write-Host "Registro guardado en: $(Join-Path $Dir 'instalar.log')"
Pausa
