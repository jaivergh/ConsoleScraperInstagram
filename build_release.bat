@ECHO off
ECHO Build release project
dotnet publish -r win-x64 -c Release /p:PublishSingleFile=true /p:PublishTrimmed=true
ECHO DONE!! :D