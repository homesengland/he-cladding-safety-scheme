using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageProjectTeam.CheckYourAnswers;


namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageProjectTeam;

public class ProjectTeamCheckYourAnswersViewModelMapper : Profile
{
    public ProjectTeamCheckYourAnswersViewModelMapper()
    {
        CreateMap<GetCheckYourAnswersResponse, ProjectTeamCheckYourAnswersViewModel>();
    }
}