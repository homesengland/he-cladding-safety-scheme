using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.ProjectPlan.Upload
{
    public class UploadProjectPlanViewModelValidator : AbstractValidator<UploadProjectPlanViewModel>
    {
        private const long FileSizeLimit = 50 * 1024 * 1024; // 50MB

        public UploadProjectPlanViewModelValidator()
        {
            When(x => x.File is not null, () =>
            {
                RuleFor(x => x.File)
                    .NotEmpty()
                    .WithMessage("File upload required")
                    .Must(x => x?.Length <= FileSizeLimit)
                    .WithMessage("File exceeds 50mb limit. Please upload an alternate file.");
            });
        }
    }
}
