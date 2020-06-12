using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using launchdarklymvc.Models;
using Microsoft.Extensions.Configuration;
using LaunchDarkly.Client;

namespace launchdarklymvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index(string email="default email address")
        {
            LdClient client = new LdClient(_configuration["ConnectionStrings:AppConfig"]);
            LaunchDarkly.Client.User user = LaunchDarkly.Client.User.WithKey(email);
            var flag = client.BoolVariation("test", user, false);
            ViewBag.Flag = flag;
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
}
