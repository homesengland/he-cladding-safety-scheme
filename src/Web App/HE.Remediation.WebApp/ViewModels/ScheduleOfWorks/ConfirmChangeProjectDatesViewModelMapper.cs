using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.ConfirmChangeProjectDates.Get;
using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.ProjectDates.Set;

namespace HE.Remediation.WebApp.ViewModels.ScheduleOfWorks;

public class ConfirmChangeProjectDatesViewModelMapper : Profile
{
    public ConfirmChangeProjectDatesViewModelMapper()
    {
        CreateMap<GetConfirmChangeProjectDatesResponse, ConfirmChangeProjectDatesViewModel>();
        CreateMap<ProjectDatesViewModel, ConfirmChangeProjectDatesViewModel>()
            .ForMember(d => d.ProjectEndDateMonth, o => o.MapFrom(s => s.ProjectEndDateMonth))
            .ForMember(d => d.ProjectStartDateYear, o => o.MapFrom(s => s.ProjectStartDateYear))
            .ForMember(d => d.ProjectEndDateMonth, o => o.MapFrom(s => s.ProjectEndDateMonth))
            .ForMember(d => d.ProjectEndDateYear, o => o.MapFrom(s => s.ProjectEndDateYear));
        CreateMap<ConfirmChangeProjectDatesViewModel, SetProjectDatesRequest>();
    }
}
