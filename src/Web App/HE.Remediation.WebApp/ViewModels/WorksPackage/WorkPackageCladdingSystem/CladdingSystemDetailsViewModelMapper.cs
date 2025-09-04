using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCladdingSystem.CladdingSystemDetails.Get;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCladdingSystem.CladdingSystemDetails.Set;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageCladdingSystem;

public class CladdingSystemDetailsViewModelMapper : Profile
{
    public CladdingSystemDetailsViewModelMapper()
    {
        CreateMap<GetCladdingSystemDetailsResponse, CladdingSystemDetailsViewModel>();
        CreateMap<CladdingSystemDetailsViewModel, SetCladdingSystemDetailsRequest>();
    }
}
