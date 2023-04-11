using Microsoft.AspNetCore.Mvc;

namespace HE.Remediation.WebApp.Controllers
{
    public class LandingPageController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}