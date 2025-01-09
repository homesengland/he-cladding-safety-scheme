using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.StartedOnSiteMilestone;

namespace HE.Remediation.WebApp.ViewModels.StartedOnSiteMilestone;

public class CheckYourAnswersViewModelMapper : Profile
{
    public CheckYourAnswersViewModelMapper()
    {
        CreateMap<GetCheckYourAnswersResponse, CheckYourAnswersViewModel>();
    }
}