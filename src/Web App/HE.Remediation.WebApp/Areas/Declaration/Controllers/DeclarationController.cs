using HE.Remediation.Core.UseCase.Areas.Declaration.SetConfirmDeclaration;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HE.Remediation.WebApp.Areas.Declaration.Controllers
{
    [Area("Declaration")]
    [Route("BeforeYouContinue")]
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