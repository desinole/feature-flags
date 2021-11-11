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

// Add services to the container.
// builder.Services.AddControllersWithViews();

var app = builder.Build();

// // Configure the HTTP request pipeline.
// if (!app.Environment.IsDevelopment())
// {
//     app.UseExceptionHandler("/Home/Error");
//     // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//     app.UseHsts();
// }

// app.UseHttpsRedirection();
// app.UseStaticFiles();

// app.UseRouting();

// app.UseAuthorization();

app.MapGet("/", (HttpContext context, 
    DisplayService displayService) 
    => displayService.Display(builder.Services.BuildServiceProvider().GetRequiredService<IFeatureManager>()));

app.Run();
