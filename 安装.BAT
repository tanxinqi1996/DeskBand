%1 mshta vbscript:createobject("shell.application").shellexecute("cmd.exe","/c %~s0 ::","","runas",1)(window.close)&&exit
@echo off
cd /d %~dp0
%SystemRoot%\Microsoft.NET\Framework64\v4.0.30319\regasm.exe /nologo /codebase DeskBand.dll
@echo 在任务栏-右键-工具栏-任务栏助手启动此功能
timeout 5 > nul
