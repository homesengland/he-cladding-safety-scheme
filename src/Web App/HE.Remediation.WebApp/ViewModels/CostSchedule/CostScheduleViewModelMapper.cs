using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.CostSchedule;

namespace HE.Remediation.WebApp.ViewModels.CostSchedule;

public class CostScheduleViewModelMapper : Profile
{
    public CostScheduleViewModelMapper()
    {
        CreateMap<GetCostScheduleResponse, CostScheduleViewModel>();
    }
}