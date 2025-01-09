using FluentValidation;
using HE.Remediation.WebApp.Extensions;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageCostsScheduling;

public class PreliminaryCostsViewModelValidator : AbstractValidator<PreliminaryCostsViewModel>
{
    public PreliminaryCostsViewModelValidator()
    {
        RuleFor(x => x.MainContractorPreliminariesAmountText)
            .Must(x => string.IsNullOrEmpty(x) || decimal.TryParse(x, out _))
            .WithMessage("Main contractor's preliminaries costs must be a number")
            .NotEmpty()
            .WithMessage("Enter main contractor's preliminaries costs")
            .Must(x => x.NotExceedMaximumDigits())
            .WithMessage(x => $"Main contractor's preliminaries costs must not have more than {FluentValidationExtensions.MaxNumberOfDigitsInNumber} digits")
            .DependentRules(() =>
            {
                RuleFor(x => x.MainContractorPreliminariesAmount)
                    .GreaterThanOrEqualTo(0M)
                    .OverridePropertyName(nameof(PreliminaryCostsViewModel.MainContractorPreliminariesAmountText))
                    .WithMessage(x => $"Main contractor's preliminaries costs must be a positive amount")
                    .Must(x => x.HaveNoDecimalsInAmount())
                    .OverridePropertyName(nameof(PreliminaryCostsViewModel.MainContractorPreliminariesAmountText))
                    .WithMessage(x => "Main contractor's preliminaries costs must be a whole number");
            });

        RuleFor(x => x.MainContractorPreliminariesDescription)
            .NotEmpty()
            .WithMessage("Enter information about main contractor's preliminaries costs")
            .MaximumLength(500)
            .WithMessage("Information about main contractor's preliminaries costs cannot exceed 500 characters");

        RuleFor(x => x.AccessAmountText)
            .Must(x => string.IsNullOrEmpty(x) || decimal.TryParse(x, out _))
            .WithMessage("Access costs must be a number")
            .NotEmpty()
            .WithMessage("Enter access costs")
            .Must(x => x.NotExceedMaximumDigits())
            .WithMessage(x => $"Access costs must not have more than {FluentValidationExtensions.MaxNumberOfDigitsInNumber} digits")
            .DependentRules(() =>
            {
                RuleFor(x => x.AccessAmount)
                    .GreaterThanOrEqualTo(0M)
                    .OverridePropertyName(nameof(PreliminaryCostsViewModel.AccessAmountText))
                    .WithMessage(x => "Access costs must be a positive amount")
                    .Must(x => x.HaveNoDecimalsInAmount())
                    .OverridePropertyName(nameof(PreliminaryCostsViewModel.AccessAmountText))
                    .WithMessage(x => "Access costs must be a whole number");
            });

        RuleFor(x => x.AccessDescription)
            .NotEmpty()
            .WithMessage("Enter information about access costs")
            .MaximumLength(500)
            .WithMessage("Information about access costs cannot exceed 500 characters");

        RuleFor(x => x.MainContractorOverheadAmountText)
            .Must(x => string.IsNullOrEmpty(x) || decimal.TryParse(x, out _))
            .WithMessage("Main contractor's overheads and profits must be a number")
            .NotEmpty()
            .WithMessage("Enter main contractor's overheads and profits")
            .Must(x => x.NotExceedMaximumDigits())
            .WithMessage(x => $"Main contractor's overheads and profits must not have more than {FluentValidationExtensions.MaxNumberOfDigitsInNumber} digits")
            .DependentRules(() =>
            {
                RuleFor(x => x.MainContractorOverheadAmount)
                    .GreaterThanOrEqualTo(0M)
                    .OverridePropertyName(nameof(PreliminaryCostsViewModel.MainContractorOverheadAmountText))
                    .WithMessage(x => "Main contractor's overheads and profits must be a positive amount")
                    .Must(x => x.HaveNoDecimalsInAmount())
                    .OverridePropertyName(nameof(PreliminaryCostsViewModel.MainContractorOverheadAmountText))
                    .WithMessage(x => "Main contractor's overheads and profits must be a whole number");
            });

        RuleFor(x => x.MainContractorOverheadDescription)
            .NotEmpty()
            .WithMessage("Enter information about main contractor's overheads and profits")
            .MaximumLength(500)
            .WithMessage("Information about main contractor's overheads and profits cannot exceed 500 characters");

        RuleFor(x => x.ContractorContingenciesAmountText)
            .Must(x => string.IsNullOrEmpty(x) || decimal.TryParse(x, out _))
            .WithMessage("Contractor's contingencies must be a number")
            .NotEmpty()
            .WithMessage("Enter contractor's contingencies")
            .Must(x => x.NotExceedMaximumDigits())
            .WithMessage(x => $"Contractor's contingencies must not have more than {FluentValidationExtensions.MaxNumberOfDigitsInNumber} digits")
            .DependentRules(() =>
            {
                RuleFor(x => x.ContractorContingenciesAmount)
                    .GreaterThanOrEqualTo(0M)
                    .OverridePropertyName(nameof(PreliminaryCostsViewModel.ContractorContingenciesAmountText))
                    .WithMessage(x => "Contractor's contingencies must be a positive amount")
                    .Must(x => x.HaveNoDecimalsInAmount())
                    .OverridePropertyName(nameof(PreliminaryCostsViewModel.ContractorContingenciesAmountText))
                    .WithMessage(x => "Contractor's contingencies must be a whole number");
            });

        RuleFor(x => x.ContractorContingenciesDescription)
            .NotEmpty()
            .WithMessage("Enter information about contractor's contingencies")
            .MaximumLength(500)
            .WithMessage("Information about contractor's contingencies cannot exceed 500 characters");
    }
}