using HE.Remediation.Core.UseCase.Areas.Declaration.SetConfirmDeclaration;
using HE.Remediation.WebApp.Authorisation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HE.Remediation.WebApp.Areas.Declaration.Controllers
{
    [Area("Declaration")]
    [Route("BeforeYouContinue")]
    [CookieApplicationAuthorise]
    public class DeclarationController : Controller
    {
        private readonly ISender _sender;

        public DeclarationController(ISender sender)
        {
            _sender = sender;
        }

        #region "Before You Continue"

        [HttpGet(nameof(BeforeYouContinue))]
        public IActionResult BeforeYouContinue()
        {
            return View();
        }

        #endregion

        #region "Declaration"

        [HttpGet(nameof(Declaration))]
        public IActionResult Declaration()
        {
            return View();
        }

        [HttpPost(nameof(ConfirmDeclaration))]
        public async Task<IActionResult> ConfirmDeclaration()
        {
            await _sender.Send(SetConfirmDeclarationRequest.Request);
            return RedirectToAction("Index", "TaskList", new { Area = "Application" });
        }

        #endregion
    }
}