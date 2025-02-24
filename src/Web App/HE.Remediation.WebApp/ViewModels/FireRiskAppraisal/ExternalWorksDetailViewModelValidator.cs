using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.FireRiskAppraisal
{
    public class ExternalWorksDetailViewModelValidator: AbstractValidator<ExternalWorksDetailViewModel>
    {
        public ExternalWorksDetailViewModelValidator()
        {
            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Enter a description")
                .MaximumLength(1000)
                .WithMessage("Description cannot exceed 1000 characters");
        }
    }
}
