using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.ProjectSupport
{
    public class ProjectSupportViewModelValidator : AbstractValidator<ProjectSupportViewModel>
    {
        public ProjectSupportViewModelValidator()
        {
            RuleFor(x => x.RequiresSupport)
                .NotEmpty()
                .WithMessage("Please select whether you need support");
        }
    }
}