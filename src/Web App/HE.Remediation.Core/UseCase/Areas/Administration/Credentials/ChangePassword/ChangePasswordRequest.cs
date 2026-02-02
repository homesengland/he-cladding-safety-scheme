using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.Administration.Credentials.ChangePassword
{
    public class ChangePasswordRequest : IRequest<Unit>
    {
        public string OldPassword { get; set; }

        public string NewPassword { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
