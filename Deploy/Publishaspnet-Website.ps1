#Requires -Version 3.0
 
param($websiteNames, $packOutput, $slotName)
 
$VerbosePreference = "continue"
$ErrorActionPreference = "continue"
     
Write-Verbose "Published requested of the following website(s): $websiteNames"
     
$websiteNames.split(',') | % {
    $websiteName = $_
     
    Write-Verbose "Restarting Azure Websites $websiteName to ensure no locks"
    Restart-AzureWebsite -Name $websiteName
     
    Write-Verbose "Starting publish of $websiteName"
    $website = if ([string]::IsNullOrWhiteSpace($slotName)) {  Get-AzureWebsite -Name $websiteName } else {  Get-AzureWebsite -Name $websiteName -Slot $slotName }
     
    # If we have an array, we most likely have additional slots on this website. Throw an exception and leave.
    if($website -is [System.Object[]]) {
        throw [System.Exception] "Multiple websites returned for $websiteName; please specify a slot"
    }
     
    # Grab SCM url to use with MSDeploy; there should only be one
    $msdeployurl = $website.EnabledHostNames | Where-Object {$_ -like "*.scm.*"}
     
    if($msdeployurl -is [System.Object[]]) {
        throw [System.Exception] "Multiple SCM urls returned for $websiteName; consult Kudu/Azure portal to clarify."
    }
     
    $publishProperties = @{'WebPublishMethod'='MSDeploy';
                            'MSDeployServiceUrl'=$msdeployurl;
                            'DeployIisAppPath'=$website.Name;
                            'EnableMSDeployAppOffline'=$true;
                            'MSDeployUseChecksum'=$true;}
 
    Write-Verbose "Using the following publish properties (excluding username and password):"           
    Write-Verbose ($publishProperties | Format-List | Out-String)
     
    $publishProperties.Add('Username', $website.PublishingUsername)
    $publishProperties.Add('Password', $website.PublishingPassword)
     
    $publishScript = "${env:ProgramFiles(x86)}\Microsoft Visual Studio 14.0\Common7\IDE\Extensions\Microsoft\Web Tools\Publish\Scripts\default-publish.ps1"
         
    Write-Verbose "Running publish script $publishScript"
     
    . $publishScript -publishProperties $publishProperties -packOutput $packOutput
     
    Write-Verbose "Finished publish of $websiteName"
 }
  
 Write-Verbose "Finished requested publish of the following website(s): $websiteNames"