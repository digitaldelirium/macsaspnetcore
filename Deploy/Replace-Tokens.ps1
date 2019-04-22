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
            Set-AzContext -Subscription $SubscriptionId -Tenant $TenantId
        }

        $script:kvSecrets = Get-AzKeyVaultSecret -VaultName $VaultName

        $script:jsonFiles = Get-ChildItem -File -Path ./ -Filter '*.json'
        $script:csFiles = Get-ChildItem -File -Path ./ -Filter '*.cs'

        $script:files = @($script:jsonFiles + $script:csFiles)
    }
        
    process {
        $pfxFiles = $kvSecrets | Where-Object -FilterScript { $_.contentType -like 'application/x-pkcs12' } | Select-Object
        $pfxFiles.ForEach({
                $prefix = "https://$($VaultName).vault.azure.net/secrets/"
                $name = $_.id.Substring($prefix.ToString().Length + 4)
                Write-Host "$name"                
                $pfxSecret = Get-AzKeyVaultSecret -VaultName $vaultName -Name $name
                $pfxUnprotectedBytes = [Convert]::FromBase64String($pfxSecret.SecretValueText)
                $pfx = New-Object Security.Cryptography.X509Certificates.X509Certificate2
                $pfx.Import($pfxUnprotectedBytes, $null, [Security.Cryptography.X509Certificates.X509KeyStorageFlags]::Exportable)
                $pfx.PrivateKey.ExportParameters($true)
                [IO.File]::WriteAllBytes("$name.pfx", $pfxUnprotectedBytes)
            })

        $script:tokenExp = "${+[a-z]+\W*[a-z]*}+"
        $secrets = @{}

        $kvSecrets.Name.ForEach({
            $secret = $_
            Write-Host "Looking for #{$secret}#"
            $files.ForEach({
                $file = $_
                $content = Get-Content $file
                $matches = 0
                
                if($content -match "#{$secret}#"){
                    $matches++
                    Write-Host "Replacing $file Values with $secret Secret Values"
                    $content.replace("#{$secret}#", $(Get-AzKeyVaultSecret -Name $secret -VaultName $VaultName).SecretValueText) | Set-Content $file
                }

                if($matches -eq 0){
                    Write-Host "No matches found for $file"
                }
            })    
        })

    }
        
    end {
    }
}