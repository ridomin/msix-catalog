param ([Parameter(Mandatory = $true)][string] $suffix)


function UpdateVersion($path)
{
	Write-Host "Updating manifest with suffix $suffix"
	[xml]$manifest= get-content $path
	
	# Add suffix
	$manifest.Package.Identity.Name += $suffix
	#$manifest.Package.Properties.DisplayName += " " + $suffix
    $manifest.Package.Applications.Application.VisualElements.DisplayName += " " + $suffix	
	$manifest.Save($path)
}

Get-ChildItem $PSScriptRoot -Include *.appxmanifest -Recurse | % { UpdateVersion $_.FullName }
