
# Meetings integrator service
Based on .NET7 with clean architecture
All implementations are for "on beahlf of" code flow and meetings are created by accessToken provided from external provider app (google, microsoft, etc.).
> 

Source of all integrations is in "Services" folder in Infrastructure project
> 
![](https://github.com/AnastasKosstow/Meetings.Integrator/blob/main/integrations.png)

Microsoft Teams
--------------
To create microsoft teams meeting, first must be provided the necessary settings in appsettings.json
Documentation on how to create azure app and get them: https://learn.microsoft.com/en-us/azure/active-directory/develop/quickstart-register-app
> 
```json
  "MicrosoftGraphApiSettings": {
    "TenantId": "",
    "ClientId": "",
    "ClientSecret": "",
    "Scopes": [
      "https://graph.microsoft.com/.default"
    ]
  }
```

Google Hangout
--------------
To create google hangout meeting, first must be provided the necessary settings in appsettings.json
Documentation on how to create google calendar app and get them: https://developers.google.com/calendar/api/guides/overview
> 
```json
  "GoogleCalendarApiSettings": {
    "PrivateKey": "",
    "ClientEmail": ""
  }
```

