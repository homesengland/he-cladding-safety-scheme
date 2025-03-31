using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageProgrammePlan;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageProgrammePlan;

public class StartInformationViewModelMapper : Profile
{
    public StartInformationViewModelMapper()
    {
        CreateMap<GetStartInformationResponse, StartInformationViewModel>();
    }
}