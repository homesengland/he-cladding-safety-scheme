using FluentValidation;

namespace HE.Remediation.Core.UseCase.Areas.Administration.Credentials.ChangePassword
{
    public class ChangePasswordViewModelValidator : AbstractValidator<ChangePasswordRequest>
    {
        public ChangePasswordViewModelValidator()
        {
            RuleFor(x => x.OldPassword)
                .NotEmpty()
                .WithMessage("Please enter the old password")
                .Must(x => x.ToString().Length > 8)
                .WithMessage("Please enter the correct old password.");
            RuleFor(x => x.NewPassword)
                .NotEmpty()                
                .WithMessage("Please enter the new password.");                        
            RuleFor(x => x.NewPassword)
                .Equal(x => x.ConfirmPassword)
                .When(x => !String.IsNullOrWhiteSpace(x.NewPassword))
                .WithMessage("The new and confirm passwords must match.");            
        }
    }
}
