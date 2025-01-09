using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class LeaseholdersInformedLastViewModelValidator : AbstractValidator<LeaseholdersInformedLastViewModel>
{
    public LeaseholdersInformedLastViewModelValidator()
    {
        RuleFor(x => x.LeaseholdersInformedLastDate)
            .NotNull()
            .WithMessage("Enter a valid instruction date in a DD MM YYYY format")
            .Must(x => x <= DateTime.Today)
            .WithMessage("Date must be in the past");
    }
}