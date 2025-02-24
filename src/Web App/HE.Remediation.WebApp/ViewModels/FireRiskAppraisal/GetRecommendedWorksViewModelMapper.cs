using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.RecommendedWorks.GetRecommendedWorks;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.RecommendedWorks.SetRecommendedWorks;

namespace HE.Remediation.WebApp.ViewModels.FireRiskAppraisal;

public class GetRecommendedWorksViewModelMapper: Profile
{
    public GetRecommendedWorksViewModelMapper()
    {
        CreateMap<GetRecommendedWorksResponse,GetRecommendedWorksViewModel>();   
        CreateMap<GetRecommendedWorksViewModel, SetRecommendedWorksRequest > ();
    }
}
