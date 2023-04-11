using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.FireRiskAppraisal
{
    public class CompletedAppraisalViewModelValidator : AbstractValidator<CompletedAppraisalViewModel>
    {
        public CompletedAppraisalViewModelValidator()
        {
            RuleFor(x => x.IsAppraisalCompleted)
                .NotNull()
                .WithMessage("Select an option");
        }
    }
}