using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.OrganisationManagement.OrganisationDetails;

namespace HE.Remediation.WebApp.ViewModels.OrganisationManagement;

public class OrganisationDetailsViewModelMapper: Profile
{
    public OrganisationDetailsViewModelMapper()
    {        
        CreateMap<OrganisationDetailsResponse, OrganisationDetailsViewModel>();
        CreateMap<OrganisationDetailsViewModel, OrganisationDetailsRequest>();
    }    
}
