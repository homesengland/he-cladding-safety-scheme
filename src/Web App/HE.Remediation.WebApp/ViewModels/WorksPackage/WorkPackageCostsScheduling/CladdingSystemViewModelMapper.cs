using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.CladdingSystem.Get;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.CladdingSystem.Set;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageCostsScheduling;

public class CladdingSystemViewModelMapper : Profile
{
    public CladdingSystemViewModelMapper()
    {
        CreateMap<GetCladdingSystemResponse, CladdingSystemViewModel>();
        CreateMap<CladdingSystemViewModel, SetCladdingSystemRequest>();
    }
}
