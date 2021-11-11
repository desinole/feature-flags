# Add a timer feature flag to a .net 6 mvc app using Azure App Configuration

In this app, we will use a sliding timewindow feature flag to show how to enable and disable a Holiday sales banner

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

We then define a class to map the feature flags to

```csharp

internal class Globals
{
    internal enum FeatureFlags
    {
        HolidaySaleTimeWindow
    }
}

```

Now we can connect to the Azure resource using the connection string
```csharp

builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
        var settings = config.Build();
        config.AddAzureAppConfiguration(options =>
        {
            options.UseFeatureFlags();
            options.ConfigureRefresh(refresh =>
            {
                refresh.Register(nameof(Globals.FeatureFlags.HolidaySaleTimeWindow), refreshAll: true).SetCacheExpiration(TimeSpan.FromSeconds(10));
            });
            options.Connect(settings[$"{nameof(AppSecrets)}:{nameof(AppSecrets.AzureAppConfigConnectionString)}"]);
        });
});

```

App configuration gets hooked up to the middleware to detect refresh for the flags (based on above code section it checks for refresh every 10 seconds - default is 30 seconds)

```csharp

var app = builder.Build();
app.UseAzureAppConfiguration();

```

Finally, we add the ability for the app to handle featuremanagement and azure app configuration

```csharp
builder.Services.AddFeatureManagement().AddFeatureFilter<TimeWindowFilter>();
builder.Services.AddAzureAppConfiguration();
```

Now the holiday sale flag can be access from the controller code using dependency injection as follows:

```csharp
    private readonly IFeatureManager _featureManagement;
    public HomeController(IFeatureManager featureManagement)
    {
        _featureManagement = featureManagement;
    }
    public IActionResult Index()
    {
        var flag = _featureManagement.IsEnabledAsync(nameof(Globals.FeatureFlags.HolidaySaleTimeWindow)).Result;
        return View();
    }
```

We can also incorporate feature management into razor views with a little bit of plumbing. Add the below razor code in your entire app by appending to the _ViewImports.cshtml file in the Views folder

```razor
@addTagHelper *, Microsoft.FeatureManagement.AspNetCore
```

At this point we can invoke feature flags in our views using ```<feature>``` tag

```html
<feature name="@Globals.FeatureFlags.HolidaySaleTimeWindow">
    <div class="text-center">
        <a class="nav-link text-dark" asp-area="" asp-controller="Home" asp-action="HolidaySale">Click here for the
            sale</a>
    </div>

    <div class="text-center">
        <img src="holidaysale.jpg" />
    </div>
</feature>
```

We can also make the view behave in the opposite way of the toggle using ```negate```. For instance, by appending below code we can create the equivalent of an _if-else_ logical flow in the view

```html
<feature name="@Globals.FeatureFlags.HolidaySaleTimeWindow" negate="true">
    <div class="text-center">
        <img src="holidaysale.jpg" </div>
</feature>
```

Now, we add an additional controller for the products on sale. We will use a FeatureGate for this controller to prevent access when the toggle is off

```csharp
    public IActionResult HolidaySale()
    {
        return View();
    }
```

Create a _HolidaySale.cshtml_ view page as shown below
```razor
@{
    ViewData["Title"] = "Holiday Sales";
}
<h1>@ViewData["Title"]</h1>
<img src="/products.png" />
```

You can toggle the flag in Azure App Config and watch how the home page flips between the sales banner and coming soon banner. Additionally, you can also try to access the HolidaySales page and experience how the page either displays or throws a 404 error depending on how the flag was set.