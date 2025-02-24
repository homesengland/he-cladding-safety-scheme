using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.VariationRequest.IneligibleCosts.Get;
using HE.Remediation.Core.UseCase.Areas.VariationRequest.IneligibleCosts.Set;

namespace HE.Remediation.WebApp.ViewModels.VariationRequest;

public class IneligibleCostsViewModelMapper : Profile
{
    public IneligibleCostsViewModelMapper()
    {
        CreateMap<GetIneligibleCostsResponse, IneligibleCostsViewModel>();
        CreateMap<IneligibleCostsViewModel, SetIneligibleCostsRequest>();
    }
}