using FluentValidation;

namespace HE.Remediation.Core.UseCase.Areas.Application.EligibilityCheck.CreateEligibilityCheck
{
    public class CreateEligibilityCheckRequestValidator : AbstractValidator<CreateEligibilityCheckRequest>
    {
        public CreateEligibilityCheckRequestValidator()
        {
            RuleFor(x => x.ApplyingForTheRemediationOfUnsafeCladding)
                .Must(x => x == true)
                .WithMessage("This must be checked to proceed.");

            RuleFor(x => x.EligibleToCreateApplication)
                .Must(x => x == true)
                .WithMessage("This must be checked to proceed.");

            RuleFor(x => x.ReadGuidanceAndPoliciesRelated)
                .Must(x => x == true)
                .WithMessage("This must be checked to proceed.");

            RuleFor(x => x.AwareOfTheBuildingHeight)
                .Must(x => x == true)
                .WithMessage("This must be checked to proceed.");
        }
    }
}