using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.AlternativeFundingRoutes.PursuedSourcesFunding.GetPursuedSourcesFunding;
using HE.Remediation.Core.UseCase.Areas.AlternativeFundingRoutes.PursuedSourcesFunding.SetPursuedSourcesFunding;

namespace HE.Remediation.WebApp.ViewModels.AlternativeFundingRoutes
{
    public class PursuedSourcesFundingViewModelMapper : Profile
    {
        public PursuedSourcesFundingViewModelMapper()
        {
            CreateMap<PursuedSourcesFundingViewModel, SetPursuedSourcesFundingRequest>();
            CreateMap<GetPursuedSourcesFundingResponse, PursuedSourcesFundingViewModel>();
        }
    }
}