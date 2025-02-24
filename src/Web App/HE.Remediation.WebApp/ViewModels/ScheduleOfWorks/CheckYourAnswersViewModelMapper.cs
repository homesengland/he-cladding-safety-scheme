using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.CheckYourAnswers.Get;

namespace HE.Remediation.WebApp.ViewModels.ScheduleOfWorks;

public class CheckYourAnswersViewModelMapper : Profile
{
    public CheckYourAnswersViewModelMapper()
    {
        CreateMap<GetCheckYourAnswersResponse, CheckYourAnswersViewModel>();
    }
}
