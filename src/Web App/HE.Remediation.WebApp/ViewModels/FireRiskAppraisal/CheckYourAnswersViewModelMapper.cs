using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.CheckYourAnswers.GetCheckYourAnswers;

namespace HE.Remediation.WebApp.ViewModels.FireRiskAppraisal
{
    public class CheckYourAnswersViewModelMapper : Profile
    {
        public CheckYourAnswersViewModelMapper()
        {
            CreateMap<GetCheckYourAnswersResponse, CheckYourAnswersViewModel>();
        }
    }
}
