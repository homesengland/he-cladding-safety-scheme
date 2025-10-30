using AutoMapper;

using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.ApprovalDateGateWayTwoApplication.Get;
using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.ApprovalDateGateWayTwoApplication.Set;

namespace HE.Remediation.WebApp.ViewModels.ScheduleOfWorks;

public class BuildingControlApprovalDateViewModelMapper : Profile
{
    public BuildingControlApprovalDateViewModelMapper()
    {
        CreateMap<GetApprovalDateResponse, BuildingControlApprovalDateViewModel>();
        CreateMap<BuildingControlApprovalDateViewModel, SetApprovalDateRequest>();
    }
}