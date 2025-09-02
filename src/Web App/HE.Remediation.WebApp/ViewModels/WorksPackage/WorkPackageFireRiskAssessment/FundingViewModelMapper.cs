using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageFireRiskAssessment;

namespace HE.Remediation.WebApp.ViewModels.WorksPackage.WorkPackageFireRiskAssessment;

public class FundingViewModelMapper : Profile
{
    public FundingViewModelMapper()
    {
        CreateMap<GetFundingResponse, FundingViewModel>();
        CreateMap<FundingViewModel, SetFundingRequest>();
    }
}