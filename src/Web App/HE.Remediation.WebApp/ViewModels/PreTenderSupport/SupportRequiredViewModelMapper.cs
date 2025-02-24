using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.PreTenderSupport.SupportRequired.GetSupportRequired;
using HE.Remediation.Core.UseCase.Areas.PreTenderSupport.SupportRequired.SetSupportRequired;

namespace HE.Remediation.WebApp.ViewModels.PreTenderSupport
{
    public class SupportRequiredViewModelMapper : Profile
    {
        public SupportRequiredViewModelMapper()
        {
            CreateMap<GetSupportRequiredResponse, SupportRequiredViewModel>();

            CreateMap<SupportRequiredViewModel, SetSupportRequiredRequest>();
        }
    }
}