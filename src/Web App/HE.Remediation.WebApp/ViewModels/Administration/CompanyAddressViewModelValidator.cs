using FluentValidation;
using HE.Remediation.WebApp.CustomPropertyValidators;
using HE.Remediation.WebApp.ViewModels.ResponsibleEntities;

namespace HE.Remediation.WebApp.ViewModels.Administration;

public class CompanyAddressViewModelValidator : AbstractValidator<CompanyAddressViewModel>
{
    public CompanyAddressViewModelValidator()
    {
        RuleFor(x => x).SetValidator(new AddressViewModelValidator());
    }
}