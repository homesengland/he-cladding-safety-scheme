using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities
{
    public class FreeholderCompanyOrIndividualViewModelValidator : AbstractValidator<FreeholderCompanyOrIndividualViewModel>
    {
        public FreeholderCompanyOrIndividualViewModelValidator()
        {
            RuleFor(x => x.ReponsibleEntityType)
                .NotNull()
                .WithMessage("Select an option");
        }
    }
}
