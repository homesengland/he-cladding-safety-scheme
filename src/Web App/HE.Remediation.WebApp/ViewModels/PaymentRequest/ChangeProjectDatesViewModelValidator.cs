using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.PaymentRequest;

public class ChangeProjectDatesViewModelValidator : AbstractValidator<ChangeProjectDatesViewModel>
{
    private const int MaxYear = 2040;

    public ChangeProjectDatesViewModelValidator()
    {
        RuleFor(x => x.ProjectDateEndMonth)
        .NotNull()
        .WithMessage("Please enter the end date month")
        .NotEmpty()
        .WithMessage("Please enter the end date month")
        .InclusiveBetween(1, 12)
        .WithMessage("Please enter the end date month");

        RuleFor(x => x.ProjectDateEndYear)
        .NotNull()
        .WithMessage("Please enter the end date year")
        .NotEmpty()
        .WithMessage("Please enter the end date year")
        .LessThanOrEqualTo(MaxYear)
        .WithMessage($"Please enter the end year no later than {MaxYear}")
        .DependentRules(() =>
        {                
            RuleFor(x => x)
                .Must(x => SecondDateIsAfterOrSameAsFirst(x.ExpectedStartDate, x.ProjectDateEndMonth, x.ProjectDateEndYear))
                .WithName("ProjectDateEndMonth")
                .WithMessage(x => $"The end date cannot be before the start date")
                .When(x => x.ProjectDateEndMonth.HasValue && x.ProjectDateEndYear.HasValue);
        });                                
    }

    private bool SecondDateIsAfterOrSameAsFirst(DateTime? firstDate, int? secondDateMonth, int? secondDateYear)
    {
        if (firstDate is null ) return false;
        if (secondDateMonth is null or < 1 or > 12) return false;
        if (secondDateYear is null or < 1 or > MaxYear) return false;

        var firstDateForComparison = new DateTime(firstDate.Value.Year, firstDate.Value.Month, 1);
        var secondDateForComparison = new DateTime(secondDateYear.Value, secondDateMonth.Value, 1);

        return secondDateForComparison.Date >= firstDateForComparison;
    }    
}
