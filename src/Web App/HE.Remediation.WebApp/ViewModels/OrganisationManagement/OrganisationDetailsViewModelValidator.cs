using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.OrganisationManagement;

public class OrganisationDetailsViewModelValidator : AbstractValidator<OrganisationDetailsViewModel>
{
    public OrganisationDetailsViewModelValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Please enter a Name")
            .MaximumLength(150)
            .WithMessage("Name must be less than 150 characters")
            .Matches("^[a-zA-Z0-9 ]*$")
            .WithMessage("Name must contain only alphanumeric characters");

        RuleFor(x => x.RegistrationNumber)
            .NotEmpty()
            .WithMessage("Please enter a Registration Number")
            .MinimumLength(4)
            .MaximumLength(8)
            .WithMessage("Registration Number must be between 4 and 8 characters")
            .Matches("^[a-zA-Z0-9]*$")
            .WithMessage("Registration Number must contain only alphanumeric characters");
    }
}
