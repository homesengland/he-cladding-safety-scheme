using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.ExistingTeamMember;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class ExistingTeamMemberViewModelMapper : Profile
{
    public ExistingTeamMemberViewModelMapper()
    {
        CreateMap<GetExistingTeamMemberResponse, ExistingTeamMemberViewModel>();
    }
}