using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.WorksAlreadyCompleted.GetWorksAlreadyCompleted;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.WorksAlreadyCompleted.SetWorksAlreadyCompleted;

namespace HE.Remediation.WebApp.ViewModels.BuildingDetails;

public class WorksAlreadyCompletedViewModelMapper : Profile
{
    public WorksAlreadyCompletedViewModelMapper()
    {
        CreateMap<WorksAlreadyCompletedViewModel, SetWorksAlreadyCompletedRequest>();

        CreateMap<GetWorksAlreadyCompletedResponse, WorksAlreadyCompletedViewModel>();
    }
}