using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.BuildingDetails
{
    public class NameOfDevelopmentViewModelValidator : AbstractValidator<NameOfDevelopmentViewModel>
    {
        public NameOfDevelopmentViewModelValidator()
        {
            RuleFor(x => x.NameOfDevelopment)
                .NotNull()
                .WithMessage("Enter a name for this development")
                .MaximumLength(150)
                .WithMessage("Development name cannot exceed 150 characters");

        }
    }
}
