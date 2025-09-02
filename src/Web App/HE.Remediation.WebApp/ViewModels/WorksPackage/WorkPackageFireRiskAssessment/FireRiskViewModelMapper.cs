using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageFireRiskAssessment;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageFireRiskAssessment;

public class FireRiskViewModelMapper : Profile
{
    public FireRiskViewModelMapper()
    {
        CreateMap<GetFireRiskResponse, FireRiskViewModel>();
        CreateMap<FireRiskViewModel, SetFireRiskRequest>();
    }
}