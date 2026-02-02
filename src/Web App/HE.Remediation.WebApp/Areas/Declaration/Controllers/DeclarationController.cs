using HE.Remediation.Core.UseCase.Areas.Declaration.SetConfirmDeclaration;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace HE.Remediation.WebApp.Areas.Declaration.Controllers
{
    [Area("Declaration")]
    [Route("Declaration")]
    public class DeclarationController : StartController
    {
        private readonly ISender _sender;

        public DeclarationController(ISender sender)
            : base(sender)
        {
            _sender = sender;
        }

        protected override IActionResult DefaultStart => RedirectToAction("BeforeYouContinue", "Declaration", new { Area = "Declaration" });

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
            if (TempData.TryGetValue("Error", out var message))
            {
                ModelState.AddModelError(string.Empty, message.ToString());
            }
            return View();
        }

        [HttpPost(nameof(ConfirmDeclaration))]
        public async Task<IActionResult> ConfirmDeclaration()
        {
            var response = await _sender.Send(SetConfirmDeclarationRequest.Request);
            if (!response.Success)
            {
                TempData["Error"] = response.ErrorMessage;
                return RedirectToAction("Declaration", "Declaration", new { Area = "Declaration" });
            }
            return RedirectToAction("Index", "TaskList", new { Area = "Application" });
        }

        #endregion
    }
}