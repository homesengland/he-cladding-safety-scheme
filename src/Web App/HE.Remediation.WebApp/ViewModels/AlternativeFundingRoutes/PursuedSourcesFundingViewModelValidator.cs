using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.AlternativeFundingRoutes
{
    public class PursuedSourcesFundingViewModelValidator : AbstractValidator<PursuedSourcesFundingViewModel>
    {
        public PursuedSourcesFundingViewModelValidator()
        {
            RuleFor(x => x.PursuedSourcesFunding)
                .NotNull()
                .WithMessage("Select an option");
        }
    }
}