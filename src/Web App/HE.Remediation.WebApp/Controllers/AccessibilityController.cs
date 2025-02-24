using Microsoft.AspNetCore.Mvc;

namespace HE.Remediation.WebApp.Controllers;
public class AccessibilityController : Controller
{
    public AccessibilityController()
    {
    }

    public IActionResult Index()
    {
        if (HttpContext.Request.GetTypedHeaders().Referer != null)
        {
            ViewData["Referer"] = HttpContext.Request.GetTypedHeaders().Referer?.AbsolutePath;
        }

        return View();
    }
}
