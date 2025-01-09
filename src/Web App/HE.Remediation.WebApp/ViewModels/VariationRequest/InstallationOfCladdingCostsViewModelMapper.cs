using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.VariationRequest.InstallationOfCladdingCosts.Get;
using HE.Remediation.Core.UseCase.Areas.VariationRequest.InstallationOfCladdingCosts.Set;

namespace HE.Remediation.WebApp.ViewModels.VariationRequest
{
    public class InstallationOfCladdingCostsViewModelMapper : Profile
    {
        public InstallationOfCladdingCostsViewModelMapper()
        {
            CreateMap<GetInstallationOfCladdingCostsResponse, InstallationOfCladdingCostsViewModel>()
                .ForMember(d => d.VariationNewCladdingAmountText, o => o.MapFrom(s => s.VariationNewCladdingAmount.HasValue ? s.VariationNewCladdingAmount.Value.ToString("N0") : null))
                .ForMember(d => d.VariationOtherEligibleWorkToExternalWallAmountText, o => o.MapFrom(s => s.VariationOtherEligibleWorkToExternalWallAmount.HasValue ? s.VariationOtherEligibleWorkToExternalWallAmount.Value.ToString("N0") : null))
                .ForMember(d => d.VariationInternalMitigationWorksAmountText, o => o.MapFrom(s => s.VariationInternalMitigationWorksAmount.HasValue ? s.VariationInternalMitigationWorksAmount.Value.ToString("N0") : null));


            CreateMap<InstallationOfCladdingCostsViewModel, SetInstallationOfCladdingCostsRequest>();
        }
    }
}
