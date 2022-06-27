App Secrets Local Setup
* copy the AzureAd section (and any additional sections containing sensitive information) in appsettings.json (purposely left blank).
  Make sure to leave these values blank on appsettings.json and appsettings.environment.json files.
* create a new file called "secrets.json" (preferably on the folder containing the Poketto.Api.csproj file) and paste the copied section.
* set the values for the AzureAd configuration in "secrets.json" and save the file
* open a terminal window and set the path to the root project folder of Poketto.Api
* (only for first time setup) run this command to initialize Secret Manager: dotnet user-secrets init
* run this command to set the contents of the 'secrets.json' file to the secret storage: type .\secrets.json | dotnet user-secrets set
* don't forget to add the "secrets.json" file to .gitignore


AzureAd configuration explanation

* Instance	- "https://login.microsoftonline.com/"
* Domain	- domainName.onmicrosoft.com
* ClientId	- the application ID of the app registration with permission to use the protected API, i.e. the ReactJs App etc. (the app dependent to the API)
* TenantId	- "common"
* Audience	- the application ID of the app registration of the protected API, i.e. this API

AzureB2C configuration sample

"AzureAdB2C": {
  "Instance": "https://<your-tenant-name>.b2clogin.com",
  "ClientId": "<web-app-application-id>",
  "Domain": "<your-b2c-domain>",
  "SignedOutCallbackPath": "/signout/<your-sign-up-in-policy>",
  "SignUpSignInPolicyId": "<your-sign-up-in-policy>"
}