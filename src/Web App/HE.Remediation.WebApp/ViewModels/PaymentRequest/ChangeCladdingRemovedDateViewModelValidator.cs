using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.PaymentRequest;

public class ChangeCladdingRemovedDateViewModelValidator : AbstractValidator<ChangeCladdingRemovedDateViewModel>
{    
    private DateTime EarliestDate = new(2022, 1, 1);
    private const int MaxYear = 2040;

    public ChangeCladdingRemovedDateViewModelValidator()
    {
        RuleFor(x => x.DateRemovedMonth)
            .NotNull()
            .WithMessage("Please enter the removal month")
            .NotEmpty()
            .WithMessage("Please enter the removal month")
            .InclusiveBetween(1, 12)
            .WithMessage("Please enter the removal month");

        RuleFor(x => x.DateRemovedYear)
            .NotNull()
            .WithMessage("Please enter the removal year")
            .NotEmpty()
            .WithMessage("Please enter the removal year")
            .DependentRules(() =>
            {
                RuleFor(x => x)                    

                    .Must(x => BeOnAfter(x.DateRemovedMonth, x.DateRemovedYear, EarliestDate.Month, EarliestDate.Year))
                    .WithName("ProjectStartDateMonth")
                    .WithMessage($"The start date cannot be earlier than 01/2022")
                    .When(x => x.DateRemovedMonth.HasValue && x.DateRemovedYear.HasValue)
                    .Must(x => !DateBeyondCurrentMonth(x.DateRemovedMonth, x.DateRemovedYear))
                    .WithName("DateRemovedMonth")
                    .WithMessage("Please enter a date prior to today's date");                    
            });        
    }    

    private bool DateBeyondCurrentMonth(int? month, int? year)
    {
        if (month is null or < 1 or > 12) return false;
        if (year is null or < 1 or > MaxYear) return false;

        var inputDateStartOfMonth = new DateTime(year.Value, month.Value, 1);
        var currentStartOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

        return inputDateStartOfMonth > currentStartOfMonth;                
    }

    private bool BeOnAfter(int? actualMonth, int? actualYear, int? validMonth, int? validYear)
    {
        if (actualMonth is null or < 1 or > 12) return false;
        if (actualYear is null or < 1 or > MaxYear) return false;
        if (validMonth is null or < 1 or > 12) return false;
        if (validYear is null or < 1 or > MaxYear) return false;

        var date = new DateTime(actualYear.Value, actualMonth.Value, 1);
        var validDate = new DateTime(validYear.Value, validMonth.Value, 1).AddDays(-1);

        return date.Date > validDate;
    }
}
