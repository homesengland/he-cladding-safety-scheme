using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.WorksRequirePermission.GetWorksRequirePermission;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.WorksRequirePermission.SetWorksRequirePermission;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class WorksRequirePermissionViewModelMapper : Profile
{
    public WorksRequirePermissionViewModelMapper()
    {
        CreateMap<GetWorksRequirePermissionResponse, WorksRequirePermissionViewModel>();
        CreateMap<WorksRequirePermissionViewModel, SetWorksRequirePermissionRequest>();
    }
}
