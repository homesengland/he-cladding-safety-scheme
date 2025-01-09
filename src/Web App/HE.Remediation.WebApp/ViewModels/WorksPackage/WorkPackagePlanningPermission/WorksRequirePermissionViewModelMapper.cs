using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackagePlanningPermission.WorksRequirePermission.Get;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackagePlanningPermission.WorksRequirePermission.Set;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackagePlanningPermission;

public class WorksRequirePermissionViewModelMapper : Profile
{
    public WorksRequirePermissionViewModelMapper()
    {
        CreateMap<GetWorksRequirePermissionResponse, WorksRequirePermissionViewModel>();
        CreateMap<WorksRequirePermissionViewModel, SetWorksRequirePermissionRequest>();
    }
}
