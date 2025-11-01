using AutoMapper;

using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.BaseInformation.Get;

namespace HE.Remediation.WebApp.ViewModels.ScheduleOfWorks;

public class WorksContractStartInformationViewModelMpper : Profile
{
    public WorksContractStartInformationViewModelMpper()
    {
        CreateMap<GetBaseInformationResponse, WorksContractStartInformationViewModel>();
    }
}