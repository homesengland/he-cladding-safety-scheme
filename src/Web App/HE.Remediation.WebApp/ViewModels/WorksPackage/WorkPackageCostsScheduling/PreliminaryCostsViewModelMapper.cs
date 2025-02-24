using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.Costs.Preliminary;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageCostsScheduling;

public class PreliminaryCostsViewModelMapper : Profile
{
    public PreliminaryCostsViewModelMapper()
    {
        CreateMap<GetPreliminaryResponse, PreliminaryCostsViewModel>()
            .ForMember(d => d.AccessAmountText, o => o.MapFrom(s => s.AccessAmount.HasValue ? s.AccessAmount.Value.ToString("N0") : null))
            .ForMember(d => d.ContractorContingenciesAmountText, o => o.MapFrom(s => s.ContractorContingenciesAmount.HasValue ? s.ContractorContingenciesAmount.Value.ToString("N0") : null))
            .ForMember(d => d.MainContractorPreliminariesAmountText, o => o.MapFrom(s => s.MainContractorPreliminariesAmount.HasValue ? s.MainContractorPreliminariesAmount.Value.ToString("N0") : null))
            .ForMember(d => d.MainContractorOverheadAmountText, o => o.MapFrom(s => s.MainContractorOverheadAmount.HasValue ? s.MainContractorOverheadAmount.Value.ToString("N0") : null))            ;
        CreateMap<PreliminaryCostsViewModel, SetPreliminaryRequest>();
    }
}