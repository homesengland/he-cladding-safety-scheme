using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.Costs.Overview;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageCostsScheduling;

public class CostsViewModelMapper : Profile
{
    public CostsViewModelMapper()
    {
        CreateMap<GetCostsResponse, CostsViewModel>();
    }
}