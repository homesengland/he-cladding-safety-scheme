using HE.Remediation.WebApp.Authorisation;
using Microsoft.AspNetCore.Mvc;

namespace HE.Remediation.WebApp.Areas.Application.Controllers
{
    [Area("Application")]
    [CookieApplicationAuthorise]
    public class LeaseholderEngagementController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}