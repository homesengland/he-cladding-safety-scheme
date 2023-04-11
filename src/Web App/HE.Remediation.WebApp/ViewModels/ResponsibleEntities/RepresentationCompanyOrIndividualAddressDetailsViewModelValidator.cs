using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities;

public class RepresentationCompanyOrIndividualAddressDetailsViewModelValidator : AbstractValidator<RepresentationCompanyOrIndividualAddressDetailsViewModel>
{
    public RepresentationCompanyOrIndividualAddressDetailsViewModelValidator()
    {
        RuleFor(x => x).SetValidator(new AddressViewModelValidator());
    }
}