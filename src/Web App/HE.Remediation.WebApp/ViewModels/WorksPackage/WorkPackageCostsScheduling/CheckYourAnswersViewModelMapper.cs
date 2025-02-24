using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.CheckYourAnswers;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageCostsScheduling
{
    public class CheckYourAnswersViewModelMapper: Profile
    {
        public CheckYourAnswersViewModelMapper()
        {
            CreateMap<GetCheckYourAnswersResponse, CheckYourAnswersViewModel>();

            CreateMap<GetCheckYourAnswersResponse.SubContractor, SubContractors>();

            CreateMap<GetCheckYourAnswersResponse.CladdingSystem, CladdingSystem>();
        }
    }
}
