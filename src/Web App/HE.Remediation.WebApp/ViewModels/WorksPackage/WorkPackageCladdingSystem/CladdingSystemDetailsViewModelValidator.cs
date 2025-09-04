using FluentValidation;
using HE.Remediation.WebApp.Constants;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageCladdingSystem;

public class CladdingSystemDetailsViewModelValidator : AbstractValidator<CladdingSystemDetailsViewModel>
{
    public CladdingSystemDetailsViewModelValidator()
    {
        RuleFor(x => x.ReplacementCladdingSystemTypeId)
            .NotNull()
            .NotEqual(0)
            .WithMessage("Select the cladding type");

        RuleFor(x => x.ReplacementOtherCladdingSystemType)
            .NotNull()
            .When(y => y.ReplacementCladdingSystemTypeId == CladdingSystemTypeConstants.Other)
            .WithMessage("Enter the cladding type")
            .MaximumLength(256)
            .WithMessage("The maximum number of characters allowed is 256");

        RuleFor(x => x.ReplacementCladdingManufacturerId)
            .NotNull()
            .NotEqual(0)
            .WithMessage("Select the cladding manufacturer");

        RuleFor(x => x.ReplacementOtherCladdingManufacturer)
            .NotNull()
            .When(y => y.ReplacementCladdingManufacturerId == CladdingManufacturerConstants.Other)
            .WithMessage("Enter the cladding manufacturer")
            .MaximumLength(256)
            .WithMessage("The maximum number of characters allowed is 256");

        RuleFor(x => x.ReplacementInsulationTypeId)
            .NotNull()
            .NotEqual(0)
            .WithMessage("Select the insulation material");

        RuleFor(x => x.ReplacementOtherInsulationType)
            .NotNull()
            .When(y => y.ReplacementInsulationTypeId == CladdingInsulationTypeConstants.Other)
            .WithMessage("Enter the insulation material")
            .MaximumLength(256)
            .WithMessage("The maximum number of characters allowed is 256");

        RuleFor(x => x.ReplacementInsulationManufacturerId)
            .NotNull()
            .NotEqual(0)
            .WithMessage("Select the insulation manufacturer");

        RuleFor(x => x.ReplacementOtherInsulationManufacturer)
            .NotNull()
            .When(y => y.ReplacementInsulationManufacturerId == CladdingManufacturerConstants.Other)
            .WithMessage("Enter the insulation manufacturer")
            .MaximumLength(256)
            .WithMessage("The maximum number of characters allowed is 256");

        RuleFor(x => x.CladdingSystemArea)
            .NotEmpty()
            .WithMessage("Enter the Cladding Area in square metres")
            .GreaterThan(0)
            .WithMessage("Enter a positive whole number for the Cladding Area");
       


    }
}
