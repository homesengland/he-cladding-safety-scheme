using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageThirdPartyContributions.PursuingThirdPartyContribution.Get;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageThirdPartyContributions.PursuingThirdPartyContribution.Set;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageThirdPartyContributions;

public class PursuingThirdPartyContributionViewModelMapper : Profile
{
    public PursuingThirdPartyContributionViewModelMapper()
    {
        CreateMap<GetPursuingThirdPartyContributionResponse, PursuingThirdPartyContributionViewModel>();
        CreateMap<PursuingThirdPartyContributionViewModel, SetPursuingThirdPartyContributionRequest>();
    }
}
