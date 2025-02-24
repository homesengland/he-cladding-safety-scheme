using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.Costs.InstallationOfCladding;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageCostsScheduling;

public class InstallationOfCladdingCostsViewModelMapper : Profile
{
    public InstallationOfCladdingCostsViewModelMapper()
    {
        CreateMap<GetInstallationOfCladdingResponse, InstallationOfCladdingCostsViewModel>()
            .ForMember(d => d.NewCladdingAmountText, o => o.MapFrom(s => s.NewCladdingAmount.HasValue ? s.NewCladdingAmount.Value.ToString("N0") : null))
            .ForMember(d => d.ExternalWorksAmountText, o => o.MapFrom(s => s.ExternalWorksAmount.HasValue ? s.ExternalWorksAmount.Value.ToString("N0") : null))
            .ForMember(d => d.InternalWorksAmountText, o => o.MapFrom(s => s.InternalWorksAmount.HasValue ? s.InternalWorksAmount.Value.ToString("N0") : null));
        CreateMap<InstallationOfCladdingCostsViewModel, SetInstallationOfCladdingRequest>();
    }
}