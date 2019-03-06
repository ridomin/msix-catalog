# MSIX Catalog

*WPF application to inspect MSIX/APPX packages deployed in your machine.*

The purpose of this app is to explore different "Desktop Modernization" technologies available to .NET developers: porting from .NET Framework to .NET Core 3, using Win10 APIs, and different deployment techniques.

### Sideload packages are signed with a developer certificate

> Install [Cert Central](https://certcentral.x509.online/) Client (aka ccc) with `dotnet tool install -g dotnet-ccc` (requires .NET Core 2.1 SDK)

> Search certs from `ccc certs -u ridomin` and select the cert `CN=FD784322-3D44-4DA4-9F73-0E23D5DF0893` with thumbprint `C5A4ABA7655B2B11C41FAAE43A9D4FC2FA438E6F`


||.NET Framework 4.7 |.NET Core 3|
|-|-|-|
|Store Release|[![rel badge](https://rido.vsrm.visualstudio.com/_apis/public/Release/badge/3946e8eb-731c-4bd3-a330-f374e4f8a046/3/3)](https://bit.ly/msix-catalog)|[![rel badge](https://rido.vsrm.visualstudio.com/_apis/public/Release/badge/3946e8eb-731c-4bd3-a330-f374e4f8a046/5/5)](https://bit.ly/msix-catalog-core)|
|Sideload|[![Rel Badge](https://rido.vsrm.visualstudio.com/_apis/public/Release/badge/3946e8eb-731c-4bd3-a330-f374e4f8a046/1/1)](http://msix-catalog.azurewebsites.net/AppxPackages) |[![](https://rido.vsrm.visualstudio.com/_apis/public/Release/badge/3946e8eb-731c-4bd3-a330-f374e4f8a046/4/4)](http://msix-catalog.azurewebsites.net/netcore3)|
|AppCenter|[![Build status](https://build.appcenter.ms/v0.1/apps/a92bf008-9e06-4c8c-8a30-d7f6099c3242/branches/dev/badge)](https://install.appcenter.ms/users/rido/apps/msix-catalog)||

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