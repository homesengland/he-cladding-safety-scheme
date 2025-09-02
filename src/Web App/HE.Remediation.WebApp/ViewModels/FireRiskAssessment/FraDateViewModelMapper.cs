using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.FireRiskAssessment;

namespace HE.Remediation.WebApp.ViewModels.FireRiskAssessment;

public class FraDateViewModelMapper : Profile
{
    public FraDateViewModelMapper()
    {
        CreateMap<GetFraDateResponse, FraDateViewModel>();
        CreateMap<FraDateViewModel, SetFraDateRequest>();
    }   
}