using FluentValidation;
using HE.Remediation.Core.Enums;
using HE.Remediation.WebApp.ViewModels.Leaseholder;

namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities.RightToManage;

public class RightToManageEvidenceViewModelValidator : AbstractValidator<RightToManageEvidenceViewModel>
{
    private const int MaxSize = 100 * 1024 * 1024; // 100mb

    public RightToManageEvidenceViewModelValidator()
    {
        When(x => x.SubmitAction == ESubmitAction.Upload, () =>
        {
            RuleFor(x => x.File)
                .NotEmpty()
                .WithMessage("File is required")
                .Must(x => x?.Length <= MaxSize)
                .WithMessage("File exceeds 100mb limit. Please upload an alternate file");
        });

        When(x => x.SubmitAction == ESubmitAction.Continue, () =>
        {
            RuleFor(x => x.AddedFiles)
                .NotEmpty()
                .OverridePropertyName(nameof(LeaseHolderEvidenceViewModel.File))
                .WithMessage("File is required");
        });
    }
}