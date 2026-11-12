@echo off
rem Genera dist\Pacientes (carpeta lista para copiar) y dist\Pacientes.zip.
rem OJO: esto NO instala nada. Para instalar, ejecute instalar.bat desde la carpeta copiada.
cd /d "%~dp0"
if exist dist\Pacientes rmdir /s /q dist\Pacientes
dotnet build Pacientes.Web\Pacientes.Web.csproj -c Release -o dist\Pacientes -p:DebugType=none -p:DebugSymbols=false
if errorlevel 1 (
  echo.
  echo ERROR al compilar.
  pause
  exit /b 1
)
del /q dist\Pacientes\*.xml 2>nul
del /q dist\Pacientes\_frame.html 2>nul
if exist dist\Pacientes.zip del /q dist\Pacientes.zip
powershell -NoProfile -Command "Compress-Archive -Path 'dist\Pacientes' -DestinationPath 'dist\Pacientes.zip'"
echo.
echo ============================================================
echo  Paquete listo en:  %~dp0dist\Pacientes
echo.
echo  PARA INSTALAR (esto todavia NO instalo nada):
echo    1. Copie la carpeta  dist\Pacientes  a  C:\Pacientes
echo    2. Dentro de C:\Pacientes, doble clic en  instalar.bat
echo ============================================================
echo.
pause
