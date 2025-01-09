using AutoMapper;
using HE.Remediation.Core.Data.StoredProcedureResults.VariationRequest;
using HE.Remediation.Core.UseCase.Areas.VariationRequest.Costs.Get;

namespace HE.Remediation.WebApp.ViewModels.VariationRequest
{
    public class CostsViewModelMapper : Profile
    {
        public CostsViewModelMapper()
        {
            CreateMap<GetCostsResponse, CostsViewModel>();
        }
    }
}
