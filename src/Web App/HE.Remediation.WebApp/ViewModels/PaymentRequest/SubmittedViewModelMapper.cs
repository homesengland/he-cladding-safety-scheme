using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetSubmitted;

namespace HE.Remediation.WebApp.ViewModels.PaymentRequest;

public class SubmittedViewModelMapper : Profile
{
    public SubmittedViewModelMapper()
    {
        CreateMap<GetSubmittedResponse, SubmittedViewModel>();
    }
}
