using AutoMapper;

using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.BuildingSafetyRegulator.Get;
using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.BuildingSafetyRegulator.Set;

namespace HE.Remediation.WebApp.ViewModels.ScheduleOfWorks;

public class BuildingControlApprovalViewModelMapper : Profile
{
    public BuildingControlApprovalViewModelMapper()
    {
        CreateMap<GetBuildingControlApprovalResponse, BuildingControlApprovalViewModel>();
        CreateMap<BuildingControlApprovalViewModel, SetBuildingControlApprovalRequest>();
    }
}