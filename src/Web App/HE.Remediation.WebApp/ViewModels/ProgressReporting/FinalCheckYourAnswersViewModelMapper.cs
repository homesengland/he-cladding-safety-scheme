using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.FinalCheckYourAnswers.GetFinalCheckYourAnswers;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class FinalCheckYourAnswersViewModelMapper : Profile
{
    public FinalCheckYourAnswersViewModelMapper()
    {
        CreateMap<GetFinalCheckYourAnswersResponse, FinalCheckYourAnswersViewModel>();
    }
}
