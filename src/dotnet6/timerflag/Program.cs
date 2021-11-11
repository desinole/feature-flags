using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.FeatureFilters;
using timerflag;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFeatureManagement().AddFeatureFilter<TimeWindowFilter>();
builder.Services.AddAzureAppConfiguration();

// Add services to the container.
builder.Services.AddControllersWithViews();

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



var app = builder.Build();
app.UseAzureAppConfiguration();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
