# Add a basic feature flag to a .net 6 mvc app

Create a new mvc project using following dotnetcli command

```dotnetcli
dotnet new mvc --framework net6.0
```

Add the FeatureManagement library using following dotnetcli command

```dotnetcli
 dotnet add package Microsoft.FeatureManagement
```

In this case we will toggle the feature from the appsettings.json file so add this blurb in appsettings.json

```json
  "FeatureManagement": {
    "Toggle": true
  }
```

Now we need to hook up the mvc app to read the config file. Looking inside the Program.cs file for the default app created, we can see this line of code

```csharp
var builder = WebApplication.CreateBuilder(args);
``` 

This code initializes a new instance of the _WebApplicationBuilder_ class with preconfigured defaults, which means the mvc app will have appsettings.json as a preconfigured option. We should be able to get the necessary key-value pairs using _GetSection()_ method on the configuration.

Once we get the appropriate section and toggle from the config file, we can hook it up to the app by adding _FeatureManagement_ to the _Services_ collection of the app. This all can be accomplished as follows

```csharp

// access the configuration from the WebApplicationBuilder
IConfiguration configuration = builder.Configuration;

// get featuremanagement from the config file so we can map the key value pairs as toggles
// hook this up to the services collection
builder.Services.AddFeatureManagement(configuration.GetSection("FeatureManagement"));

```

This will require invoking the featuremanagement library 

```csharp
using Microsoft.FeatureManagement;
```

At this point the application is capable of reading the feature flag. We can now add it to a controller as follows:

Declare an _IFeatureManager_ variable at the class level of the controller

```csharp
private readonly IFeatureManager _featureManager;
```

Inject the featuremanager into the controller and read it into local variable 

```csharp
    public HomeController(ILogger<HomeController> logger, IFeatureManager featureManager)
    {
        _logger = logger;
        _featureManager = featureManager;
    }
```

Now we can read the feature flag within the controller scope as follows:

```csharp
var flag = _featureManager.IsEnabledAsync("Toggle").Result;
```

The flag can be flipped to _true_ or _false_ depending on the value in the config file