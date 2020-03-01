terraform {
    backend "remote" {
        hostname = "app.terraform.io"
        organization = "icornett"
        workspaces {
            prefix = "macsaspnetcore-"
        }
    }
}

provider "azurerm" {
}

resource "azurerm_resource_group" "rg" {
    name = var.rg_name
    location = var.location
}

resource "azurerm_storage_account" "function_storage" {
    name = var.storage_name
    location = var.location
    resource_group_name = var.rg_name
    account_tier = "Standard"
    account_replication_type = "LRS"
}

resource "azurerm_app_service_plan" "asp" {
    name = var.asp_name
    resource_group_name = var.rg_name
    location = var.location
    kind = "Linux"
    reserved = true
    sku {
        tier = var.app_tier
        size = var.app_size
    }
}

resource "azurerm_app_service" "app" {
  name                = var.app_name
  location            = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name
  app_service_plan_id = azurerm_app_service_plan.asp.id

  site_config {
      always_on = true
      http2_enabled = true

      cors {
          allowed_origins = ["*"]
      }
  }

  app_settings = {
        "SiteEmailAddress" = "info@macscampingarea.com"
        "InfoEmailAddress" = "info@macscampingarea.com"
        "SendGridAPIKey" = var.sendgrid_api_key
        "SendGridUserName" = "azure_4ffd1bc7f64f364a8236fc8ae4b5b70a@azure.com"
        "SendGridPassword" = var.sendgrid_password
        "AdminEmail" = "admin@macscampingarea.com"
        "AdminPassword" = var.admin_password
        "FacebookAppId" = var.facebook_app_id
        "FacebookAppSecret" = var.facebook_app_secret
  }

  connection_string {
    name  = "ActivityDb"
    type  = "MySQL"
    value = var.activitydb_conn_string
  }
}