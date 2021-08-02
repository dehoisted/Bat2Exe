rem example.bat
: Example batch file for converting it into an exe.
: If you get any compilation errors, make a new issue at https://github.com/dehoisted/Bat2Exe/issues
: Huge batch files may fail.
: Note: comments can be viewed if the exe is decompiled.

@echo off
cls
title Bat2Exe Example
echo Hello :catkiss:
set /p text=Enter text: 
echo Text entered = %text%
pause
start https://github.com/dehoisted/Bat2Exe
echo Brought you to Bat2Exe github page.
pause
echo Bye
