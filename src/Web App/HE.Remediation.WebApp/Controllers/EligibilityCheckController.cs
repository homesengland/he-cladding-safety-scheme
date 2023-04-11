using FluentValidation.AspNetCore;
using HE.Remediation.Core.UseCase.Areas.Application.EligibilityCheck.CreateEligibilityCheck;
using Microsoft.AspNetCore.Mvc;

namespace HE.Remediation.WebApp.Controllers
{
    public class EligibilityCheckController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(CreateEligibilityCheckRequest request)
        {
            var validator = new CreateEligibilityCheckRequestValidator();

            var validationResult = validator.Validate(request);

            if (validationResult.IsValid)
            {
                return RedirectToAction("Login", "Authentication");
            }

            validationResult.AddToModelState(ModelState, String.Empty);

            return View();
        }
    }
}