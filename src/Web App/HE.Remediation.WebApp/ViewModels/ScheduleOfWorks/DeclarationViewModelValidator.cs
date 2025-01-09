using FluentValidation;
using HE.Remediation.Core.Extensions;

namespace HE.Remediation.WebApp.ViewModels.ScheduleOfWorks;

public class DeclarationViewModelValidator : AbstractValidator<DeclarationViewModel>
{
    public DeclarationViewModelValidator()
    {
        RuleFor(x => x.ConfirmedAwareOfProcess)
            .Must(x => x == true)
            .WithMessage("Confirm that you are aware of the process and timeframes for submitting claims and that failure to submit claims in line with process will result in delayed payments.");

        RuleFor(x => x.ConfirmedAwareOfVariationApproval)
            .Must(x => x == true)
            .WithMessage("Confirm that you are aware that Homes England approves each variation on its individual merits and that approval does not set a precedent for future variation requests.");

        RuleFor(x => x.ConfirmedAccuratelyProfiledCosts)
            .Must(x => x == true)
            .WithMessage("Confirm that you have profiled my costs as accurately as possible in accordance with my works contract and that this excludes ineligible VAT.");

        RuleFor(x => x.ProjectStartDate)
            .GreaterThanOrEqualTo(DateTime.Today.FirstOfMonth().AddMonths(1))
            .WithMessage("The start date for your schedule must be in the future - Click here to change your schedule dates");

    }
}
