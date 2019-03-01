param (
	[Parameter(Mandatory = $true)][string] $suffix, 
	[string] $publisher)


function UpdateVersion($path)
{
	Write-Host "Updating manifest $path with suffix $suffix and publisher $publisher"
	[xml]$manifest= get-content $path
	
	# Add suffix
	$manifest.Package.Identity.Name += $suffix
	#$manifest.Package.Properties.DisplayName += " " + $suffix
    $manifest.Package.Applications.Application.VisualElements.DisplayName += " " + $suffix	

	if ($publisher) {
		$manifest.Package.Identity.Publisher = $publisher
		$manifest.Package.Properties.PublisherDisplayName = $publisher
	}
	
	$manifest.Save($path)
}

Get-ChildItem $PSScriptRoot -Include *.appxmanifest -Recurse | % { UpdateVersion $_.FullName }
