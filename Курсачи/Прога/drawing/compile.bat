@echo off
if not "%1" == "max" start /MAX cmd /c %0 max & exit/b

tasklist /fi "ImageName eq drawing.exe" /fo csv 2>NUL | find /I "drawing.exe">NUL
if "%ERRORLEVEL%"=="0" taskkill /IM drawing.exe
IF EXIST "drawing.exe" (
del "drawing.exe"
)
gcc -I ./headers ./source/* -lgdi32 -w -o "drawing.exe"
IF EXIST "drawing.exe" (
start drawing.exe
) ELSE (
PAUSE
)