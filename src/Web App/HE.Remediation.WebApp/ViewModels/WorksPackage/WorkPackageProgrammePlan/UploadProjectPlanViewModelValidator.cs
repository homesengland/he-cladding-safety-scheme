using FluentValidation;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageProgrammePlan;

public class UploadProjectPlanViewModelValidator : AbstractValidator<UploadProjectPlanViewModel>
{
    private const int MaxSizeMb = 100;
    private const int MaxSize = MaxSizeMb * 1024 * 1024;

    public UploadProjectPlanViewModelValidator()
    {
        When(x => x.SubmitAction == ESubmitAction.Upload, () =>
        {
            RuleFor(x => x.File)
                .NotEmpty()
                .WithMessage("File is required")
                .Must(x => x?.Length <= MaxSize)
                .WithMessage($"File is larger than {MaxSizeMb}mb");
        });

        When(x => x.SubmitAction == ESubmitAction.Continue, () =>
        {
            RuleFor(x => x.AddedFiles)
                .NotEmpty()
                .OverridePropertyName(nameof(UploadProjectPlanViewModel.File))
                .WithMessage("File is required");
        });
    }
}