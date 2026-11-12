@echo off
rem Instancia de desarrollo (ver dev.ps1). Opciones:  dev.bat -Lan  para probar desde el celular.
cd /d "%~dp0"
powershell -NoProfile -ExecutionPolicy Bypass -File "%~dp0dev.ps1" %*
