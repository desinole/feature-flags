using Microsoft.FeatureManagement;

var builder = WebApplication.CreateBuilder(args);

// access the configuration from the WebApplicationBuilder
IConfiguration configuration = builder.Configuration;

// get featuremanagement from the config file so we can map the key value pairs as toggles
// hook this up to the services collection
builder.Services.AddFeatureManagement(configuration.GetSection("FeatureManagement"));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

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
