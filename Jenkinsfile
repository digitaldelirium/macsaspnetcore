pipeline{
    agent{
        label "linux"
    }
    parameters {
        choice(name: 'BuildConfiguration', description: 'The Build Configuration to use', choices: ['Release','Debug'])
        choice(name: 'RuntimeEnvironment', description: 'The environment to run in', choices: ['Development', 'Staging', 'Production'])
        string(name: 'VaultName', description: 'The name of the Azure Key Vault hosting secrets', defaultValue: 'macscampvault')
    }
    environment {
        BUILD_CONFIGURATION = "${BuildConfiguration}"
        RUNTIME_ENVIRONMENT = "${RuntimeEnvironment}"
    }
    stages{
        stage("Setup Environment"){
            steps{
                echo "========Setting Up Environment for Deployment========"
                deleteDir()
                git branch: 'master', credentialsId: 'github-creds', url: 'https://github.com/digitaldelirium/macsaspnetcore.git'
                
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
                    sed -i "s/#{BuildConfiguration}#/${BUILD_CONFIGURATION}/g" Dockerfile
                    sed -i "s/#{Environment}#/${RUNTIME_ENVIRONMENT}/g" Dockerfile
                '''

                echo "====++++Build Docker Container++++===="
                script {
                    switch(RUNTIME_ENVIRONMENT) {
                        case "Development":
                            sh"""
                                docker build --rm --compress -t macscampingapp:development -t macscampingapp:\$BUILD_NUMBER -t macscampingarea.azurecr.io/macscampingapp:\$BUILD_NUMBER -t macscampingarea.azurecr.io/macscampingapp:development -f Dev.dockerfile
                            """
                        break
                        case "Staging":
                            sh"""
                                docker build --rm --compress -t macscampingapp:staging -t macscampingapp:\$BUILD_NUMBER -t macscampingarea.azurecr.io/macscampingapp:\$BUILD_NUMBER -t macscampingarea.azurecr.io/macscampingapp:staging .
                            """                        
                        break
                        case "Production":
                            sh"""
                                docker build --rm --compress -t macscampingapp:latest -t macscampingapp:\$BUILD_NUMBER -t macscampingarea.azurecr.io/macscampingapp:\$BUILD_NUMBER -t macscampingarea.azurecr.io/macscampingapp:prod -t macscampingarea.azurecr.io/macscampingapp:latest .
                            """
                        break
                    }
                }
            }
        }
        stage("Push container to registry"){
            steps{
                echo "====++++Push Container to ACR++++===="
                withCredentials([azureServicePrincipal('JenkinsWorker')]) {
                    script {                        
                        sh'''
                            docker login macscampingarea.azurecr.io -u $AZURE_CLIENT_ID -p $AZURE_CLIENT_SECRET
                        '''
                        switch(RUNTIME_ENVIRONMENT){
                            case "Development":
                                echo "====++++Not Pushing DevCode to upstream repo++++===="
                            break
                            case "Staging":
                                sh '''
                                    docker push macscampingarea.azurecr.io/macscampingapp:staging
                                '''
                            break
                            case "Production"
                                sh'''
                                    docker push macscampingarea.azurecr.io/macscampingapp:prod
                                '''
                            break
                        }
                    }
                }
            }
        }
        stage("Run Container"){
            steps{
                echo "====++++Connect to MacsVM via SSH++++===="
                script {
                    switch (RUNTIME_ENVIRONMENT){
                        case "Development":
                            sh'''
                                docker run -dit --name macsdev macscampingapp:$BUILD_NUMBER
                            '''
                        break
                        case "Staging":
                            sshagent(['macscampingarea']) {                        
                                sh'''
                                    ssh -A macs@macsvm.macscampingarea.com
                                    
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
                                    docker rm macsprod -f
                                    docker run -dit --name macsprod-$BUILD_NUMBER --net host macscampingarea.azurecr.io/macscampingapp:$BUILD_NUMBER
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