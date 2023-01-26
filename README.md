
## Meetings integrator service
based on .NET7 with clean architecture

## Development Build Prerequisites
Note that when doing a local development build, you need to have **Composer v2** installed. 
If your OS provides a lower version than v2, you can install Composer v2 manually. 

Source of all integrations is in Service folder in Infrastructure
![](https://github.com/AnastasKosstow/Meetings.Integrator/blob/main/integrations.png)

Microsoft Teams
--------------
To create MS teams meeting, first must be provided the necessary settings in appsettings.json
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
  },
```

Google Hangout
--------------
> 

