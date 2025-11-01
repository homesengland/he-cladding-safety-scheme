using FluentValidation;

using HE.Remediation.Core.Enums;
using HE.Remediation.WebApp.ViewModels.ScheduleOfWorks.Shared;

namespace HE.Remediation.WebApp.ViewModels.ScheduleOfWorks;

public class BuildingControlApprovalViewModelValidator : AbstractValidator<BuildingControlApprovalViewModel>
{
    public BuildingControlApprovalViewModelValidator()
    {
        RuleFor(x => x.IsBuildingControlApprovalApplied)
            .NotNull()
            .WithMessage("Select an option");
    }
}