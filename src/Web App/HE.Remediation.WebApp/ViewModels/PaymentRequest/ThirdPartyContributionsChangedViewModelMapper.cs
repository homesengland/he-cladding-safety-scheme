using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetThirdPartyContributionsChanged;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.SetThirdPartyContributionsChanged;

namespace HE.Remediation.WebApp.ViewModels.PaymentRequest;

public class ThirdPartyContributionsChangedViewModelMapper : Profile
{
    public ThirdPartyContributionsChangedViewModelMapper()
    {
        CreateMap<GetThirdPartyContributionsChangedResponse, ThirdPartyContributionsChangedViewModel>();
        CreateMap<ThirdPartyContributionsChangedViewModel, SetThirdPartyContributionsChangedRequest>();
    }   
}
