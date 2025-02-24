using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackagePlanningPermission.CheckYourAnswers.Get;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackagePlanningPermission;

public class CheckYourAnswersViewModelMapper : Profile
{
    public CheckYourAnswersViewModelMapper()
    {
        CreateMap<GetCheckYourAnswersResponse, CheckYourAnswersViewModel>();
    }
}
