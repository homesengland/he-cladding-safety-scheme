using AutoMapper;
using HE.Remediation.Core.Data.StoredProcedureResults.ScheduleOfWorks;

namespace HE.Remediation.WebApp.ViewModels.ApprovedScheduleOfWorks;

public class CostProfileItemViewModelMapper : Profile
{
    public CostProfileItemViewModelMapper()
    {
        CreateMap<CostsProfileResult, CostProfileItemViewModel>();
    }
}
