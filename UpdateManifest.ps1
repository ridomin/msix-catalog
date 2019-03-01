param ([Parameter(Mandatory = $true)][string] $suffix, [Parameter(Mandatory = $false)][string] $publisher)


function UpdateVersion($path)
{
	Write-Host "Updating manifest with suffix $suffix"
	[xml]$manifest= get-content $path
	
	# Add suffix
	$manifest.Package.Identity.Name += $suffix
	#$manifest.Package.Properties.DisplayName += " " + $suffix
    $manifest.Package.Applications.Application.VisualElements.DisplayName += " " + $suffix	

	if ($publisher.Length>0){
		$manifest.Package.Identity.Publisher = $publisher
		$manifest.Package.Properties.PublisherDisplayName = $publisher
	}
	
	$manifest.Save($path)
}

Get-ChildItem $PSScriptRoot -Include *.appxmanifest -Recurse | % { UpdateVersion $_.FullName }
