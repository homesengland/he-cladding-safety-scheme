using FluentValidation;
using HE.Remediation.WebApp.CustomPropertyValidators;

namespace HE.Remediation.WebApp.ViewModels;

public class PostCodeResultsViewModelValidator: AbstractValidator<PostCodeResultsViewModel>
{
    public PostCodeResultsViewModelValidator()
    {        
        RuleFor(x => x.SelectedAddressId)
        .NotNull()
        .WithMessage("Please select an address")
        .Must(BeValidDropDownSelection)                
        .WithMessage("Please select an address");                
    }

    private bool BeValidDropDownSelection(string inputStr)
    {
        return ((inputStr != null) && (inputStr != "0"));
    }
}
