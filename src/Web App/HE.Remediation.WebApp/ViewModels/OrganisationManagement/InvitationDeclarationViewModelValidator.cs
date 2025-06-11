using FluentValidation;
using HE.Remediation.WebApp.CustomPropertyValidators;

namespace HE.Remediation.WebApp.ViewModels.OrganisationManagement
{
    public class InvitationDeclarationViewModelValidator : AbstractValidator<InvitationDeclarationViewModel>
    {
        public InvitationDeclarationViewModelValidator()
        {
            RuleFor(x => x.IsAdminResponsibilityConfirmed)
            .Equal(true)
            .WithMessage("Tick declaration");
        }
    }
}
