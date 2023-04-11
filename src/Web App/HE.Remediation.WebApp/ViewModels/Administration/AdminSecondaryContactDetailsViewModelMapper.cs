using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.Administration.SecondaryContactDetails.GetSecondaryContactDetails;
using HE.Remediation.Core.UseCase.Areas.Administration.SecondaryContactDetails.SetSecondaryContactDetails;

namespace HE.Remediation.WebApp.ViewModels.Administration
{
    public class AdminSecondaryContactDetailsViewModelMapper : Profile
    {
        public AdminSecondaryContactDetailsViewModelMapper()
        {            
            CreateMap<GetSecondaryContactDetailsResponse, AdminSecondaryContactDetailsViewModel>();            
            CreateMap<AdminSecondaryContactDetailsViewModel, SetSecondaryContactDetailsRequest>();            
        }
    }
}
