using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.VariationRequest.BaseInformation.Get;

namespace HE.Remediation.WebApp.ViewModels.VariationRequest;

public class StartInformationViewModelMapper : Profile
{
    public StartInformationViewModelMapper()
    {
        CreateMap<GetBaseInformationResponse, StartInformationViewModel>();
    }
}
