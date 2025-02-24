using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetSubContractorCheckYourAnswers;

namespace HE.Remediation.WebApp.ViewModels.PaymentRequest.SubcontractorSurvey;

public class CheckYourAnswersViewModelMapper : Profile
{
    public CheckYourAnswersViewModelMapper()
    {
        CreateMap<GetSubContractorCheckYourAnswersResponse, CheckYourAnswersViewModel>();
    }
}