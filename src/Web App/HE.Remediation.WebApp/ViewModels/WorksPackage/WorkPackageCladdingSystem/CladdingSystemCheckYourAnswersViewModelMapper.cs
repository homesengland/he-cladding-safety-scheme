using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCladdingSystem.CladdingSystemCheckYourAnswers.Get;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCladdingSystem.CladdingSystemCheckYourAnswers.Set;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageCladdingSystem;

public class CladdingSystemCheckYourAnswersViewModelMapper : Profile
{
    public CladdingSystemCheckYourAnswersViewModelMapper()
    {
        CreateMap<GetCladdingSystemCheckYourAnswersResponse, CladdingSystemCheckYourAnswersViewModel>();
        CreateMap<CladdingSystemCheckYourAnswersViewModel, SetCladdingSystemCheckYourAnswersRequest>();
    }
}
