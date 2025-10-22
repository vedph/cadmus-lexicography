@echo off
echo PRESS ANY KEY TO INSTALL TO LOCAL NUGET FEED
echo Remember to generate the up-to-date package.
c:\exe\nuget add .\Cadmus.Lexicography.Parts\bin\Debug\Cadmus.Lexicography.Parts.0.0.1.nupkg -source C:\Projects\_NuGet
c:\exe\nuget add .\Cadmus.Seed.Lexicography.Parts\bin\Debug\Cadmus.Seed.Lexicography.Parts.0.0.2.nupkg -source C:\Projects\_NuGet
c:\exe\nuget add .\Cadmus.Lexicography.Services\bin\Debug\Cadmus.Lexicography.Services.0.0.2.nupkg -source C:\Projects\_NuGet
pause
