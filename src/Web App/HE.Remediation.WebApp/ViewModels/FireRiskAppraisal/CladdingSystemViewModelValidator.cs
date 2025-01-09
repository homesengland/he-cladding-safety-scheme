using FluentValidation;
using HE.Remediation.Core.Enums;
using HE.Remediation.WebApp.Constants;

namespace HE.Remediation.WebApp.ViewModels.FireRiskAppraisal
{
    public class CladdingSystemViewModelValidator : AbstractValidator<CladdingSystemViewModel>
    {
        public CladdingSystemViewModelValidator()
        {
            RuleFor(x => x.CladdingSystemTypeId)
                .NotNull()
                .NotEqual(0)
                .WithMessage("Select an option - Cladding System Type");

            RuleFor(x => x.InsulationTypeId)
                .NotNull()
                .NotEqual(0)
                .WithMessage("Select an option - Insulation Type");

            RuleFor(x => x.InsulationManufacturerId)
                .NotNull()
                .NotEqual(0)
                .WithMessage("Select an option - Insulation Manufacturer");

            RuleFor(x => x.CladdingManufacturerId)
                .NotNull()
                .NotEqual(0)
                .WithMessage("Select an option - Cladding Manufacturer");

            RuleFor(x => x.OtherInsulationManufacturer)
                .NotNull()
                .When(y => y.InsulationManufacturerId == CladdingManufacturerConstants.Other)
                .WithMessage("Enter other insulation manufacturer name")
                .MaximumLength(256)
                .WithMessage("The maximum number of characters allowed is 256");

            RuleFor(x => x.OtherCladdingManufacturer)
                .NotNull()
                .When(y => y.CladdingManufacturerId == CladdingManufacturerConstants.Other)
                .WithMessage("Enter other cladding manufacturer name")
                .MaximumLength(256)
                .WithMessage("The maximum number of characters allowed is 256");

            RuleFor(x => x.OtherCladdingType)
                .NotEmpty()
                .When(x => x.CladdingSystemTypeId == (int)ECladdingSystemType.Other, ApplyConditionTo.CurrentValidator)
                .WithMessage("Enter other cladding system type")
                .MaximumLength(256)
                .WithMessage("The maximum number of characters allowed is 256");

            RuleFor(x => x.OtherInsulationType)
                .NotEmpty()
                .When(x => x.InsulationTypeId == (int)EInsulationType.Other, ApplyConditionTo.CurrentValidator)
                .WithMessage("Enter other insulation type")
                .MaximumLength(256)
                .WithMessage("The maximum number of characters allowed is 256");
        }
    }
}