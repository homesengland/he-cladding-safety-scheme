using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.Application
{
    public class RemoveAccessViewModelValidator : AbstractValidator<RemoveAccessViewModel>
    {
        public RemoveAccessViewModelValidator()
        {
            RuleFor(x => x.TeamMemberId)
                .NotEmpty()
                .WithMessage("TeamMemberId must be included");
        }
    }
}
