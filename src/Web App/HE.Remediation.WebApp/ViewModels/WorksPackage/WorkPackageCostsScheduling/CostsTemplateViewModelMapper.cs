using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.CostsTemplate.Get;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageCostsScheduling;

public class CostsTemplateViewModelMapper : Profile
{
    public CostsTemplateViewModelMapper()
    {
        CreateMap<GetCostsTemplateResponse, CostsTemplateViewModel>();
    }
}
