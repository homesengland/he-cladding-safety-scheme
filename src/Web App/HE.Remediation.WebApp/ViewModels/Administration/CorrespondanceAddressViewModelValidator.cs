using FluentValidation;
using HE.Remediation.WebApp.ViewModels.ResponsibleEntities;

namespace HE.Remediation.WebApp.ViewModels.Administration
{
    public class CorrespondanceAddressViewModelValidator: AbstractValidator<CorrespondanceAddressViewModel>
    {
        public CorrespondanceAddressViewModelValidator()
        {
            RuleFor(x => x).SetValidator(new AddressViewModelValidator());
        }
    }
}
