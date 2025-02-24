using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageProjectTeamMember.ExistingMember.Get;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageProjectTeamMember;

public class ExistingMemberViewModelMapper : Profile
{
    public ExistingMemberViewModelMapper()
    {
        CreateMap<GetExistingMemberResponse, ExistingMemberViewModel>();
    }
}