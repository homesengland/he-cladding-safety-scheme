using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetProjectTeam;

namespace HE.Remediation.WebApp.ViewModels.PaymentRequest;

public class ProjectTeamViewModelMapper : Profile
{
    public ProjectTeamViewModelMapper()
    {
        CreateMap<GetProjectTeamResponse, ProjectTeamViewModel>();
    }
}
