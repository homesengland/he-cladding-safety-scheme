using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.Costs.Description;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageCostsScheduling;

public class CostsDescriptionViewModelMapper : Profile
{
    public CostsDescriptionViewModelMapper()
    {
        CreateMap<GetCostDescriptionResponse, CostsDescriptionViewModel>();
    }
}