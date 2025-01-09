using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.VariationRequest.UnsafeCladdingCosts.Get;
using HE.Remediation.Core.UseCase.Areas.VariationRequest.UnsafeCladdingCosts.Set;

namespace HE.Remediation.WebApp.ViewModels.VariationRequest
{
    public class UnsafeCladdingCostsViewModelMapper : Profile
    {
        public UnsafeCladdingCostsViewModelMapper()
        {
            CreateMap<GetUnsafeCladdingCostsResponse, UnsafeCladdingCostsViewModel>()
                    .ForMember(d => d.VariationRemovalOfCladdingAmountText, o => o.MapFrom(s => s.VariationRemovalOfCladdingAmount.HasValue ? s.VariationRemovalOfCladdingAmount.Value.ToString("N0") : null));
            CreateMap<UnsafeCladdingCostsViewModel, SetUnsafeCladdingCostsRequest>();
        }
    }
}