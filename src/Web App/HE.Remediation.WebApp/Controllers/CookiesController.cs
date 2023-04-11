using HE.Remediation.Core.Interface;
using Microsoft.AspNetCore.Mvc;

namespace HE.Remediation.WebApp.Controllers
{
    public class CookiesController : Controller
    {
        private readonly IApplicationDataProvider _applicationDataProvider;

        public CookiesController(IApplicationDataProvider applicationDataProvider)
        {
            _applicationDataProvider = applicationDataProvider;
        }

        public IActionResult Index()
        {
            if (HttpContext.Request.GetTypedHeaders().Referer != null)
            {
                ViewData["Referer"] = HttpContext.Request.GetTypedHeaders().Referer?.AbsolutePath;
            }

            ViewData["AppDataCookieName"] = _applicationDataProvider.GetCookieName;

            return View();
        }
    }
}
