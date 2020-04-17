#! /usr/bin/pwsh
param (
    [Parameter(Mandatory, HelpMessage = "Key Vault Name", ParameterSetName = 'Default')]   
    [ValidateNotNullOrEmpty()]
    [string]
    $VaultName
)
function Replace-Tokens {
    [CmdletBinding(DefaultParameterSetName = 'Default')]
    param (

        [Parameter(Mandatory, HelpMessage = "Key Vault Name", ParameterSetName = 'Default')]    
        [ValidateNotNullOrEmpty()]
        [string]
        $VaultName
    )
        
    begin {

        if (!([string]::IsNullOrWhiteSpace($SubscriptionId))) {
            Set-AzContext -Subscription $SubscriptionId -Tenant $TenantId
        }

        $script:kvSecrets = Get-AzKeyVaultSecret -VaultName $VaultName

        $script:jsonFiles = Get-ChildItem -File -Path ./ -Filter '*.json'
        $script:csFiles = Get-ChildItem -File -Path ./ -Filter '*.cs'

        $script:files = @($script:jsonFiles + $script:csFiles)
    }
        
    process {
        # $pfxFiles = $kvSecrets | Where-Object -FilterScript { $_.contentType -like 'application/x-pkcs12' } | Select-Object
        # $pfxFiles.ForEach{
        #         $prefix = "https://$($VaultName).vault.azure.net/secrets/"
        #         $name = $_.id.Substring($prefix.ToString().Length + 4)
        #         Write-Host "$name"                
        #         $pfxSecret = Get-AzKeyVaultSecret -VaultName $vaultName -Name $name
        #         $pfxUnprotectedBytes = [Convert]::FromBase64String($pfxSecret.SecretValueText)
        #         $pfx = New-Object Security.Cryptography.X509Certificates.X509Certificate2
        #         $pfx.Import($pfxUnprotectedBytes, $null, [Security.Cryptography.X509Certificates.X509KeyStorageFlags]::Exportable)
        #         $pfx.PrivateKey.ExportParameters($true)
        #         [IO.File]::WriteAllBytes("$name.pfx", $pfxUnprotectedBytes)
        #     }

        $kvSecrets.Name.ForEach{
            $secret = $_
            Write-Host "Looking for #{$secret}#"
            $files.ForEach{
                $file = $_
                try
                {
                  $content = Get-Content $file
                }
                catch [Management.Automation.ItemNotFoundException]
                {
                  Write-Debug "Skipping file $file due to bad path"
                }
                
                
                
                $matches = 0
                
                if($content -match "#{$secret}#"){
                    $matches++
                    Write-Debug "Replacing $file Values with $secret Secret Values"
                    $content.replace("#{$secret}#", $(Get-AzKeyVaultSecret -Name $secret -VaultName $VaultName).SecretValueText) | Set-Content $file
                }

                if($matches -eq 0){
                    Write-Debug "No matches found for $file"
                }
            }    
        }

    }
        
    end {
    }
}

Replace-Tokens -VaultName $VaultName