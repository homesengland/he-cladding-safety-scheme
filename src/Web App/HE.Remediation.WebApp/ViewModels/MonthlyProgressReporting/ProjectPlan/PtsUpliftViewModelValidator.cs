using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.ProjectPlan;

public class PtsUpliftViewModelValidator : AbstractValidator<PtsUpliftViewModel>
{
    private const int MaxFileSizeMb = 50 * 1024 * 1024;

    public PtsUpliftViewModelValidator()
    {
        RuleFor(x => x.File)
            .Must(x => x is null || x.Length <= MaxFileSizeMb)
            .WithMessage("File exceeds 50mb limit. Please upload an alternate file.");
    }
}