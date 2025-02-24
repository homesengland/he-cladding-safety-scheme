using AutoMapper;
using FluentValidation;

namespace HE.Remediation.WebApp.ViewModels.PaymentRequest;

public class CostsChangedViewModelValidator : AbstractValidator<CostsChangedViewModel>
{
    public CostsChangedViewModelValidator()
    {
        RuleFor(x => x.CostsChanged)
            .NotNull()
            .WithMessage("Select an option");        
    } 
}
