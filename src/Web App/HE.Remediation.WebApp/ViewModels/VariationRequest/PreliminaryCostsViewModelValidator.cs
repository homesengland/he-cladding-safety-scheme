using FluentValidation;
using HE.Remediation.WebApp.Extensions;

namespace HE.Remediation.WebApp.ViewModels.VariationRequest
{
    public class PreliminaryCostsViewModelValidator : AbstractValidator<PreliminaryCostsViewModel>
    {
        public PreliminaryCostsViewModelValidator()
        {
            RuleFor(x => x.VariationMainContractorPreliminariesAmountText)
                .Must(x => decimal.TryParse(x, out _))
                    .When(x => !string.IsNullOrWhiteSpace(x.VariationMainContractorPreliminariesAmountText))
                    .WithMessage("Main contractor additional costs must be a number")
                .Must(x => x.NotExceedMaximumDigits())
                    .WithMessage(x => $"Main contractor additional costs must not have more than {FluentValidationExtensions.MaxNumberOfDigitsInNumber} digits")
                .DependentRules(() =>
                {
                    RuleFor(x => x.VariationMainContractorPreliminariesAmount)
                        .GreaterThanOrEqualTo(0M)
                            .OverridePropertyName(nameof(PreliminaryCostsViewModel.VariationMainContractorPreliminariesAmountText))
                            .WithMessage(x => "Main contractor additional costs must be a positive amount")
                        .Must(x => x.HaveNoDecimalsInAmount())
                            .OverridePropertyName(nameof(PreliminaryCostsViewModel.VariationMainContractorPreliminariesAmountText))
                            .WithMessage(x => "Main contractor additional costs must be a whole number");
                });

            RuleFor(x => x.VariationMainContractorPreliminariesDescription)
                .Empty()
                    .When(x => x.VariationMainContractorPreliminariesAmount == 0 || x.VariationMainContractorPreliminariesAmount is null &&
                        string.IsNullOrEmpty(x.VariationMainContractorPreliminariesAmountText))
                    .WithMessage("Remove information about the changes to main contractor costs when no change to cost is entered")
                .NotEmpty()
                    .When(x => !string.IsNullOrEmpty(x.VariationMainContractorPreliminariesAmountText) & x.VariationMainContractorPreliminariesAmount != 0, ApplyConditionTo.CurrentValidator)
                    .WithMessage("Enter information about the changes to main contractor costs")
                .MaximumLength(500)
                    .WithMessage("Information about the changes to main contractor costs cannot exceed 500 characters");



            RuleFor(x => x.VariationAccessAmountText)
                .Must(x => decimal.TryParse(x, out _))
                    .When(x => !string.IsNullOrWhiteSpace(x.VariationAccessAmountText))
                    .WithMessage("Access additional cost must be a number")
                .Must(x => x.NotExceedMaximumDigits())
                    .WithMessage(x => $"Access additional costs must not have more than {FluentValidationExtensions.MaxNumberOfDigitsInNumber} digits")
                .DependentRules(() =>
                {
                    RuleFor(x => x.VariationAccessAmount)
                        .GreaterThanOrEqualTo(0M)
                            .OverridePropertyName(nameof(PreliminaryCostsViewModel.VariationAccessAmountText))
                            .WithMessage(x => "Access additional cost must be a positive amount")
                        .Must(x => x.HaveNoDecimalsInAmount())
                            .OverridePropertyName(nameof(PreliminaryCostsViewModel.VariationAccessAmountText))
                            .WithMessage(x => "Access additional cost must be a whole number");
                });

            RuleFor(x => x.VariationAccessDescription)
                .Empty()
                    .When(x => x.VariationAccessAmount == 0 || x.VariationAccessAmount is null &&
                        string.IsNullOrEmpty(x.VariationAccessAmountText))
                    .WithMessage("Remove information about the changes to access costs when no change to cost is entered")
                .NotEmpty()
                    .When(x => !string.IsNullOrEmpty(x.VariationAccessAmountText) & x.VariationAccessAmount != 0, ApplyConditionTo.CurrentValidator)
                    .WithMessage("Enter information about the changes to access costs")
                .MaximumLength(500)
                    .WithMessage("Information about the changes to access cost cannot exceed 500 characters");



            RuleFor(x => x.VariationContractorContingenciesAmountText)
                .Must(x => decimal.TryParse(x, out _))
                    .When(x => !string.IsNullOrWhiteSpace(x.VariationContractorContingenciesAmountText))
                    .WithMessage("Contractor contingency additional cost must be a number")
                .Must(x => x.NotExceedMaximumDigits())
                    .WithMessage(x => $"Contractor contingency additional costs must not have more than {FluentValidationExtensions.MaxNumberOfDigitsInNumber} digits")
                .DependentRules(() =>
                {
                    RuleFor(x => x.VariationContractorContingenciesAmount)
                        .GreaterThanOrEqualTo(0M)
                            .OverridePropertyName(nameof(PreliminaryCostsViewModel.VariationContractorContingenciesAmountText))
                            .WithMessage(x => "Contractor contingency additional cost must be a positive amount")
                        .Must(x => x.HaveNoDecimalsInAmount())
                            .OverridePropertyName(nameof(PreliminaryCostsViewModel.VariationContractorContingenciesAmountText))
                            .WithMessage(x => "Contractor contingency additional cost must be a whole number");
                });

            RuleFor(x => x.VariationContractorContingenciesDescription)
                .Empty()
                    .When(x => x.VariationContractorContingenciesAmount == 0 || x.VariationContractorContingenciesAmount is null &&
                        string.IsNullOrEmpty(x.VariationContractorContingenciesAmountText))
                    .WithMessage("Remove information about the changes to contractor contingency costs when no change to cost is entered")
                .NotEmpty()
                    .When(x => !string.IsNullOrEmpty(x.VariationContractorContingenciesAmountText) & x.VariationContractorContingenciesAmount != 0, ApplyConditionTo.CurrentValidator)
                    .WithMessage("Enter information about the changes to contractor contingency costs")
                .MaximumLength(500)
                    .WithMessage("Information about the changes to contractor contingency cost cannot exceed 500 characters");



            RuleFor(x => x.VariationOverheadsAndProfitAmountText)
                .Must(x => decimal.TryParse(x, out _))
                    .When(x => !string.IsNullOrWhiteSpace(x.VariationOverheadsAndProfitAmountText))
                    .WithMessage("Contractor overheads additional cost must be a number")
                .Must(x => x.NotExceedMaximumDigits())
                    .WithMessage(x => $"Contractor overheads additional costs must not have more than {FluentValidationExtensions.MaxNumberOfDigitsInNumber} digits")
                .DependentRules(() =>
                {
                    RuleFor(x => x.VariationOverheadsAndProfitAmount)
                        .GreaterThanOrEqualTo(0M)
                            .OverridePropertyName(nameof(PreliminaryCostsViewModel.VariationOverheadsAndProfitAmountText))
                            .WithMessage(x => "Contractor overheads additional cost must be a positive amount")
                        .Must(x => x.HaveNoDecimalsInAmount())
                            .OverridePropertyName(nameof(PreliminaryCostsViewModel.VariationOverheadsAndProfitAmountText))
                            .WithMessage(x => "Contractor overheads additional cost must be a whole number");
                });

            RuleFor(x => x.VariationOverheadsAndProfitDescription)
                .Empty()
                    .When(x => x.VariationOverheadsAndProfitAmount == 0 || x.VariationOverheadsAndProfitAmount is null &&
                        string.IsNullOrEmpty(x.VariationOverheadsAndProfitAmountText))
                    .WithMessage("Remove information about the changes to contractor overheads costs when no change to cost is entered")
                .NotEmpty()
                    .When(x => !string.IsNullOrEmpty(x.VariationOverheadsAndProfitAmountText) & x.VariationOverheadsAndProfitAmount != 0, ApplyConditionTo.CurrentValidator)
                    .WithMessage("Enter information about the changes to contractor overheads costs")
                .MaximumLength(500)
                    .WithMessage("Information about the changes to contractor overheads cost cannot exceed 500 characters");
        }
    }
}
