using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.VariationRequest.PreliminaryCosts.Get;
using HE.Remediation.Core.UseCase.Areas.VariationRequest.PreliminaryCosts.Set;

namespace HE.Remediation.WebApp.ViewModels.VariationRequest
{
    public class PreliminaryCostsViewModelMapper : Profile
    {
        public PreliminaryCostsViewModelMapper()
        {
            CreateMap<GetPreliminaryCostsResponse, PreliminaryCostsViewModel>()
                .ForMember(d => d.VariationMainContractorPreliminariesAmountText, o => o.MapFrom(s => s.VariationMainContractorPreliminariesAmount.HasValue ? s.VariationMainContractorPreliminariesAmount.Value.ToString("N0") : null))
                .ForMember(d => d.VariationAccessAmountText, o => o.MapFrom(s => s.VariationAccessAmount.HasValue ? s.VariationAccessAmount.Value.ToString("N0") : null))
                .ForMember(d => d.VariationOverheadsAndProfitAmountText, o => o.MapFrom(s => s.VariationOverheadsAndProfitAmount.HasValue ? s.VariationOverheadsAndProfitAmount.Value.ToString("N0") : null))
                .ForMember(d => d.VariationContractorContingenciesAmountText, o => o.MapFrom(s => s.VariationContractorContingenciesAmount.HasValue ? s.VariationContractorContingenciesAmount.Value.ToString("N0") : null));

            CreateMap<PreliminaryCostsViewModel, SetPreliminaryCostsRequest>();
        }
    }
}
