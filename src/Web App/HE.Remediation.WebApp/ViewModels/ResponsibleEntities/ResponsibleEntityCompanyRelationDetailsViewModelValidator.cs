using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities
{
    public class ResponsibleEntityCompanyRelationDetailsViewModelValidator : AbstractValidator<ResponsibleEntityCompanyRelationDetailsViewModel>
    {
        public ResponsibleEntityCompanyRelationDetailsViewModelValidator()
        {
            RuleFor(x => x.ResponsibleEntityRelation)
                .NotEmpty()
                .WithMessage("Please select an option.");
        }
    }
}
