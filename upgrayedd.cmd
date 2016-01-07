@ECHO OFF

IF EXIST bin RMDIR bin /S /Q
MKDIR bin

COPY obj\*.* bin >> NUL

START bin\DevPad.exe "%cd%"
REM FORFILES /P src /S /C "cmd /c echo @path" /D 0

@ECHO ON
