using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.TeamMember.GetTeamMemberCheckYourAnswers;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class TeamMemberCheckYourAnswersViewModelMapper : Profile
{
    public TeamMemberCheckYourAnswersViewModelMapper()
    {
        CreateMap<GetTeamMemberCheckYourAnswersResponse, TeamMemberCheckYourAnswersViewModel>();
    }
}