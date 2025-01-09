using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.Costs.UnsafeCladding;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageCostsScheduling;

public class UnsafeCladdingCostsViewModelMapper : Profile
{
    public UnsafeCladdingCostsViewModelMapper()
    {
        CreateMap<GetUnsafeCladdingCostsResponse, UnsafeCladdingCostsViewModel>()
            .ForMember(d => d.UnsafeCladdingRemovalAmountText, o => o.MapFrom(s => s.UnsafeCladdingRemovalAmount.HasValue ? s.UnsafeCladdingRemovalAmount.Value.ToString("N0") : null));
        CreateMap<UnsafeCladdingCostsViewModel, SetUnsafeCladdingCostsRequest>();
    }
}