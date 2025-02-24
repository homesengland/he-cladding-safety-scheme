using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.PracticalCompletionMilestone;

namespace HE.Remediation.WebApp.ViewModels.PracticalCompletionMilestone;

public class CheckYourAnswersViewModelMapper : Profile
{
    public CheckYourAnswersViewModelMapper()
    {
        CreateMap<GetCheckYourAnswersResponse, CheckYourAnswersViewModel>();
    }
}