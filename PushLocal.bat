@echo off
echo PRESS ANY KEY TO INSTALL TO LOCAL NUGET FEED
echo Remember to generate the up-to-date package.
c:\exe\nuget add .\Cadmus.Geo.Parts\bin\Debug\Cadmus.Geo.Parts.4.0.6.nupkg -source C:\Projects\_NuGet
c:\exe\nuget add .\Cadmus.Seed.Geo.Parts\bin\Debug\Cadmus.Seed.Geo.Parts.4.0.6.nupkg -source C:\Projects\_NuGet
pause
