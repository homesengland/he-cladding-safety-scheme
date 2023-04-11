using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.AlternativeFundingRoutes.PursuedSourcesFunding.GetFundingStillPursuing;
using HE.Remediation.Core.UseCase.Areas.AlternativeFundingRoutes.PursuedSourcesFunding.SetFundingStillPursuing;

namespace HE.Remediation.WebApp.ViewModels.AlternativeFundingRoutes
{
    public class FundingStillPursuingViewModelMapper : Profile
    {
        public FundingStillPursuingViewModelMapper()
        {
            CreateMap<FundingStillPursuingViewModel, SetFundingStillPursuingRequest>();
            CreateMap<GetFundingStillPursuingResponse, FundingStillPursuingViewModel>();
        }
    }
}