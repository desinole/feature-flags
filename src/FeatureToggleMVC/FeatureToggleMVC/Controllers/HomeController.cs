using FeatureToggle;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FeatureToggleMVC.Controllers
{
    public class AISimpleFeature : SimpleFeatureToggle { }
    public class AIDateFeature : EnabledOnOrAfterDateFeatureToggle { }
    public class AICustomFeature : CustomFeatureToggle { }

    public class HomeController : Controller
    {
        readonly IFeatureToggle aiSimpleFeatureFlag = new AISimpleFeature();
        readonly IFeatureToggle aiDateFeatureFlag = new AIDateFeature();
        readonly IFeatureToggle aiCustomFeatureFlag = new AICustomFeature();
        public ActionResult Index()
        {
            var codeFlag = true;
            var plainFlag = bool.Parse(ConfigurationManager.AppSettings["ShowAIButton"]);
            bool aiSimpleFlag = aiSimpleFeatureFlag.FeatureEnabled;
            bool aiDateFlag = aiDateFeatureFlag.FeatureEnabled;
            bool aiCustomFlag = aiCustomFeatureFlag.FeatureEnabled;
            ViewBag.Flag = codeFlag; //replace with approriate flag
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public static int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
    }

    public class CustomFeatureToggle : IFeatureToggle
    {
        public bool FeatureEnabled => true;
    }
}