using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.CladdingSystemDetails.Get;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.CladdingSystemDetails.Set;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageCostsScheduling;

public class CladdingSystemDetailsViewModelMapper : Profile
{
    public CladdingSystemDetailsViewModelMapper()
    {
        CreateMap<GetCladdingSystemDetailsResponse, CladdingSystemDetailsViewModel>();
        CreateMap<CladdingSystemDetailsViewModel, SetCladdingSystemDetailsRequest>();
    }
}
