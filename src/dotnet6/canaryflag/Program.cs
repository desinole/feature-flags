using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.FeatureFilters;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<DisplayService>
    (new DisplayService());

// access the configuration from the WebApplicationBuilder
IConfiguration configuration = builder.Configuration;

// get featuremanagement from the config file so we can map the key value pairs as toggles
// hook this up to the services collection
builder.Services.AddFeatureManagement(configuration.GetSection("FeatureManagement"))
.AddFeatureFilter<ContextualTargetingFilter>();


var app = builder.Build();

app.MapGet("/", (HttpContext context, 
    DisplayService displayService) 
    => displayService.Display(builder.Services.BuildServiceProvider().GetRequiredService<IFeatureManager>()));

app.Run();
