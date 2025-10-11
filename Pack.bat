@echo off
echo BUILD Cadmus Lexicography Packages
del .\Cadmus.Lexicography.Parts\bin\Debug\*.snupkg
del .\Cadmus.Lexicography.Parts\bin\Debug\*.nupkg

del .\Cadmus.Seed.Lexicography.Parts\bin\Debug\*.snupkg
del .\Cadmus.Seed.Lexicography.Parts\bin\Debug\*.nupkg

del .\Cadmus.Lexicography.Services\bin\Debug\*.snupkg
del .\Cadmus.Lexicography.Services\bin\Debug\*.nupkg

cd .\Cadmus.Lexicography.Parts
dotnet pack -c Debug -p:IncludeSymbols=true -p:SymbolPackageFormat=snupkg
cd..

cd .\Cadmus.Seed.Lexicography.Parts
dotnet pack -c Debug -p:IncludeSymbols=true -p:SymbolPackageFormat=snupkg
cd..

cd .\Cadmus.Lexicography.Services
dotnet pack -c Debug -p:IncludeSymbols=true -p:SymbolPackageFormat=snupkg
cd..

pause
