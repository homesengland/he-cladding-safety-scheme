using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.StartInformation.Get;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageCostsScheduling;

public class StartInformationViewModelMapper : Profile
{
    public StartInformationViewModelMapper()
    {
        CreateMap<GetStartInformationResponse, StartInformationViewModel>();
    }
}
