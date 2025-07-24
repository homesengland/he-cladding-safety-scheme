using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.ClosingReport;

public class DeclarationViewModelValidator : AbstractValidator<DeclarationViewModel>
{
    public DeclarationViewModelValidator()
    {
        RuleFor(x => x.DateOfCompletion)
                .NotNull()
                .WithMessage("Enter a valid Date of practical completion in a DD MM YYYY format")
                .Must(BeAValidDate)
                .WithMessage("Enter a valid Date of practical completion in a DD MM YYYY format")
                .GreaterThan(x => x.ApplicationCreationDate)
                .WithMessage("The date of practical completion must be after the application creation date")
                .Must(NotInTheFuture)
                .WithMessage("The date of practical completion must not be in the future");

        RuleFor(x => x.FraewRiskToLifeReduced)
            .Must(x => x == true)
            .WithMessage("You must confirm that your Fire Risk Assessment shows that the risk to life has been reduced");
        
        RuleFor(x => x.GrantFundingObligations)
           .Must(x => x == true)
           .WithMessage("You must confirm that you have met the obligations of the grant funding agreement");

        RuleFor(x => x.DischargedObligations)
            .Must(x => x == true)
            .WithMessage("You must confirm that you understand obligations have been discharged");
    }

    private bool BeAValidDate(DateTime? date)
    {
        return date >= new DateTime(2020, 01, 01);
    }

    private bool NotInTheFuture(DateTime? date)
    {
        return date.HasValue ? date.Value.Date <= DateTime.Now.Date : true;
    }
}
