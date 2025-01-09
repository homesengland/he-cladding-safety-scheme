using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.ReasonNoOtherMembers.GetReasonNoOtherMembers;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.ReasonNoOtherMembers.SetReasonNoOtherMembers;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class ReasonNoOtherMembersViewModelMapper : Profile
{
    public ReasonNoOtherMembersViewModelMapper()
    {
        CreateMap<GetReasonNoOtherMembersResponse, ReasonNoOtherMembersViewModel>();
        CreateMap<ReasonNoOtherMembersViewModel, SetReasonNoOtherMembersRequest>();
    }
}
