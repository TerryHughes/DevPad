@ECHO OFF

IF EXIST bin RMDIR bin /S /Q
MKDIR bin

"%windir%\Microsoft.NET\Framework\v4.0.30319\csc.exe" /out:bin\DevPad.exe /target:winexe /recurse:src\*.cs /nologo

@ECHO ON
