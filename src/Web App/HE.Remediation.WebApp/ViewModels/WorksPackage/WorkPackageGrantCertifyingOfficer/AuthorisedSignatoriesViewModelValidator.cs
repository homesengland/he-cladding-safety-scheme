using FluentValidation;
using HE.Remediation.WebApp.CustomPropertyValidators;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageGrantCertifyingOfficer;

public class AuthorisedSignatoriesViewModelValidator : AbstractValidator<AuthorisedSignatoriesViewModel>
{
    public AuthorisedSignatoriesViewModelValidator()
    {
        var currentYear = DateTime.Now.Year; 
        
        RuleFor(x => x.AuthorisedSignatory1)
            .NotEmpty()
            .WithMessage("Enter authorised signatory")
            .MaximumLength(150)
            .WithMessage("Authorised signatory cannot exceed 150 characters");

        RuleFor(x => x.AuthorisedSignatory1EmailAddress)
            .NotEmpty()
            .WithMessage("Enter email address")
            .EmailAddress()
            .WithMessage(@"Enter an email address in the correct format, like name@example.com")
            .NotValidEmailAddress()
            .MaximumLength(150)
            .WithMessage("Email Address cannot exceed 150 characters");

        RuleFor(x => x.CompaniesDateOfAppointmentDay)
            .NotNull()
            .WithMessage("Enter a day for Company's Date of Appointment to the project");

        RuleFor(x => x.CompaniesDateOfAppointmentMonth)
            .NotNull()
            .WithMessage("Enter a month for Company's Date of Appointment to the project")
            .InclusiveBetween(1, 12)
            .When(x => x.CompaniesDateOfAppointmentMonth != null, ApplyConditionTo.CurrentValidator)
            .WithMessage("Please provide a valid month for Company's Date of Appointment to the project");

        RuleFor(x => x.CompaniesDateOfAppointmentYear)
            .NotNull()
            .WithMessage("Enter a year for Company's Date of Appointment to the project")
            .LessThanOrEqualTo(currentYear)
            .When(x => x.CompaniesDateOfAppointmentYear != null, ApplyConditionTo.CurrentValidator)
            .WithMessage($"Please provide a year no later than {currentYear}");

        RuleFor(x => new { x.CompaniesDateOfAppointmentDay, x.CompaniesDateOfAppointmentMonth, x.CompaniesDateOfAppointmentYear })
            .Must(x => BeAValidDayInTheMonth(x.CompaniesDateOfAppointmentDay, x.CompaniesDateOfAppointmentMonth, x.CompaniesDateOfAppointmentYear))
            .When(x => x.CompaniesDateOfAppointmentDay != null || x.CompaniesDateOfAppointmentMonth != null || x.CompaniesDateOfAppointmentYear != null)
            .WithName("CompaniesDateOfAppointmentDay")
            .WithMessage("Enter a valid day for Company's Date of Appointment to the project, for the entered month")
            .Must(x => BeInThePast(x.CompaniesDateOfAppointmentDay, x.CompaniesDateOfAppointmentMonth, x.CompaniesDateOfAppointmentYear))
            .When(x => x.CompaniesDateOfAppointmentDay != null || x.CompaniesDateOfAppointmentMonth!= null || x.CompaniesDateOfAppointmentYear != null) 
            .WithName("CompaniesDateOfAppointmentYear")
            .WithMessage("Company's Date of Appointment to the project cannot be in the future");
    }

    private bool BeAValidDayInTheMonth(int? day, int? month, int? year)
    {
        var currentYear = DateTime.Today.Year;

        if (month is null or < 1 or > 12) return false;
        if (year is null || year < 1 || year > currentYear) return false;

        return day <= DateTime.DaysInMonth(year.Value, month.Value);
    }

    private bool BeInThePast(int? day, int? month, int? year)
    {
        var currentYear = DateTime.Now.Year;

        if (day is null or < 1 or > 31) return false;
        if (month is null or < 1 or > 12) return false;
        if (year is null || year < 1 || year > currentYear) return false;
        if (day.HasValue && day.Value > DateTime.DaysInMonth(year.Value, month.Value)) return false;

        var date = new DateTime(year.Value, month.Value, day.Value);

        return date.Date <= DateTime.Today;
    }
}
