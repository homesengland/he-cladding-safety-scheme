using FluentValidation;
using HE.Remediation.WebApp.Extensions;

namespace HE.Remediation.WebApp.ViewModels.Shared;

public class MonthlyCostViewModelValidator : AbstractValidator<MonthlyCostViewModel>
{
    public MonthlyCostViewModelValidator()
    {
        RuleFor(x => x.AmountText)
            .Must(x => string.IsNullOrEmpty(x) || decimal.TryParse(x, out _))
            .WithMessage(x => $"{x.MonthDate?.ToString("MMMM yyyy")} cost must be a number")
            .When(x => !string.IsNullOrEmpty(x.AmountText))
            .Must(x => x.NotExceedMaximumDigits())
            .WithMessage(x => $"{x.MonthDate?.ToString("MMMM yyyy")} cost must not have more than {FluentValidationExtensions.MaxNumberOfDigitsInNumber} digits")
            .DependentRules(() =>
            {
                RuleFor(x => x.Amount)
                    .GreaterThanOrEqualTo(0M)
                    .OverridePropertyName(nameof(MonthlyCostViewModel.AmountText))
                    .WithMessage(x => $"{x.MonthDate?.ToString("MMMM yyyy")} cost must not be negative")
                    .Must(x => x.HaveNoDecimalsInAmount())
                    .OverridePropertyName(nameof(MonthlyCostViewModel.AmountText))
                    .WithMessage(x => $"{x.MonthDate?.ToString("MMMM yyyy")} cost must be a whole number");
            });
    }
}
