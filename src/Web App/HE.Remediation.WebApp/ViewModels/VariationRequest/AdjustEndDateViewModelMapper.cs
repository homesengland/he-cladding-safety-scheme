using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.VariationRequest.Timescale.Get;
using HE.Remediation.Core.UseCase.Areas.VariationRequest.Timescale.Set;

namespace HE.Remediation.WebApp.ViewModels.VariationRequest
{
    public class AdjustEndDateViewModelMapper : Profile
    {
        public AdjustEndDateViewModelMapper()
        {
            CreateMap<GetAdjustEndDateResponse, AdjustEndDateViewModel>();
            CreateMap<AdjustEndDateViewModel, SetAdjustEndDateRequest>();
        }
    }
}
