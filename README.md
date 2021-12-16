# Bat2Exe
Windows user interface for converting your batch files into executables.                                                                                                                                                                                                                                                                   

The latest release has all the dependencies you need, including the Bat2Exe executable.                                                                          
Latest release can be found and downloaded [here](https://github.com/dehoisted/Bat2Exe/releases/tag/v2.0).                                                                                                                                                   

Current version: **2.1**

## Features
+ EXE Compression
+ Different methods for conversion (2 methods)
+ Batch obfuscation (2 methods)
+ Hide console when EXE opened
+ Select icon for EXE
## Update Log
11/17/2021
```
+ Added batch script encoding option (BOM)
+ Improved update checking
+ Small UI Changes
```

## Images
When first opening:

![image](https://user-images.githubusercontent.com/75084509/142293194-36499183-9e92-4f69-b0e3-cfbbfc537766.png)




When selecting batch file & some options then pressing "Compile File":

![image](https://user-images.githubusercontent.com/75084509/133697306-d06b5a82-0c6b-4f0c-93a4-baf44cfe934c.png)

## Build
First, download the ZIP file of all code on this repository [here](https://github.com/dehoisted/Bat2Exe/archive/refs/heads/main.zip).
### Dependencies
All Bat2Exe needs is .NET Framework 4, Winforms desktop development SDK, and GunaUI2.

Guna is used for most of the user interface.                                                                                     
Bytepress is used for when you want to compress your generated EXE file.
+ Guna: https://www.nuget.org/packages/Guna.UI2.WinForms/                                                               
+ Bytepress: https://github.com/roachadam/bytepress/releases/tag/v1.0.0.2

## Issues
If you have any problems or suggestions for Bat2Exe then make an issue [here](https://github.com/dehoisted/Bat2Exe/issues).
