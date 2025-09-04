using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.BaseInformation.Get;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCladdingSystem.ResetCladdingSystem;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageCladdingSystem;

public class CladdingSystemChangeAnswersViewModelMapper : Profile
{
    public CladdingSystemChangeAnswersViewModelMapper()
    {
        CreateMap<GetBaseInformationResponse, CladdingSystemChangeAnswersViewModel>();
        CreateMap<CladdingSystemChangeAnswersViewModel, ResetCladdingSystemRequest>();
    }
}
