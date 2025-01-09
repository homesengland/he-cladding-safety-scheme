using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.VariationRequest.Confirmation.Get;
using HE.Remediation.Core.UseCase.Areas.VariationRequest.Confirmation.Set;

namespace HE.Remediation.WebApp.ViewModels.VariationRequest
{
    public class ConfirmationViewModelMapper : Profile
    {
        public ConfirmationViewModelMapper()
        {
            CreateMap<GetConfirmationResponse, ConfirmationViewModel>();
            CreateMap<ConfirmationViewModel, SetConfirmationRequest>();
        }
    }
}
