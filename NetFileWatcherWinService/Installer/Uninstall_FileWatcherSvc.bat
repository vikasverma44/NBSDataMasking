@ECHO OFF

echo Uninstalling NBSRouter Service...
echo ---------------------------------------------------
echo "%~dp0..\NetFileWatcherWinService.exe"
echo ---------------------------------------------------
 "%~dp0..\NetFileWatcherWinService.exe" uninstall
echo ---------------------------------------------------
rem echo DPMSRouter Service Successfully Uninstalled...
pause