@ECHO OFF

IF EXIST obj RMDIR obj /S /Q
MKDIR obj

REM debug
"%windir%\Microsoft.NET\Framework\v4.0.30319\csc.exe" /out:obj\DevPad.exe /target:winexe /platform:anycpu32bitpreferred /recurse:src\*.cs /debug+ /debug:full /optimize- /warn:4 /define:DEBUG;TRACE /nologo

REM release
REM "%windir%\Microsoft.NET\Framework\v4.0.30319\csc.exe" /out:obj\DevPad.exe /target:winexe /platform:anycpu32bitpreferred /recurse:src\*.cs /debug:pdbonly /optimize+ /warn:4 /define:TRACE /nologo


@ECHO ON
