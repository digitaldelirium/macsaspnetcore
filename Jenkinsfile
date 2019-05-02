pipeline{
    agent{
        label "linux"
    }
    parameters {
        choice(name: 'BuildConfiguration', description: 'The Build Configuration to use', choices: ['Release','Debug'])
        choice(name: 'GitBranch', description: 'The branch to build from Git', choices: ['develop','master','netcore_lts'])
        choice(name: 'Environment', description: 'The environment to run in', choices: ['Development', 'Staging', 'Production'])
        string(name: 'VaultName', description: 'The name of the Azure Key Vault hosting secrets', defaultValue: 'macscampvault')
    }
    environment {
        BUILD_CONFIGURATION = "${BuildConfiguration}"
        ENVIRONNENT = "${Environment}"
        BRANCH = "${GitBranch}"
    }
    stages{
        stage("Setup Environment"){
            steps{
                echo "========Setting Up Environment for Deployment========"
                deleteDir()
                git branch: '$BRANCH', credentialsId: 'github-creds', url: 'https://github.com/digitaldelirium/macsaspnetcore.git'
                
                withCredentials([azureServicePrincipal('JenkinsWorker')]) {
                    echo "====++++Replacing Tokens in Files++++===="
                    sh'''
                        /snap/bin/pwsh || sudo snap install powershell --classic
                        /snap/bin/pwsh Deploy/Replace-Tokens.ps1 -ClientId ${AZURE_CLIENT_ID} -ClientSecret ${AZURE_CLIENT_SECRET} -SPN -TenantId ${AZURE_TENANT_ID} -SubscriptionId ${AZURE_SUBSCRIPTION_ID} -VaultName ${VaultName}
                    '''
                }
            }
            post{
                success{
                    echo "========Environment setup executed successfully========"
                }
                failure{
                    echo "========Environment setup execution failed========"
                }
            }
        }
        stage("Build Docker Container"){
            steps {
                echo "====++++Setup .NET Build environment++++===="
                sh'''
                    sed -i 's/#{BuildConfiguration}#/$BuildConfiguration/g' Dockerfile
                    sed -i 's/#{Environment}#/$ENVIRONMENT/g' Dockerfile
                '''

                echo "====++++Build Docker Container++++===="
                script {
                    switch($ENVIRONMENT) {
                        case "Development":
                            sh"""
                                docker build --rm --compress Dev.dockerfile -t macscampingapp:development -t macscampingapp:\$BUILD_NUMBER -t macscampingarea.azurecr.io/macscampingapp:\$BUILD_NUMBER macscampingarea.azurecr.io/macscampingapp:development
                            """
                        break
                        case "Staging":
                            sh"""
                                docker build --rm --compress . -t macscampingapp:staging -t macscampingapp:\$BUILD_NUMBER -t macscampingarea.azurecr.io/macscampingapp:\$BUILD_NUMBER macscampingarea.azurecr.io/macscampingapp:staging
                            """                        
                        break
                        case "Production":
                            sh"""
                                docker build --rm --compress . -t macscampingapp:latest -t macscampingapp:\$BUILD_NUMBER -t macscampingarea.azurecr.io/macscampingapp:\$BUILD_NUMBER macscampingarea.azurecr.io/macscampingapp:prod
                            """
                        break
                    }
                }
            }
        }
        stage("Push container to registry"){
            steps{
                script {
                    if($ENVIRONMENT != "Development"){
                        echo "====++++Push Container to ACR++++===="
                        withCredentials([azureServicePrincipal('JenkinsWorker')]) {                
                            sh'''
                                az login -u $AZURE_CLIENT_ID --password $AZURE_CLIENT_SECRET --service-principal --tenant $AZURE_TENANT_ID
                                REGISTRY_NAME="macscampingarea"
                                az acr login --name $REGISTRY_NAME
                                docker push $REGISTRY_NAME.azurecr.io/macscampingapp:$BUILD_NUMBER
                            '''
                        }
                    }
                    else {
                        echo "====++++Skipping due to Development Environment++++===="
                    }
                }
            }
        }
        stage("Run Container"){
            steps{
                echo "====++++Connect to MacsVM via SSH++++===="
                script {
                    switch ($ENVIRONMENT){
                        case "Development":
                            sh'''
                                docker run -dit --name macsdev macscampingapp:$BUILD_NUMBER
                            '''
                        break
                        case "Staging":
                            sshagent(['macscampingarea']) {                        
                                sh'''
                                    ssh -A macs@macsvm.macscampingarea.com
                                    az acr login --name macscampingarea
                                    docker pull macscampingarea.azurecr.io/macscampingapp:$BUILD_NUMBER                                
                                    docker run -dit --name macsstaging macscampingarea.azurecr.io/macscampingapp:$BUILD_NUMBER
                                '''
                            }
                        break
                        case "Production":
                            sshagent(['macscampingarea']) {                        
                                sh'''
                                    ssh -A macs@macsvm.macscampingarea.com
                                    az acr login --name macscampingarea
                                    docker pull macscampingarea.azurecr.io/macscampingapp:$BUILD_NUMBER
                                    docker run -dit --name macsstaging --net host macscampingarea.azurecr.io/macscampingapp:$BUILD_NUMBER
                                '''
                            }
                        break
                    }
                }

            }
        post{
            success{
                echo "====++++Run Container executed succesfully++++===="
            }
            failure{
                echo "====++++Run Container execution failed++++===="
            }
    
        }
    }
    }
    post{
        success{
            echo "========pipeline executed successfully ========"
        }
        failure{
            echo "========pipeline execution failed========"
        }
    }
}