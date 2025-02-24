using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.VariationRequest.ThirdPartyContribution.Get;
using HE.Remediation.Core.UseCase.Areas.VariationRequest.ThirdPartyContribution.Set;

namespace HE.Remediation.WebApp.ViewModels.VariationRequest;

public class ThirdPartyContributionViewModelMapper : Profile
{
    public ThirdPartyContributionViewModelMapper()
    {
        CreateMap<GetThirdPartyContributionResponse, ThirdPartyContributionViewModel>();
        CreateMap<ThirdPartyContributionViewModel, SetThirdPartyContributionRequest>();
    }
}
