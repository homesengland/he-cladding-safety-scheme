using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.IneligibleCosts;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageCostsScheduling;

public class IneligibleCostsViewModelMapper : Profile
{
    public IneligibleCostsViewModelMapper()
    {
        CreateMap<GetIneligibleCostsResponse, IneligibleCostsViewModel>()
            .ForMember(d => d.IneligibleAmountText, o => o.MapFrom(s => s.IneligibleAmount.HasValue ? s.IneligibleAmount.Value.ToString("N0") : null));
        CreateMap<IneligibleCostsViewModel, SetIneligibleCostsRequest>();
    }
}