using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.VariationRequest.CostProfile.Get;

namespace HE.Remediation.WebApp.ViewModels.VariationRequest;

public class CostProfileItemViewModelMapper : Profile
{
    public CostProfileItemViewModelMapper()
    {
        CreateMap<GetCostProfileResultItem, CostProfileItemViewModel>();
    }
}
