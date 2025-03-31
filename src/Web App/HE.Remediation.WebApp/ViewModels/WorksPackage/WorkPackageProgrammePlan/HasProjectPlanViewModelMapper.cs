using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageProgrammePlan;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageProgrammePlan;

public class HasProjectPlanViewModelMapper : Profile
{
    public HasProjectPlanViewModelMapper()
    {
        CreateMap<GetHasProjectPlanResponse, HasProjectPlanViewModel>();
        CreateMap<HasProjectPlanViewModel, SetHasProjectPlanRequest>();
    }
}