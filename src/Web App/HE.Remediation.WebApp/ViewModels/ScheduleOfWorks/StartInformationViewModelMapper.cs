using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.BaseInformation.Get;

namespace HE.Remediation.WebApp.ViewModels.ScheduleOfWorks;

public class StartInformationViewModelMapper : Profile
{
    public StartInformationViewModelMapper()
    {
        CreateMap<GetBaseInformationResponse, StartInformationViewModel>();
    }
}
