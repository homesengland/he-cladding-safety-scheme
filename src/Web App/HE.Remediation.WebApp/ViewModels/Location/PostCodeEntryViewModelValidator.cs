using FluentValidation;
using HE.Remediation.WebApp.CustomPropertyValidators;
using HE.Remediation.WebApp.ViewModels.BuildingDetails;

namespace HE.Remediation.WebApp.ViewModels.Location;

public class PostCodeEntryViewModelValidator: AbstractValidator<PostCodeEntryViewModel>
{
    public PostCodeEntryViewModelValidator()
    {
        RuleFor(x => x.PostCode)
        .NotEmpty()
        .WithMessage("Please enter a postcode")
        .ValidGovPostcode();                
    }
}
