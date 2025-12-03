using FluentValidation;

using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.FireRiskAssessment;

public class FraBuildingWorkTypeViewModelValidator : AbstractValidator<FraBuildingWorkTypeViewModel>
{
    public FraBuildingWorkTypeViewModelValidator()
    {
        RuleFor(x => x.FraBuildingWorkTypeId)
            .NotNull()
            .WithMessage("Select an option");
    }
}