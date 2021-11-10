# Add a timer feature flag to a .net 6 mvc app using Azure App Configuration

Create a new mvc project using following dotnetcli command

```dotnetcli
dotnet new mvc --framework net6.0
```

Add the FeatureManagement library using following dotnetcli command

```dotnetcli
 dotnet add package Microsoft.FeatureManagement
```

Enabled the mvc app to use secrets so we can use connection string

```dotnetcli
dotnet user-secrets init
```

Create an Azure App Configuration resource using these [instructions](https://docs.microsoft.com/en-us/azure/azure-app-configuration/quickstart-feature-flag-aspnet-core?tabs=core5x#create-an-app-configuration-store)

In this newly created resource navigate to the _Access Keys_ section and copy the _Primary Key -> Connection String_ which will look something like this:

```
Endpoint=https://resourcename.azconfig.io;Id=longAlphaNumericString
```

Now we could simply use magic strings to save and retrieve the connection string but we prefer strongly typed class instead.

So, for this example, we use a class

```csharp
namespace timerflag
{
    public class AppSecrets
    {
        public string AzureAppConfigConnectionString { get; set; }
    }
}
```

Store above connection string into  dotnet user secrets as follows

```dotnetcli
dotnet user-secrets set "AppSecrets:AzureAppConfigConnectionString" "connection string goes here"
```

While we have to use magic strings for storage the retrieval can be done using strongly typed class.

At this point, if we put a breakpoint in our _Program.cs_ file and run in debug mode we can find this connection string under your the _WebApplicationBuilder.Configuration_ object. This is because .NET uses secrets.json as a default config provider.

Now we can start hooking up this application to Azure App Configuration and consuming the Feature Flag.

We will need the following:

```dotnetcli
dotnet add package Microsoft.Azure.AppConfiguration.AspNetCore
```

```dotnetcli
dotnet add package Microsoft.FeatureManagement.AspNetCore
```

```dotnetcli
dotnet add package Microsoft.Extensions.Configuration.AzureAppConfiguration
```