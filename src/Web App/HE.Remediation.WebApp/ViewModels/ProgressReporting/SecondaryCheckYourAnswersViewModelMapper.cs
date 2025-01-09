using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.SecondaryCheckYourAnswers.GetCheckYourAnswers;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting
{
    public class SecondaryCheckYourAnswersViewModelMapper : Profile
    {
        public SecondaryCheckYourAnswersViewModelMapper()
        {
            CreateMap<GetCheckYourAnswersResponse, SecondaryCheckYourAnswersViewModel>();
        }
    }
}
