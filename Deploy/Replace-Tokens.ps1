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
            az login --service-principal --username $ClientId --password $ClientSecret --tenant $TenantId
        }
        else {
            $username = Read-Host -Prompt 'Please enter your Azure Username'
            az login --username $username
        }

        if (!([string]::IsNullOrWhiteSpace($SubscriptionId))) {
            $accounts = az account list
            az account set --subscription $SubscriptionId
        }

        $script:secrets = az keyvault secret list --vault-name $VaultName
        [psobject[]]$script:kvSecrets
        try {
            $kvSecrets = $secrets | ConvertFrom-Json
        }
        catch [System.Exception] {
            Write-Error -Message "Conversion from JSON failed"
            ThrowError
        }

        $script:jsonFiles = Get-ChildItem -File -Path ./ -Filter '*.json'
    }
        
    process {
        $pfxFiles = $kvSecrets | Where-Object -FilterScript { $_.contentType -like 'application/x-pkcs12' } | Select-Object
        $pfxFiles.ForEach( {
                $prefix = "https://$($VaultName).vault.azure.net/secrets/".Length
                $name = $_.id.Substring($prefix)
                az keyvault secret download --vault-name $VaultName --file "./$($name).pfx" --name $name 
            })

        $script:tokenExp = "${+[a-z]+\W*[a-z]*}+"
        $jsonFiles.ForEach( {
                $content = Get-Content $_.Name
                $x = 0
                foreach ($s in $content) {
                    if ($s -ne $null) {
                        $r = $s
                        if ($s -match '${+[a-z]+\W*[a-z]*}+') {
                            $token = $Matches.Values
                            $token = $token.TrimStart(2).TrimEnd(2)
                            $secretValue = $(az keyvault secret show --name $token --vault-name $VaultName) | ConvertFrom-Json | Select-Object -ExpandProperty value

                            $r.Replace($Matches.Values, $secretValue)
                        }

                        if (!$r.Equals($s)) {
                            $content.Item($x) = $r
                            Out-File -FilePath ./$r -InputObject $content -Force
                        }
                    }

                    $x++ 
                }
            })
    }
        
    end {
    }
}