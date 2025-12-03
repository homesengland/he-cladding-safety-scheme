using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting;

public class ProgressReportDetailsViewModelMapper : Profile
{
    public ProgressReportDetailsViewModelMapper()
    {
        CreateMap<GetProgressReportDetailsResponse, ProgressReportDetailsViewModel>();
        CreateMap<GetProgressReportDetailsResponse.TeamMember, ProgressReportDetailsViewModel.TeamMember>();
        CreateMap<GetProgressReportDetailsResponse.KeyRoleTeamMember, ProgressReportDetailsViewModel.KeyRoleTeamMember>();
    }
}