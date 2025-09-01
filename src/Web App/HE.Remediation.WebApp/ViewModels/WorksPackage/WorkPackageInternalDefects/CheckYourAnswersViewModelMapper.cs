using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageInternalDefects.CheckYourAnswers;
namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageInternalDefects;

public class CheckYourAnswersViewModelMapper : Profile
{
    public CheckYourAnswersViewModelMapper()
    {
        CreateMap<GetInternalDefectsCheckYourAnswersResponse, CheckYourAnswersViewModel>();
    }
}