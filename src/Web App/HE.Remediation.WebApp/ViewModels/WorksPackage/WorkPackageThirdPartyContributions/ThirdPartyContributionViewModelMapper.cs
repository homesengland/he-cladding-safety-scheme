using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageThirdPartyContributions.ThirdPartyContribution.Get;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageThirdPartyContributions.ThirdPartyContribution.Set;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageThirdPartyContributions;

public class ThirdPartyContributionViewModelMapper : Profile
{
    public ThirdPartyContributionViewModelMapper()
    {
        CreateMap<GetThirdPartyContributionResponse, ThirdPartyContributionViewModel>();
        CreateMap<ThirdPartyContributionViewModel, SetThirdPartyContributionRequest>();
    }
}
