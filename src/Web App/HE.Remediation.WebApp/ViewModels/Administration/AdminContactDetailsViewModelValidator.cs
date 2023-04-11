using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.Administration
{
    public class AdminContactDetailsViewModelValidator : AbstractValidator<AdminContactDetailsViewModel>
    {
        public AdminContactDetailsViewModelValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("Please enter a First name");
            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("Please enter a Last name");
            RuleFor(x => x.ContactNumber)
                .NotEmpty()
                .WithMessage("Please enter a Contact number");
        }
    }
}
