using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.VariationRequest.OtherCosts.Get;
using HE.Remediation.Core.UseCase.Areas.VariationRequest.OtherCosts.Set;

namespace HE.Remediation.WebApp.ViewModels.VariationRequest
{
    public class OtherCostsViewModelMapper : Profile
    {
        public OtherCostsViewModelMapper()
        {
            CreateMap<GetOtherCostsResponse, OtherCostsViewModel>()
                .ForMember(d => d.VariationFraewSurveyCostsAmountText, o => o.MapFrom(s => s.VariationFraewSurveyCostsAmount.HasValue ? s.VariationFraewSurveyCostsAmount.Value.ToString("N0") : null))
                .ForMember(d => d.VariationFeasibilityStageAmountText, o => o.MapFrom(s => s.VariationFeasibilityStageAmount.HasValue ? s.VariationFeasibilityStageAmount.Value.ToString("N0") : null))
                .ForMember(d => d.VariationPostTenderStageAmountText, o => o.MapFrom(s => s.VariationPostTenderStageAmount.HasValue ? s.VariationPostTenderStageAmount.Value.ToString("N0") : null))
                .ForMember(d => d.VariationIrrecoverableVatAmountText, o => o.MapFrom(s => s.VariationIrrecoverableVatAmount.HasValue ? s.VariationIrrecoverableVatAmount.Value.ToString("N0") : null))
                .ForMember(d => d.VariationPropertyManagerAmountText, o => o.MapFrom(s => s.VariationPropertyManagerAmount.HasValue ? s.VariationPropertyManagerAmount.Value.ToString("N0") : null));

            CreateMap<OtherCostsViewModel, SetOtherCostsRequest>();
        }
    }
}