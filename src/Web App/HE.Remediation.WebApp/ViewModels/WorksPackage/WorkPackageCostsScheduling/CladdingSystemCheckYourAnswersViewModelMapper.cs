using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.CladdingSystemCheckYourAnswers.Get;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.CladdingSystemCheckYourAnswers.Set;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageCostsScheduling;

public class CladdingSystemCheckYourAnswersViewModelMapper : Profile
{
    public CladdingSystemCheckYourAnswersViewModelMapper()
    {
        CreateMap<GetCladdingSystemCheckYourAnswersResponse, CladdingSystemCheckYourAnswersViewModel>();
        CreateMap<CladdingSystemCheckYourAnswersViewModel, SetCladdingSystemCheckYourAnswersRequest>();
    }
}
