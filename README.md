# MSIX Catalog

*WPF application to inspect MSIX/APPX packages deployed in your machine.*

The purpose of this app is to explore different "Desktop Modernization" technologies available to .NET developers: porting from .NET Framework to .NET Core 3, using Win10 APIs, and different deployment techniques.

## Channels

||.NET Framework 4.7 |.NET Core 3|
|-|-|-|
|Store Release|[![rel badge](https://rido.vsrm.visualstudio.com/_apis/public/Release/badge/3946e8eb-731c-4bd3-a330-f374e4f8a046/6/9)](https://bit.ly/msix-catalog)|[![rel badge](https://rido.vsrm.visualstudio.com/_apis/public/Release/badge/3946e8eb-731c-4bd3-a330-f374e4f8a046/6/11)](https://bit.ly/msix-catalog-core)|
|Sideload from WebApp|[![Rel Badge](https://rido.vsrm.visualstudio.com/_apis/public/Release/badge/3946e8eb-731c-4bd3-a330-f374e4f8a046/6/6)](http://msix-catalog.azurewebsites.net/FX) |[![](https://rido.vsrm.visualstudio.com/_apis/public/Release/badge/3946e8eb-731c-4bd3-a330-f374e4f8a046/6/10)](http://msix-catalog.azurewebsites.net/NC3)|


### Sideload packages are signed with a developer certificate

> Install [X509Online CLI](https://x509online.azurewebsites.net) Client (aka ccc) with `dotnet tool install -g dotnet-ccc` (requires .NET Core 2.1 SDK)

Sideloaded packages are signed with a different certificates for AppCenter and WebApps, to install these certs you must trust the signer by running the next command from an elevated command prompt: 

- `ccc trust -u ridomin -t 728511CC02E6A80B45ABC0CC862FEF1BFD9617D7`

>The ccc trust command adds the certificate to your LocalMachine\TrustedPeople certificate store

The certificate is also available as gists:
- [RidoTheDev](https://gist.githubusercontent.com/ridomin/85ffb2f933c68683b87bbfb27ca114cf/raw/77ac9ac02dc553e4c7810c7ddb1cfa4c0ba6e6db/RidoTheDev.cer)


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
