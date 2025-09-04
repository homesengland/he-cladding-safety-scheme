using AutoMapper;

using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCladdingSystem.StartInformation.Get;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageCladdingSystem;

public class StartInformationViewModelMapper : Profile
{
    public StartInformationViewModelMapper()
    {
        CreateMap<GetStartInformationResponse, StartInformationViewModel>();
    }
}
