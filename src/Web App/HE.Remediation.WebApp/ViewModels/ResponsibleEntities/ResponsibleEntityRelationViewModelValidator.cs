using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities
{
    public class ResponsibleEntityRelationViewModelValidator : AbstractValidator<ResponsibleEntityRelationViewModel>
    {
        public ResponsibleEntityRelationViewModelValidator()
        {
            RuleFor(x => x.ResponsibleEntityRelation)
                .NotEmpty()
                .WithMessage("Select an option");
        }
    }
}
