using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorksPackageCladdingSystem.CladdingSystem.Get;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorksPackageCladdingSystem.CladdingSystem.Set;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageCladdingSystem;

public class CladdingSystemViewModelMapper : Profile
{
    public CladdingSystemViewModelMapper()
    {
        CreateMap<GetCladdingSystemResponse, CladdingSystemViewModel>();
        CreateMap<CladdingSystemViewModel, SetCladdingSystemRequest>();
    }
}
