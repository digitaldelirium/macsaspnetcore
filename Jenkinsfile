node {
    properties([
        parameters([

        ])
    ])

    stage('Prepare Environment') {
        deleteDir()
        git 'git@github.com:digitaldelirium/macsaspnetcore.git'
        sh'''
            sudo snap install powershell --classic
        '''
    }

    stage('Replace Tokens'){
        // withAzureKeyvault(
        //     applicationIDOverride: '', 
        //     applicationSecretOverride: '', 
        //     azureKeyVaultSecrets: [
        //         [envVariable: 'ADMIN_PW', name: 'AppSettings--AdminPassword', secretType: 'Secret', version: ''], 
        //         [envVariable: 'SENDGRID_API_KEY', name: 'AppSettings--SendGridAPIKey', secretType: 'Secret', version: ''], 
        //         [envVariable: 'FB_APP_SECRET', name: 'FacebookAppSecret', secretType: 'Secret', version: ''], 
        //         [envVariable: 'CLIENTPFX', name: 'ClientPFX', secretType: 'Certificate', version: ''], 
        //         [envVariable: 'ACTIVITYDB_CONNSTR', name: 'Data--ActivityDb', secretType: 'Secret', version: ''], 
        //         [envVariable: 'APPDB_CONNSTR', name: 'Data--ApplicationDb', secretType: 'Secret', version: ''], 
        //         [envVariable: 'CUSTOMERDB_CONNSTR', name: 'Data--CustomerDb', secretType: 'Secret', version: ''], 
        //         [envVariable: 'RESERVATIONDB_CONNSTR', name: 'Data--ReservationDb', secretType: 'Secret', version: ''], 
        //         [envVariable: 'CLIENT_SECRET', name: 'client-secret', secretType: 'Secret', version: '']
        //         ], 
        //     credentialIDOverride: 'JenkinsWorker', 
        //     keyVaultURLOverride: 'https://macscampvault.vault.azure.net/') {

        // }
        withCredentials([azureServicePrincipal('JenkinsWorker')]) {
            sh'''
                pwsh Deploy/Replace-Tokens.ps1 -ClientId $AZURE_CLIENT_ID -ClientSecret $AZURE_CLIENT_SECRET -TenantId $AZURE_TENANT_ID -VaultName macscampvault -SubscriptionId $AZURE_SUBSCRIPTIONI_ID -SPN
            '''
        }

        sh'''
            sed s/#{BuildConfiguration}#/Release/g Dockerfile
        '''
    }
}