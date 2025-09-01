using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.FireRiskAssessment;

namespace HE.Remediation.WebApp.ViewModels.FireRiskAssessment;

public class HasFraViewModelMapper : Profile
{
    public HasFraViewModelMapper()
    {
        CreateMap<GetHasFraResponse, HasFraViewModel>();
        CreateMap<HasFraViewModel, SetHasFraRequest>();
    }
}