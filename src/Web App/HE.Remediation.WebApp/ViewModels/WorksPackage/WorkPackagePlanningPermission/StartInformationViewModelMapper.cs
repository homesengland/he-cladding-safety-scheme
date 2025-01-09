using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackagePlanningPermission.StartInformation.Get;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackagePlanningPermission;

public class StartInformationViewModelMapper : Profile
{
    public StartInformationViewModelMapper()
    {
        CreateMap<GetStartInformationResponse, StartInformationViewModel>();
    }
}
