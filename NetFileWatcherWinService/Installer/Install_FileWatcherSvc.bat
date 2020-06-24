@ECHO OFF

rem through topshelf tool for core.net
echo Installing NBSRouter Service...
echo ---------------------------------------------------
echo "%~dp0..\NetFileWatcherWinService.exe"
echo ---------------------------------------------------
 "%~dp0..\NetFileWatcherWinService.exe" install
  sc start NBSRouter
echo ---------------------------------------------------
echo NBSRouter Service Successfully Installed...
pause