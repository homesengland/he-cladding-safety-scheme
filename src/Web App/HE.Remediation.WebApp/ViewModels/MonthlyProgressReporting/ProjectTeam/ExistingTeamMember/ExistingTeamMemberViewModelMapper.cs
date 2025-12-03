using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.ProjectTeam.ExistingTeamMember;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.ProjectTeam.ExistingTeamMember;
public class ExistingTeamMemberViewModelMapper : Profile
{
    public ExistingTeamMemberViewModelMapper()
    {
        CreateMap<GetProjectTeamExistingTeamMemberResponse, ExistingTeamMemberViewModel>();
    }
}
