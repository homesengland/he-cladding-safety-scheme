using FluentValidation;

using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.ProjectPlan
{
    public class ProjectPlanViewModelValidator : AbstractValidator<ProjectPlanViewModel>
    {
        public ProjectPlanViewModelValidator()
        {

            RuleFor(x => x.InternalAdditionalWork)
                .NotEmpty()
                .WithMessage("Please select whether you are planning to do other internal works");

            RuleFor(x => x.IntentToProceedType)
                .IsInEnum()
                .WithMessage("Please select an intent to proceed type")
                .NotEmpty()
                .WithMessage("Please select an intent to proceed type");

            When(x => x.ApplicationScheme != EApplicationScheme.SocialSector, () =>
            {
                RuleFor(x => x.RemainingAmount)
                    .NotNull()
                    .WithMessage("Please enter the remaining amount")
                    .GreaterThanOrEqualTo(0)
                    .WithMessage("Remaining amount must be greater than zero")
                    .Must((model, remainingAmount) =>
                        !remainingAmount.HasValue ||
                        !model.AmountPaidForPTS.HasValue ||
                        remainingAmount.Value <= model.AmountPaidForPTS.Value)
                    .WithMessage("Remaining amount cannot be greater than the amount paid for PTS");

                RuleFor(x => x.EnoughFunds)
                    .NotEmpty()
                    .WithMessage("Please specify if you have enough funds");
            });
        }
    }
}
