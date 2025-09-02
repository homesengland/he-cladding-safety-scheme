using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.VariationRequest;

public class AdjustEndDateViewModelValidator : AbstractValidator<AdjustEndDateViewModel>
{
    private const int MaxYear = 2040;

    public AdjustEndDateViewModelValidator()
    {
        RuleFor(x => x.NewEndMonth)
                .NotEmpty()
                .WithMessage("Please enter month")
                .GreaterThan(0)
                .WithMessage("Month should be greater than 0")
                .LessThanOrEqualTo(12)
                .WithMessage("Month should be less than or equal to 12");

        RuleFor(x => x.NewEndYear)
                .NotEmpty()
                .WithMessage("Please enter year")
                .LessThanOrEqualTo(MaxYear)
                .WithMessage($"Please enter a year no later than {MaxYear}")
                .DependentRules(() =>
                {
                    RuleFor(x => x)
                        .Must(x => BeInTheFuture(x.NewEndMonth, x.NewEndYear))
                        .WithName("NewEndMonth")
                        .WithMessage(x => "The end date must be in the future")
                        .When(x => x.LastMonthlyPaymentCompleted == false && x.NewEndMonth.HasValue && x.NewEndYear.HasValue);

                    RuleFor(x => x)
                        .Must(x => BeInTheFuture(x.NewEndMonth, x.NewEndYear, x.PreviousEndMonth, x.PreviousEndYear))
                        .WithName("NewEndMonth")
                        .WithMessage(x => "The end date must be later than the current end date")
                        .When(x => x.LastMonthlyPaymentCompleted == true && x.NewEndMonth.HasValue && x.NewEndYear.HasValue);
                });
    }

    private bool BeInTheFuture(int? month, int? year)
    {
        if (month is null or < 1 or > 12) return false;
        if (year is null or < 1 or > MaxYear) return false;

        var date = new DateTime(year.Value, month.Value, 1);
        var validDate = (new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)).AddMonths(-1);

        return date.Date > validDate;
    }

    private bool BeInTheFuture(int? newEndMonth, int? newEndYear, int? currentEndMonth, int? currentEndYear)
    {
        if (newEndMonth is null or < 1 or > 12) return false;
        if (newEndYear is null or < 1 or > MaxYear) return false;

        var newEndDate = new DateTime(newEndYear.Value, newEndMonth.Value, 1);
        var currentEndDate = new DateTime(currentEndYear!.Value, currentEndMonth!.Value, 1);

        return newEndDate.Date > currentEndDate.Date;
    }
}