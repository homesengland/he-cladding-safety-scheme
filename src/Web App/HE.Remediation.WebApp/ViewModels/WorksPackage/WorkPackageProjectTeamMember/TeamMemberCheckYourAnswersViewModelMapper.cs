using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageProjectTeamMember.CheckYourAnswers.Get;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageProjectTeamMember;

public class TeamMemberCheckYourAnswersViewModelMapper : Profile
{
    public TeamMemberCheckYourAnswersViewModelMapper()
    {
        CreateMap<GetTeamMemberCheckYourAnswersResponse, TeamMemberCheckYourAnswersViewModel>();
    }
}