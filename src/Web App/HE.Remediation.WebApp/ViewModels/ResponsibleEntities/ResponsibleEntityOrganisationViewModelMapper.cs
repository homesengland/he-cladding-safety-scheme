using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities;

namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities
{
    public class ResponsibleEntityOrganisationViewModelMapper : Profile
    {
        public ResponsibleEntityOrganisationViewModelMapper()
        {
            CreateMap<GetResponsibleEntityOrganisationDetailsResponse, ResponsibleEntityOrganisationViewModel>();
            CreateMap<ResponsibleEntityOrganisationViewModel, SetResponsibleEntityOrganisationDetailsRequest>();
        }
    }
}
