@echo off
echo PUSH PACKAGES TO NUGET
prompt
set nu=C:\Exe\nuget.exe
set src=-Source https://api.nuget.org/v3/index.json

%nu% push .\Cadmus.Lexicography.Parts\bin\Debug\*.nupkg %src%
%nu% push .\Cadmus.Seed.Lexicography.Parts\bin\Debug\*.nupkg %src%
%nu% push .\Cadmus.Lexicography.Services\bin\Debug\*.nupkg %src%
echo COMPLETED
echo on
