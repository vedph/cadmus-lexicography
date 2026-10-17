@echo off
echo BUILD Cadmus Lexicography Packages
del .\Cadmus.Lexicography.Parts\bin\Release\*.snupkg
del .\Cadmus.Lexicography.Parts\bin\Release\*.nupkg

del .\Cadmus.Seed.Lexicography.Parts\bin\Release\*.snupkg
del .\Cadmus.Seed.Lexicography.Parts\bin\Release\*.nupkg

del .\Cadmus.Lexicography.Services\bin\Release\*.snupkg
del .\Cadmus.Lexicography.Services\bin\Release\*.nupkg

cd .\Cadmus.Lexicography.Parts
dotnet pack -c Release -p:IncludeSymbols=true -p:SymbolPackageFormat=snupkg
cd..

cd .\Cadmus.Seed.Lexicography.Parts
dotnet pack -c Release -p:IncludeSymbols=true -p:SymbolPackageFormat=snupkg
cd..

cd .\Cadmus.Lexicography.Services
dotnet pack -c Release -p:IncludeSymbols=true -p:SymbolPackageFormat=snupkg
cd..

pause
