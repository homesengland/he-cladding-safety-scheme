using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.ClosingReport
{
    public class ThirdPartyYesNoDeclarationViewModelValidator : AbstractValidator<ThirdPartyYesNoDeclarationViewModel>
    {
        public ThirdPartyYesNoDeclarationViewModelValidator()
        {
            RuleFor(x => x.Declaration)
                .NotNull()
                .WithMessage("Please state Yes or No.");
        }
    }
}
 