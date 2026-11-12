# Funciones compartidas por los scripts de instalacion de Pacientes.Web.
# (Sin acentos a proposito: PowerShell 5.1 lee los .ps1 sin BOM como ANSI.)
$ErrorActionPreference = 'Stop'

$Dir          = $PSScriptRoot
$Exe          = Join-Path $Dir 'Pacientes.Web.exe'
$Cfg          = "$Exe.config"
$TareaServ    = 'Pacientes Web'
$TareaBackup  = 'Pacientes Backup'
$ReglaFw      = 'Pacientes Web'

function Asegurar-Admin([string]$script, [string[]]$extra = @()) {
    $id = [Security.Principal.WindowsIdentity]::GetCurrent()
    $p  = New-Object Security.Principal.WindowsPrincipal($id)
    if ($p.IsInRole([Security.Principal.WindowsBuiltInRole]::Administrator)) { return }
    Write-Host 'Se necesitan permisos de administrador. Windows va a pedir autorizacion...'
    # -NoExit: la ventana elevada no se cierra sola, para poder leer cualquier mensaje o error.
    $a = @('-NoProfile', '-NoExit', '-ExecutionPolicy', 'Bypass', '-File', "`"$script`"") + $extra
    Start-Process powershell -Verb RunAs -ArgumentList $a
    exit
}

function Leer-Config { [xml](Get-Content -LiteralPath $Cfg -Raw) }
function Guardar-Config($xml) { $xml.Save($Cfg) }

function Get-Ajuste($xml, [string]$clave) {
    $n = $xml.configuration.appSettings.add | Where-Object { $_.key -eq $clave }
    if ($n) { $n.value }
}

function Set-Ajuste($xml, [string]$clave, [string]$valor) {
    $n = $xml.configuration.appSettings.add | Where-Object { $_.key -eq $clave }
    if ($n) { $n.value = $valor; return }
    $e = $xml.CreateElement('add')
    $e.SetAttribute('key', $clave)
    $e.SetAttribute('value', $valor)
    $xml.configuration.appSettings.AppendChild($e) | Out-Null
}

function Get-Puerto($xml) {
    $u = Get-Ajuste $xml 'Url'
    if ($u -match ':(\d+)/?$') { [int]$Matches[1] } else { 8080 }
}

function Get-RutaBd($xml) {
    $r = Get-Ajuste $xml 'DatabasePath'
    if (-not $r) { $r = 'dbPacientes.db' }
    if ([IO.Path]::IsPathRooted($r)) { $r } else { Join-Path $Dir $r }
}

function Get-DireccionesLan {
    Get-NetIPAddress -AddressFamily IPv4 -ErrorAction SilentlyContinue |
        Where-Object { $_.IPAddress -notmatch '^(127\.|169\.254\.)' -and $_.PrefixOrigin -ne 'WellKnown' } |
        ForEach-Object { $_.IPAddress }
}

function Pausa { Read-Host 'Presione Enter para cerrar' | Out-Null }
