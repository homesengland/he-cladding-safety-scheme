using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageKeyDates.CheckYourAnswers.Get;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageKeyDates;

public class CheckYourAnswersViewModelMapper : Profile
{
    public CheckYourAnswersViewModelMapper()
    {
        CreateMap<GetCheckYourAnswersResponse, CheckYourAnswersViewModel>();
    }
}
