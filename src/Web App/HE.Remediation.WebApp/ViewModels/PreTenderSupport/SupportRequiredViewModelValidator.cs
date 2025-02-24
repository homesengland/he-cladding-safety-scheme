using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.PreTenderSupport
{
    public class SupportRequiredViewModelValidator : AbstractValidator<SupportRequiredViewModel>
    {
        public SupportRequiredViewModelValidator()
        {
            RuleFor(x => x.SupportRequired)
                .NotNull()
                .WithMessage("Select an option");
        }
    }
}