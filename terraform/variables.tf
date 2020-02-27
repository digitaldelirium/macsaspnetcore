variable "rg_name" {
    description = "The name of the resource group hosting the resources"
    type = string
}

variable "location" {
    description = "The region to deploy resources"
    type = string
    default = "eastus2"
}

variable "storage_name" {
    description = "The name of the storage account hosting the application"
    type = string
}

variable "asp_name" {
    description = "App Service Plan name"
    type = string
}

variable "app_tier" {
    description = "The Tier of App Service"
    type = string
    default = "Basic"
}

variable "app_size" {
    description = "The App Service Plan size"
    type = string
    default = "B1"
}

variable "image_name" {
    description = "The Docker image name"
    type = string
}

variable "sendgrid_api_key" {
    description = "The API Key for SendGrid auth"
    type = string
}

variable "sendgrid_password" {
    description = "The password to authenticate to SendGrid API"
    type = string
}

variable "facebook_app_id" {
    description = "The Application ID for Facebook integration"
    type = string
}

variable "facebook_app_secret" {
    description = "The Facebook App Secret for authenticating to the API"
    type = string
}

variable "activitydb_conn_string" {
    description = "The Connection string for Activity DB"
    type = string
}