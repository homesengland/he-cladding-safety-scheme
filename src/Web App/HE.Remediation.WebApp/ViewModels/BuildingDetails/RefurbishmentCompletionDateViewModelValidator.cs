using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.BuildingDetails;

public class RefurbishmentCompletionDateViewModelValidator : AbstractValidator<RefurbishmentCompletionDateViewModel>
{
    private const int SqlMinYear = 1753;

    public RefurbishmentCompletionDateViewModelValidator()
    {
        #region RefurbishmentCompletionDate

        RuleFor(x => x.RefurbishmentCompletionDateMonth)
                .NotNull()
                .WithMessage("Please enter a month")
                .NotEmpty()
                .WithMessage("Please enter a month")
                .InclusiveBetween(1, 12)
                .WithMessage("Please enter a valid month (1–12)")
                    .When(x => x.RefurbishmentCompletionDateMonth.HasValue || x.RefurbishmentCompletionDateYear.HasValue);

        RuleFor(x => x.RefurbishmentCompletionDateYear)
                .NotNull()
                .WithMessage("Please enter a year")
                .NotEmpty()
                .WithMessage("Please enter a year")
                .GreaterThanOrEqualTo(SqlMinYear)
                .WithMessage("Please enter a valid year")
                .When(x => x.RefurbishmentCompletionDateMonth.HasValue || x.RefurbishmentCompletionDateYear.HasValue);

        RuleFor(x => x.RefurbishmentCompletionDateMonth)
                .NotNull()
                .WithMessage("Please enter a month")
                .When(x => x.RefurbishmentCompletionDateYear.HasValue && !x.RefurbishmentCompletionDateMonth.HasValue);

        RuleFor(x => new { x.RefurbishmentCompletionDateMonth, x.RefurbishmentCompletionDateYear })
                .Must(x => BeInThePast(x.RefurbishmentCompletionDateMonth, x.RefurbishmentCompletionDateYear))
                .WithName("RefurbishmentCompletionDateMonth")
                .WithMessage($"The refurbishment completion date must be in the past")
                .When(x => x.RefurbishmentCompletionDateMonth.HasValue && x.RefurbishmentCompletionDateYear.HasValue);

        #endregion
    }

    private bool BeInThePast(int? month, int? year)
    {
        var minDate = new DateTime(1753, 1, 1);

        if (month is null or < 1 or > 12) return false;
        if (year is null || year < minDate.Year) return false;

        var date = new DateTime(year.Value, month.Value, 1);

        if (date < minDate) return false;

        var currentMonthStart = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);

        return date < currentMonthStart;
    }
}