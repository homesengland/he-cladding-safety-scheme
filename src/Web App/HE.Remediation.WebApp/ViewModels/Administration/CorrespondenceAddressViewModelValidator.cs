using FluentValidation;
using HE.Remediation.WebApp.ViewModels.ResponsibleEntities;

namespace HE.Remediation.WebApp.ViewModels.Administration
{
    public class CorrespondenceAddressViewModelValidator: AbstractValidator<CorrespondenceAddressViewModel>
    {
        public CorrespondenceAddressViewModelValidator()
        {
            RuleFor(x => x).SetValidator(new AddressViewModelValidator());
        }
    }
}
