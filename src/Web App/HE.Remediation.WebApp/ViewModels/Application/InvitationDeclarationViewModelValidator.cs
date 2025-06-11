using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.Application
{
    public class InvitationDeclarationViewModelValidator : AbstractValidator<InvitationDeclarationViewModel>
    {
        public InvitationDeclarationViewModelValidator()
        {
            RuleFor(x => x.IsDeclarationConfimed)
            .Equal(true)
            .WithMessage("Tick declaration");
        }
    }
}
