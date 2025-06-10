using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.WithdrawalRequest.GetFinalCheckYourAnswers;

namespace HE.Remediation.WebApp.ViewModels.WithdrawalRequest
{
    public class FinalCheckYourAnswersViewModelMapper : Profile
    {
        public FinalCheckYourAnswersViewModelMapper()
        {
            CreateMap<GetFinalCheckYourAnswersResponse, FinalCheckYourAnswersViewModel>();
        }
    }
}