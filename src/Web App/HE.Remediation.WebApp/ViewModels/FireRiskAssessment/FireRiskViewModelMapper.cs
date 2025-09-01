using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.FireRiskAssessment;

namespace HE.Remediation.WebApp.ViewModels.FireRiskAssessment;

public class FireRiskViewModelMapper : Profile
{
    public FireRiskViewModelMapper()
    {
        CreateMap<GetFireRiskResponse, FireRiskViewModel>();
        CreateMap<FireRiskViewModel, SetFireRiskRequest>();
    }
}