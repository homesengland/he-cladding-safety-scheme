using FluentValidation;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.ScheduleOfWorks;

public class UploadLeaseholderEngagementViewModelValidator : AbstractValidator<UploadLeaseholderEngagementViewModel>
{
    private const int FileSizeLimit = 50 * 1024 * 1024; // 50mb

    public UploadLeaseholderEngagementViewModelValidator()
    {
        When(x => x.SubmitAction == ESubmitAction.Upload, () =>
        {
            RuleFor(x => x.File)
                .NotEmpty()
                .WithMessage("File is required")
                .Must(x => x?.Length <= FileSizeLimit)
                .WithMessage("File is larger than 50MB");
        });

        When(x => x.SubmitAction == ESubmitAction.Continue, () =>
        {
            RuleFor(x => x.AddedFiles)
                .Must(x => x?.Any() == true)
                .OverridePropertyName(nameof(UploadWorksContractViewModel.File))
                .WithMessage("File is required");
        });
    }
}