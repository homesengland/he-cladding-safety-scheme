using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.FireRiskAppraisal
{
    public class CladdingSystemViewModelValidator : AbstractValidator<CladdingSystemViewModel>
    {
        public CladdingSystemViewModelValidator()
        {
            RuleFor(x => x.CladdingSystemTypeId)
                .NotNull()
                .NotEqual(0)
                .WithMessage("Select an option");

            RuleFor(x => x.InsulationTypeId)
                .NotNull()
                .NotEqual(0)
                .WithMessage("Select an option");
        }
    }
}