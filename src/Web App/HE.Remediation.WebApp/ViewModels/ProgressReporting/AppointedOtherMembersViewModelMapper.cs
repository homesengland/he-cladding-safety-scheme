using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.AppointedOtherMembers.GetAppointedOtherMembers;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.AppointedOtherMembers.SetAppointedOtherMembers;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class AppointedOtherMembersViewModelMapper : Profile
{
    public AppointedOtherMembersViewModelMapper()
    {
        CreateMap<GetAppointedOtherMembersResponse, AppointedOtherMembersViewModel>();
        CreateMap<AppointedOtherMembersViewModel, SetAppointedOtherMembersRequest>();
    }
}
