using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.VariationRequest.Submitted.Get;

namespace HE.Remediation.WebApp.ViewModels.VariationRequest;

public class SubmittedViewModelMapper : Profile
{
    public SubmittedViewModelMapper()
    {
        CreateMap<GetSubmittedResponse, SubmittedViewModel>();
    }
}
