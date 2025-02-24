using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.SubcontractorTeam.Get;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageCostsScheduling;

public class SubcontractorTeamViewModelMapper : Profile
{
    public SubcontractorTeamViewModelMapper()
    {
        CreateMap<GetSubcontractorTeamResponse, SubcontractorTeamViewModel>();
    }
}
