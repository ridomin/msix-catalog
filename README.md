# MSIX Catalog

<strong>
<a href="http://msix-catalog.rido.site/">http://msix-catalog.rido.site</a>
<strong>

WPF application that shows all MSIX/APPX packages deployed to your machine.

## .NET Flavors

The source code is shared between .NET Framework and .NET Core using different project files in the same folder.

## Build Requirements

- .NET Framework 4.6.1
- .NET Core 3.0 Preview 1
- Visual Studio 2019 Preview (with Desktop and UWP workloads)
- Windows SDK 1803 (17134)

## CI/CD Pipelines

PRs to `dev` branch are validated with CI Builds
Merges to master are deployed to the next locations

- [MSIX Catalog from the Microsoft Store](http://bit.ly/msix-catalog)
- [MSIX Catalog from Azure Web Site NETFX ](http://msix-catalog.azurewebsites.net/AppxPackages)
- [MSIX Catalog from Azure Web Site NETCORE3 ](http://msix-catalog.azurewebsites.net/netcore3)
- [MSIX Catalog from NuGet as global tool](#) `dotnet tool install -g dotnet-msix-catalog`


