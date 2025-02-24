using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.VariationRequest.AboutCosts.Get;

namespace HE.Remediation.WebApp.ViewModels.VariationRequest;

public class AboutAdjustCostsViewModelMapper : Profile
{
    public AboutAdjustCostsViewModelMapper()
    {
        CreateMap<GetAboutCostsResponse, AboutAdjustCostsViewModel>();
    }
}
