using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageThirdPartyContributions.CheckYourAnswers.Get;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageThirdPartyContributions
{
    public class CheckYourAnswersViewModelMapper: Profile
    {
        public CheckYourAnswersViewModelMapper()
        {
            CreateMap<GetCheckYourAnswersResponse, CheckYourAnswersViewModel>();
        }
    }
}
