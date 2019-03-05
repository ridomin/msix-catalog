# MSIX Catalog

*WPF application to inspect MSIX/APPX packages deployed in your machine.*

The purpose of this app is to explore different "Desktop Modernization" technologies available to .NET developers: porting from .NET Framework to .NET Core 3, using Win10 APIs, and different deployment techniques.

||.NET Framework 4.7 |.NET Core 3|
|-|-|-|
|Store Release|![rel badge](https://rido.vsrm.visualstudio.com/_apis/public/Release/badge/3946e8eb-731c-4bd3-a330-f374e4f8a046/3/3) [Install](https://bit.ly/msix-catalog)|![rel badge](https://rido.vsrm.visualstudio.com/_apis/public/Release/badge/3946e8eb-731c-4bd3-a330-f374e4f8a046/5/5) [Install](https://bit.ly/msix-catalog-core)|
|Sideload|![](https://rido.vsrm.visualstudio.com/_apis/public/Release/badge/3946e8eb-731c-4bd3-a330-f374e4f8a046/1/1) [Install](http://msix-catalog.azurewebsites.net/AppxPackages) |![](https://rido.vsrm.visualstudio.com/_apis/public/Release/badge/3946e8eb-731c-4bd3-a330-f374e4f8a046/4/4) [Install](http://msix-catalog.azurewebsites.net/netcore3)|

> All build definitions are available on [Azure Dev Ops Pipelines](https://rido.visualstudio.com/msix-catalog)

## .NET Flavors

The source code is shared between .NET Framework and .NET Core using different project files in the same folder.

## Build Requirements

- .NET Framework 4.6.1
- .NET Core 3.0 Preview (latest)
- Visual Studio 2019 Preview (with Desktop and UWP workloads)
- Windows SDK 1803 (17134 or greater) 

## Experimental 

In addition to MSIX packages, you can get the tool using one of th next alternative installation options:

- [MSIX Catalog from NuGet as global tool](https://www.nuget.org/packages/dotnet-msix-catalog) `dotnet tool install -g dotnet-msix-catalog`
- [MSIX CLickOnce](http://msix-catalog.azurewebsites.net/clickonce/publish.htm)
- [Classic MSI](#)


## Screenshot

![MSIX Catalog screenshot](media/screenshot.PNG)