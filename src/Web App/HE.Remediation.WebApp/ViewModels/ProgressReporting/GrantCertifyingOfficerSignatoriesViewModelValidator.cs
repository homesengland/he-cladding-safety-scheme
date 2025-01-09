using FluentValidation;
using HE.Remediation.WebApp.CustomPropertyValidators;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class GrantCertifyingOfficerSignatoriesViewModelValidator : AbstractValidator<GrantCertifyingOfficerSignatoriesViewModel>
{
    public GrantCertifyingOfficerSignatoriesViewModelValidator()
    {
        var currentYear = DateTime.Today.Year;

        RuleFor(x => x.Signatory)
            .NotEmpty()
            .WithMessage("Enter authorised signatory")
            .MaximumLength(150)
            .WithMessage("Signatory cannot be more than 150 characters");

        RuleFor(x => x.EmailAddress)
            .NotEmpty()
            .WithMessage("Enter an email address")
            .MaximumLength(150)
            .WithMessage("Email address cannot be more than 150 characters")
            .EmailAddress()
            .WithMessage("Enter an Email address in the correct format, like name@example.com")
            .NotValidEmailAddress();

        RuleFor(x => x.DateAppointedDay)
            .NotNull()
            .WithMessage("Enter a day");

        RuleFor(x => x.DateAppointedMonth)
            .NotNull()
            .WithMessage("Enter a month")
            .InclusiveBetween(1, 12)
            .When(x => x.DateAppointedMonth != null, ApplyConditionTo.CurrentValidator)
            .WithMessage("Please provide a valid month for Company's Date of Appointment to the project");

        RuleFor(x => x.DateAppointedYear)
            .NotNull()
            .WithMessage("Enter a year")
            .LessThanOrEqualTo(currentYear)
            .When(x => x.DateAppointedYear != null, ApplyConditionTo.CurrentValidator)
            .WithMessage($"Please provide a year no later than {currentYear}");

        RuleFor(x => new { x.DateAppointedDay, x.DateAppointedMonth, x.DateAppointedYear })
            .Must(x => BeAValidDayInTheMonth(x.DateAppointedDay, x.DateAppointedMonth, x.DateAppointedYear))
            .When(x => x.DateAppointedDay != null || x.DateAppointedMonth != null || x.DateAppointedYear != null)
            .WithName(x => nameof(x.DateAppointedDay))
            .WithMessage("Enter a valid day for Company's Date of Appointment to the project, for the entered month")
            .Must(x => BeInThePast(x.DateAppointedDay, x.DateAppointedMonth, x.DateAppointedYear))
            .When(x => x.DateAppointedDay != null || x.DateAppointedMonth != null || x.DateAppointedYear != null)
            .WithName(x => nameof(x.DateAppointedYear))
            .WithMessage("Company's Date of Appointment to the project cannot be in the future");
    }

    private bool BeAValidDayInTheMonth(int? day, int? month, int? year)
    {
        var currentYear = DateTime.Today.Year; 
        
        if (month is null or < 1 or > 12) return false;
        if (year is null || year < 1 || year > currentYear) return false;

        return day <= DateTime.DaysInMonth(year.Value, month.Value);
    }

    private static bool BeInThePast(int? day, int? month, int? year)
    {
        var currentYear = DateTime.Today.Year;

        if (day is null or < 1 or > 31) return false;
        if (month is null or < 1 or > 12) return false;
        if (year is null || year < 1 || year > currentYear) return false;
        if (day.HasValue && day.Value > DateTime.DaysInMonth(year.Value, month.Value)) return false;

        var date = new DateTime(year.Value, month.Value, day.Value);

        return date.Date <= DateTime.Today;
    }
}