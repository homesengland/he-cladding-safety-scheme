using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.VariationRequest
{
    public class ConfirmationViewModelValidator : AbstractValidator<ConfirmationViewModel>
    {
        public ConfirmationViewModelValidator()
        {
            RuleFor(x => x.VariationSummary)
                .NotEmpty()
                .WithMessage("Please enter a variation summary")
                .MaximumLength(1000)
                .WithMessage("Maximum 1000 characters allowed for variation summary");
        }
    }
}
