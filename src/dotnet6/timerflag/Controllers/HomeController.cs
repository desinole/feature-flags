using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.Mvc;
using timerflag.Models;

namespace timerflag.Controllers;

public class HomeController : Controller
{
    private readonly IFeatureManager _featureManagement;
    public HomeController(IFeatureManager featureManagement)
    {
        _featureManagement = featureManagement;
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Index()
    {
        var flag = _featureManagement.IsEnabledAsync(nameof(Globals.FeatureFlags.HolidaySaleTimeWindow)).Result;
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [FeatureGate(nameof(Globals.FeatureFlags.HolidaySaleTimeWindow))]
    public IActionResult HolidaySale()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
