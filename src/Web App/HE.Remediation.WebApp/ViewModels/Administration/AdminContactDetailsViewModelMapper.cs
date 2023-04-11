using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.Administration.ContactDetails.GetContactDetails;
using HE.Remediation.Core.UseCase.Areas.Administration.ContactDetails.SetContactDetails;

namespace HE.Remediation.WebApp.ViewModels.Administration
{
    public class AdminContactDetailsViewModelMapper : Profile
    {
        public AdminContactDetailsViewModelMapper()
        {            
            CreateMap<GetContactDetailsResponse, AdminContactDetailsViewModel>();            
            CreateMap<AdminContactDetailsViewModel, SetContactDetailsRequest>();            

        }
    }
}
