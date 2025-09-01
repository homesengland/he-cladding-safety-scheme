using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.FireRiskAssessment;

namespace HE.Remediation.WebApp.ViewModels.FireRiskAssessment;

public class FundingViewModelMapper : Profile
{
    public FundingViewModelMapper()
    {
        CreateMap<GetFundingResponse, FundingViewModel>();
        CreateMap<FundingViewModel, SetFundingRequest>();
    }
}