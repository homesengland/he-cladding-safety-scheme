using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.ProjectTeam.TeamMember;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.ProjectTeam.TeamMember;
public class TeamMemberCheckYourAnswersViewModelMapper : Profile
{
    public TeamMemberCheckYourAnswersViewModelMapper()
    {
        CreateMap<GetTeamMemberCheckYourAnswersResponse, TeamMemberCheckYourAnswersViewModel>();
    }
}