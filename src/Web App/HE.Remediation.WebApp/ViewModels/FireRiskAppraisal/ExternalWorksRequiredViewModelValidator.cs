using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.FireRiskAppraisal
{
    public class ExternalWorksRequiredViewModelValidator: AbstractValidator<ExternalWorksRequiredViewModel>
    {
        public ExternalWorksRequiredViewModelValidator()
        {
            RuleFor(x => x.WorksRequired)
                .NotEmpty()
                .WithMessage("Please select an option");
        }
    }
}
