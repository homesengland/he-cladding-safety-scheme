using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.Submitted.Get;

namespace HE.Remediation.WebApp.ViewModels.ScheduleOfWorks;

public class SubmittedViewModelMapper : Profile
{
    public SubmittedViewModelMapper()
    {
        CreateMap<GetSubmittedResponse, SubmittedViewModel>();
    }
}
