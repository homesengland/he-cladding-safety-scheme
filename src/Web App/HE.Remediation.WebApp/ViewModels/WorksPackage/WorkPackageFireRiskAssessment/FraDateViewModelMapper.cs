using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageFireRiskAssessment;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageFireRiskAssessment;

public class FraDateViewModelMapper : Profile
{
    public FraDateViewModelMapper()
    {
        CreateMap<GetFraDateResponse, FraDateViewModel>();
        CreateMap<FraDateViewModel, SetFraDateRequest>();
    }
}