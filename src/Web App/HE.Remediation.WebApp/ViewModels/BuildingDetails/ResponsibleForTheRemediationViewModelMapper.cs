using AutoMapper;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingRemediation.GetBuildingRemediationResponsibilityReason;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingRemediation.SetBuildingRemediationResponsibilityReason;

namespace HE.Remediation.WebApp.ViewModels.BuildingDetails;

public class ResponsibleForTheRemediationViewModelMapper : Profile
{
    public ResponsibleForTheRemediationViewModelMapper()
    {
        CreateMap<ResponsibleForTheRemediationViewModel, SetBuildingRemediationResponsibilityReasonRequest>();

        CreateMap<GetBuildingRemediationResponsibilityResponse, ResponsibleForTheRemediationViewModel>();
    }
}