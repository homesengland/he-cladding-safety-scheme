using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ApprovedScheduleOfWorks.BaseInformation.Get;

namespace HE.Remediation.WebApp.ViewModels.ApprovedScheduleOfWorks;

public class StartInformationViewModelMapper : Profile
{
    public StartInformationViewModelMapper()
    {
        CreateMap<GetBaseInformationResponse, StartInformationViewModel>();
    }
}
