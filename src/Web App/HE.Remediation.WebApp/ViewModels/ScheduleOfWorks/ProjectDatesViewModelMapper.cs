using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.ProjectDates.Get;
using HE.Remediation.Core.UseCase.Areas.ScheduleOfWorks.ProjectDates.Set;

namespace HE.Remediation.WebApp.ViewModels.ScheduleOfWorks;

public class ProjectDatesViewModelMapper : Profile
{
    public ProjectDatesViewModelMapper()
    {
        CreateMap<GetProjectDatesResponse, ProjectDatesViewModel>()
            .ForMember(d => d.ExpectedProjectStartDateMonth, o => o.MapFrom(s => s.ExpectedProjectStartDate.HasValue ? (int?)s.ExpectedProjectStartDate.Value.Month : null))
            .ForMember(d => d.ExpectedProjectStartDateYear, o => o.MapFrom(s => s.ExpectedProjectStartDate.HasValue ? (int?)s.ExpectedProjectStartDate.Value.Year : null))
            .ForMember(d => d.ExpectedProjectEndDateMonth, o => o.MapFrom(s => s.ExpectedProjectStartDate.HasValue ? (int?)s.ExpectedProjectEndDate.Value.Month : null))
            .ForMember(d => d.ExpectedProjectEndDateYear, o => o.MapFrom(s => s.ExpectedProjectStartDate.HasValue ? (int?)s.ExpectedProjectEndDate.Value.Year : null));
        CreateMap<ProjectDatesViewModel, SetProjectDatesRequest>();
    }
}
