using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.VariationRequest
{
    public class AdjustScopeViewModelValidator : AbstractValidator<AdjustScopeViewModel>
    {
        public AdjustScopeViewModelValidator()
        {
            RuleFor(x => x.ChangeOfScope)
                .NotEmpty()
                    .WithMessage("Please enter the change of scope")
                .MaximumLength(1000)
                    .WithMessage("Information about the changes to project scope cannot exceed 1000 characters");
        }
    }
}
