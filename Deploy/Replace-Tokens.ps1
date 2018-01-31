function Replace-Tokens {
    [CmdletBinding()]
    param (
        [Parameter(Mandatory=$true, HelpMessage='Client ID for SPN', ParameterSetName='SPN')]
        [ValidateNotNullOrEmpty()]
        [string]
        $ClientId,

        [Parameter(Mandatory=$true,HelpMessage='Client Secret for SPN', ParameterSetName='SPN')]
        [ValidateNotNullOrEmpty()]
        [string]
        $ClientSecret,

        [Parameter(Mandatory=$true,HelpMessage='AAD Tenant', ParameterSetName='SPN')]
        [ValidateNotNullOrEmpty()]
        [string]
        $TenantId,

        [Parameter(Mandatory=$true, HelpMessage="Key Vault Name")]
        [ValidateNotNullOrEmpty()]
        [string]
        $VaultName,

        [Parameter(Mandatory=$false, HelpMessage="full or partial subscription name, if not default")]
        [string]
        $SubscriptionId
    )
    
    begin {
        if ($ClientId -ne $null) {
            $password = ConvertTo-SecureString $ClientSecret -AsPlainText -Force
            az login --service-principal --username $ClientId --password $password --tenant $TenantId
        }
        else {
            az login
        }

        if(![string]::IsNullOrEmpty($SubscriptionId)){
            $accounts = az account list | ConvertFrom-Json
            az account set --subscription $SubscriptionId
        }

        $script:kvSecrets = az keyvault secret list --vault-name $VaultName | ConvertFrom-Json
        $script:jsonFiles = Get-ChildItem -File -Path ./ -Filter '*.json'
    }
    
    process {
        $pfxFiles = $kvSecrets | Where-Object -FilterScript { $_.contentType -like 'application/x-pkcs12'} | Select-Object
        $pfxFiles.ForEach({
            $prefix = "https://$($VaultName).vault.azure.net/secrets/".Length
            $name = $_.id.Substring($prefix)
            az keyvault secret download --vault-name $VaultName --file "./$($name).pfx" --name $name 
        })

        $script:tokenExp = "#{+[a-z]+\W*[a-z]*}#+"
        $jsonFiles.ForEach({
            $content = Get-Content $_.Name
            $x = 0
            foreach ($s in $content) {
                [string]$r = s
                if($s -match '#{+[a-z]+\W*[a-z]*}#+'){
                    $token = $Matches.Values
                    $token = $token.TrimStart(2).TrimEnd(2)
                    $secretValue = $(az keyvault secret show --name $token --vault-name $VaultName) | ConvertFrom-Json | Select-Object -ExpandProperty value

                    $r.Replace($Matches.Values, $secretValue)
                }

                if (!$r.Equals($s)) {
                    $content.Item($x) = $r
                }

                $x++ 
            }
            Out-File -FilePath ./$_.Name -InputObject $content -Force
        })
    }
    
    end {
        $envVariables = Get-ChildItem Env:
        $config = $envVariables.Name("BuildConfiguration")
        # New Content Block Scope
        $content = Get-Content .\Dockerfile
        foreach ($s in $content) {
            [int]$x = 0
            [string]$r = $s
            if($s -match $tokenExp){
                $token = $Matches.Values
                $token = $token.TrimStart(2).TrimEnd(2)
                $r.Replace($Matches.Values, $config)

                if (!$r.Equals($s)){
                    $content.Item($x) = $r
                }

                $x++
            }
            
            Out-File -FilePath ./Dockerfile -InputObject $content -Force
        }
    }
}