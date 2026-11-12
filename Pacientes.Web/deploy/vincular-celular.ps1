# Abre la pantalla con el QR para autorizar un celular (sin escribir el PIN en el telefono).
. "$PSScriptRoot\_comun.ps1"
$puerto = Get-Puerto (Leer-Config)
try {
    Invoke-RestMethod "http://localhost:$puerto/api/ping" -TimeoutSec 3 | Out-Null
    Start-Process "http://localhost:$puerto/vincular.html"
}
catch {
    Write-Host 'Pacientes Web no esta funcionando. Ejecute instalar.bat (o espere un minuto si la PC recien se encendio).' -ForegroundColor Red
    Pausa
}
