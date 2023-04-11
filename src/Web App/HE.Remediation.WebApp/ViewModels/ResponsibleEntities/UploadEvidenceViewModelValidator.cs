using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities;

public class UploadEvidenceViewModelValidator : AbstractValidator<UploadEvidenceViewModel>
{
    private const int MaxSize = 20971520; // 20 * 1024 * 1024, 20mb

    public UploadEvidenceViewModelValidator()
    {
        RuleFor(x => x.File)
            .NotEmpty()
            .WithMessage("File is required")
            .Must(x => x?.Length <= MaxSize)
            .WithMessage("File is larger than 20mb");
    }
}