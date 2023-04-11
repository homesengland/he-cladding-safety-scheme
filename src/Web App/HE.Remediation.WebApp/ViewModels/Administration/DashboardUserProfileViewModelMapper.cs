using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.Administration.Dashboard.GetProfile;

namespace HE.Remediation.WebApp.ViewModels.Administration;

public class DashboardUserProfileViewModelMapper : Profile
{
    public DashboardUserProfileViewModelMapper()
    {
        CreateMap<GetProfileRequest, DashboardUserProfileViewModel>();
    }
}
