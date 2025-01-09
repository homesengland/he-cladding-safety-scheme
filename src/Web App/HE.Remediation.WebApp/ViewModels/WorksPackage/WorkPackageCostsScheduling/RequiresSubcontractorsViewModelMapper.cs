using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.RequiresSubcontractors.Get;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.RequiresSubcontractors.Set;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageCostsScheduling;

public class RequiresSubcontractorsViewModelMapper : Profile
{
    public RequiresSubcontractorsViewModelMapper()
    {
        CreateMap<GetRequiresSubcontractorsResponse, RequiresSubcontractorsViewModel>();
        CreateMap<RequiresSubcontractorsViewModel, SetRequiresSubcontractorsRequest>();
    }
}
