App Secrets Local Setup
* copy the AzureAd section (and any additional sections containing sensitive information) in appsettings.json (purposely left blank).
  Make sure to leave these values blank on appsettings.json and appsettings.<environment>.json files.
* create a new file called "secrets.json" (preferably on the folder containing the Poketto.Api.csproj file) and paste the copied section.
* set the values for the AzureAd configuration in "secrets.json" and save the file
* open a terminal window and set the path to the root project folder of Poketto.Api
* (only for first time setup) run this command to initialize Secret Manager: dotnet user-secrets init
* run this command to set the contents of the 'secrets.json' file to the secret storage: type .\secrets.json | dotnet user-secrets set
* don't forget to add the "secrets.json" file to .gitignore