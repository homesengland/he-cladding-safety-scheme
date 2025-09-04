using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.AlternativeFundingRoutes;

namespace HE.Remediation.WebApp.ViewModels.AlternativeFundingRoutes;

public class CostRecoveryViewModelMapper : Profile
{
    public CostRecoveryViewModelMapper()
    {
        CreateMap<GetCostRecoveryResponse, CostRecoveryViewModel>();
        CreateMap<CostRecoveryViewModel, SetCostRecoveryRequest>();
    }
}