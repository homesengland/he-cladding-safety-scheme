using FluentValidation;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.KeyDates.BuildingControl;

public class UploadBuildingControlViewModelValidator : AbstractValidator<UploadBuildingControlViewModel>
{
    public UploadBuildingControlViewModelValidator()
    {
        When(x => x.SubmitAction == ESubmitAction.Upload, () =>
        {
            RuleFor(x => x.File)
                .NotNull()
                .WithMessage("Select a file")
                .Must(x => x?.Length <= 50 * 1024 * 1024)
                .WithMessage("File must be smaller than 50mb");
        });

        When(x => x.SubmitAction != ESubmitAction.Upload, () =>
        {
            RuleFor(x => x.AddedFiles)
                .NotEmpty()
                .OverridePropertyName(nameof(UploadBuildingControlViewModel.File))
                .WithMessage("Provide at least 1 document to support the decision");
        });
    }
}