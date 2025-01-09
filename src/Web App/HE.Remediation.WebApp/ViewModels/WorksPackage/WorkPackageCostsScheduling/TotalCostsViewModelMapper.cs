using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.TotalCosts;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageCostsScheduling;

public class TotalCostsViewModelMapper : Profile
{
    public TotalCostsViewModelMapper()
    {
        CreateMap<GetTotalCostsResponse, TotalCostsViewModel>();
    }
}