using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.WithdrawalRequest
{
    public class ReasonForClosingModelValidator : AbstractValidator<ReasonForClosingViewModel>
    {
        public ReasonForClosingModelValidator()
        {
            RuleFor(x => x.ReasonForClosing)
                .NotEmpty()
                .WithMessage("Please provide an Explanation")
                .MaximumLength(1000)
                .WithMessage("Explanation must be less than 1000 characters");
        }
    }
}
