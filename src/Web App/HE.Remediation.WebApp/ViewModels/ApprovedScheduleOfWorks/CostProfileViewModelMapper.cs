using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ApprovedScheduleOfWorks.CostProfile.Get;

namespace HE.Remediation.WebApp.ViewModels.ApprovedScheduleOfWorks;

public class CostProfileViewModelMapper : Profile
{
    public CostProfileViewModelMapper()
    {
        CreateMap<GetCostProfileResponse, CostProfileViewModel>();
    }
}
