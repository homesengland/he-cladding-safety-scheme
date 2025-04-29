using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.Application
{
    public class SchemeSelectionViewModelValidator : AbstractValidator<SchemeSelectionViewModel>
    {
        public SchemeSelectionViewModelValidator()
        {
            RuleFor(x => x.SelectedSchemeId)
                .NotNull()
                .WithMessage("Select the right scheme for your building");
        }
    }
}
