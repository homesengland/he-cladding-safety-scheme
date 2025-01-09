using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageProjectTeam.StartInformation.Get;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageProjectTeam;

public class StartInformationViewModelMapper : Profile
{
    public StartInformationViewModelMapper()
    {
        CreateMap<GetStartInformationResponse, StartInformationViewModel>();
    }
}
