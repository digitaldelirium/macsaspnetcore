#! /usr/bin/pwsh
param (
    [Parameter(Mandatory = $true, HelpMessage = 'Client ID for SPN', ParameterSetName = 'SPN')]
    [ValidateNotNullOrEmpty()]
    [string]
    $ClientId,

    [Parameter(Mandatory = $true, HelpMessage = 'Client Secret for SPN', ParameterSetName = 'SPN')]
    [ValidateNotNullOrEmpty()]
    [string]
    $ClientSecret,

    [Parameter(Mandatory = $true, HelpMessage = 'AAD Tenant', ParameterSetName = 'SPN')]
    [ValidateNotNullOrEmpty()]
    [string]
    $TenantId,

    [Parameter(Mandatory = $true, HelpMessage = "Key Vault Name", ParameterSetName = 'Default')]
    [Parameter(Mandatory = $true, HelpMessage = "Key Vault Name", ParameterSetName = 'SPN')]        
    [ValidateNotNullOrEmpty()]
    [string]
    $VaultName,

    [Parameter(Mandatory = $false, HelpMessage = "full or partial subscription name, if not default")]
    [string]
    $SubscriptionId,

    # Parameter help description
    [Parameter(Mandatory, ParameterSetName = 'SPN')]
    [switch]
    $SPN
)
function Replace-Tokens {
    [CmdletBinding(DefaultParameterSetName = 'Default')]
    param (
        [Parameter(Mandatory = $true, HelpMessage = 'Client ID for SPN', ParameterSetName = 'SPN')]
        [ValidateNotNullOrEmpty()]
        [string]
        $ClientId,

        [Parameter(Mandatory = $true, HelpMessage = 'Client Secret for SPN', ParameterSetName = 'SPN')]
        [ValidateNotNullOrEmpty()]
        [string]
        $ClientSecret,

        [Parameter(Mandatory = $true, HelpMessage = 'AAD Tenant', ParameterSetName = 'SPN')]
        [ValidateNotNullOrEmpty()]
        [string]
        $TenantId,

        [Parameter(Mandatory = $true, HelpMessage = "Key Vault Name", ParameterSetName = 'Default')]
        [Parameter(Mandatory = $true, HelpMessage = "Key Vault Name", ParameterSetName = 'SPN')]        
        [ValidateNotNullOrEmpty()]
        [string]
        $VaultName,

        [Parameter(Mandatory = $false, HelpMessage = "full or partial subscription name, if not default")]
        [string]
        $SubscriptionId,

        # Parameter help description
        [Parameter(Mandatory, ParameterSetName = 'SPN')]
        [switch]
        $SPN
    )
        
    begin {

        if ($SPN) {
            [PSCredential]$credential = [PSCredential]::new($ClientId, $($ClientSecret | ConvertTo-SecureString -AsPlainText -Force))
            Connect-AzAccount -ServicePrincipal -Credential $credential -Tenant $TenantId
        }
        else {
            Connect-AzAccount
        }

        if (!([string]::IsNullOrWhiteSpace($SubscriptionId))) {
            $accounts = Get-AzSubscription
            Set-AzContext -Subscription $SubscriptionId -Tenant $TenantId
        }

        $global:kvSecrets = Get-AzKeyVaultSecret -VaultName $VaultName

        $script:jsonFiles = Get-ChildItem -File -Path ./ -Filter '*.json'
        $script:csFiles = Get-ChildItem -File -Path ./ -Recurse -Filter '*.cs'

        $global:files = @($script:jsonFiles + $script:csFiles)
    }
        
    process {
        $pfxFiles = $kvSecrets | Where-Object -FilterScript { $_.contentType -like 'application/x-pkcs12' } | Select-Object
        $pfxFiles.ForEach( {
                $prefix = "https://$($VaultName).vault.azure.net/secrets/".Length
                $name = $_.id.Substring($prefix)
                az keyvault secret download --vault-name $VaultName --file "./$($name).pfx" --name $name 
            })

        $script:tokenExp = "${+[a-z]+\W*[a-z]*}+"
        $secrets = @{}

        $files.ForEach( {
                $file = $_
                $content = Get-Content $_
                $matches = 0
                $kvSecrets.Name.ForEach({
                    Write-Host "Looking for #{$_}#"
                    if($content -match "#{$_}#"){
                        $matches++
                        Write-Host "Replacing $Matches.Values with $_ Secret Values in $file"
                        $content.replace("#{$_}#", $(Get-AzKeyVaultSecret -Name $_ -VaultName $VaultName).SecretValueText) | Set-Content $file
                    }
                    
                })

                if($matches -eq 0){
                    Write-Host "No matches found for $file"
                }
                # foreach ($s in $content) {
                #     if ($s -ne $null) {
                #         if ($s -match '#{+[a-z]+\W*[a-z]*}#+') {
                #             Write-Host $Matches.Values
                #             $token = $Matches.Values
                #             $secretValue = Get-AzKeyVaultSecret -Name $token.TrimStart('#','{').TrimEnd('}','#') -VaultName $VaultName

                #             Write-Host "Replacing $token with $secretValue.Value for $_"
                #             $content.Replace($token, $secretValue.Value) | Set-Content $_.PSPath
                #         } else {
                #             Write-Host "No matches found for $s in $_"
                #         }
                #     }
                # }
            })
    }
        
    end {
    }
}

if ($SPN) {
    Replace-Tokens -ClientId $ClientId -ClientSecret $ClientSecret -SPN -TenantId $TenantId -SubscriptionId $SubscriptionId -VaultName $VaultName
} else {
    Replace-Tokens -VaultName $VaultName
}