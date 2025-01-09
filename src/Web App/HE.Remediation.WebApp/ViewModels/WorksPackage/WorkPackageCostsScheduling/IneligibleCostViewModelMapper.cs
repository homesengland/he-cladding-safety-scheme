using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.IneligibleCost;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageCostsScheduling;

public class IneligibleCostViewModelMapper : Profile
{
    public IneligibleCostViewModelMapper()
    {
        CreateMap<GetIneligibleCostResponse, IneligibleCostViewModel>();
        CreateMap<IneligibleCostViewModel, SetIneligibleCostRequest>();
    }
}
