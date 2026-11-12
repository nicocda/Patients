@echo off
rem Lo ejecuta la tarea programada "Pacientes Web".
rem Guarda la salida en logs\pacientes.log (cuando pasa de 5 MB la rota a pacientes.old.log).
cd /d "%~dp0"
if not exist "%~dp0logs" mkdir "%~dp0logs"
set "LOG=%~dp0logs\pacientes.log"
set "TAM=0"
if exist "%LOG%" for %%F in ("%LOG%") do set "TAM=%%~zF"
if %TAM% GTR 5000000 move /y "%LOG%" "%~dp0logs\pacientes.old.log" >nul
"%~dp0Pacientes.Web.exe" >> "%LOG%" 2>&1
