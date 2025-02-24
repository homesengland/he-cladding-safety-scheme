using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.VariationRequest.Descriptions.Get;

namespace HE.Remediation.WebApp.ViewModels.VariationRequest
{
    public class DescriptionsViewModelMapper : Profile
    {
        public DescriptionsViewModelMapper()
        {
            CreateMap<GetDescriptionsResponse, DescriptionsViewModel>();
        }
    }
}
