@echo off
echo PRESS ANY KEY TO INSTALL TO LOCAL NUGET FEED
echo Remember to generate the up-to-date package.
c:\exe\nuget add .\Cadmus.Lexicography.Parts\bin\Release\Cadmus.Lexicography.Parts.3.0.0.nupkg -source C:\Projects\_NuGet
c:\exe\nuget add .\Cadmus.Seed.Lexicography.Parts\bin\Release\Cadmus.Seed.Lexicography.Parts.3.0.0.nupkg -source C:\Projects\_NuGet
c:\exe\nuget add .\Cadmus.Lexicography.Services\bin\Release\Cadmus.Lexicography.Services.3.0.0.nupkg -source C:\Projects\_NuGet
pause
