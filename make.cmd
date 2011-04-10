@ECHO OFF

IF EXIST obj RMDIR obj /S /Q
MKDIR obj

"%windir%\Microsoft.NET\Framework\v4.0.30319\csc.exe" /out:obj\DevPad.exe /target:winexe /recurse:src\*.cs /nologo

IF ERRORLEVEL 1 GOTO :EoF

IF EXIST bin RMDIR bin /S /Q
MKDIR bin

COPY obj\*.* bin >> NUL

@ECHO ON
