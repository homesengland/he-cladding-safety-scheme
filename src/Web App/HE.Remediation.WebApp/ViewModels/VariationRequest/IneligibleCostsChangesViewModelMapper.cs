using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.VariationRequest.IneligibleCostsChanges.Get;
using HE.Remediation.Core.UseCase.Areas.VariationRequest.IneligibleCostsChanges.Set;

namespace HE.Remediation.WebApp.ViewModels.VariationRequest;

public class IneligibleCostsChangesViewModelMapper : Profile
{
    public IneligibleCostsChangesViewModelMapper()
    {
        CreateMap<GetIneligibleCostsChangesResponse, IneligibleCostsChangesViewModel>()
            .ForMember(d => d.VariationIneligibleAmountText, o => o.MapFrom(s => s.VariationIneligibleAmount.HasValue ? s.VariationIneligibleAmount.Value.ToString("N0") : null));
        CreateMap<IneligibleCostsChangesViewModel, SetIneligibleCostsChangesRequest>();
    }
}
