%1 mshta vbscript:createobject("shell.application").shellexecute("cmd.exe","/c %~s0 ::","","runas",1)(window.close)&&exit
@echo off
cd /d %~dp0
%SystemRoot%\Microsoft.NET\Framework64\v4.0.30319\regasm.exe /unregister DeskBand.dll
@ECHO 开始重启资源管理器
taskkill /f /im explorer.exe
explorer.exe
timeout 5 > nul
