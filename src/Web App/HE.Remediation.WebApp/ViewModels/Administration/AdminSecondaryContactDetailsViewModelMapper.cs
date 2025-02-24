using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.Administration.SecondaryContactDetails.GetSecondaryContactDetails;
using HE.Remediation.Core.UseCase.Areas.Administration.SecondaryContactDetails.SetSecondaryContactDetails;
using HE.Remediation.Core.UseCase.Areas.Application.ExistingApplication.GetExistingApplication;
using HE.Remediation.WebApp.ViewModels.Application;

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
