using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetForecastGateway3Submission;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.SetForecastGateway3Submission;

namespace HE.Remediation.WebApp.ViewModels.PaymentRequest;

public class ForecastGateway3SubmissionViewModelMapper : Profile
{
    public ForecastGateway3SubmissionViewModelMapper()
    {
        CreateMap<GetForecastGateway3SubmissionResponse, ForecastGateway3SubmissionViewModel>();
        CreateMap<ForecastGateway3SubmissionViewModel, SetForecastGateway3SubmissionRequest>();
    }
}