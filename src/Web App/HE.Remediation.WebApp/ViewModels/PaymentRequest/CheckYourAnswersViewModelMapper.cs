using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetCheckYourAnswers;

namespace HE.Remediation.WebApp.ViewModels.PaymentRequest;

public class CheckYourAnswersViewModelMapper : Profile
{
    public CheckYourAnswersViewModelMapper()
    {
        CreateMap<GetCheckYourAnswersResponse, CheckYourAnswersViewModel>();
        CreateMap<Core.UseCase.Areas.PaymentRequest.GetCheckYourAnswers.PaymentRequestCostFile, PaymentRequestCostFile>();
    }
}
