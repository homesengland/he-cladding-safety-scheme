using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.ClosingReport;

public class NeedVariationsViewModelValidator : AbstractValidator<NeedVariationsViewModel>
{
    public NeedVariationsViewModelValidator()
    {

        RuleFor(x => x.NeedVariations)
            .Must(x => x != null)
            .WithMessage("You must confirm you do not need any variations and are ready to continue");
      
    }
}
