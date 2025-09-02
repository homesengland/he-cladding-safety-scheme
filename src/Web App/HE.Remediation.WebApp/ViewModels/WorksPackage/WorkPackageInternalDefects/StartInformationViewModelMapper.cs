using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageInternalDefects.StartInformation;
namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageInternalDefects;

public class StartInformationViewModelMapper : Profile
{
    public StartInformationViewModelMapper()
    {
        CreateMap<GetStartInformationResponse, StartInformationViewModel>();
    }
}