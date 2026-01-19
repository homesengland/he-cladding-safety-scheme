using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.ResponsibleEntityCompanyRelationDetails;

namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities
{
    public class ResponsibleEntityCompanyRelationDetailsViewModelMapper : Profile
    {
        public ResponsibleEntityCompanyRelationDetailsViewModelMapper()
        {
            CreateMap<GetResponsibleEntityCompanyRelationDetailsResponse, ResponsibleEntityCompanyRelationDetailsViewModel>();
            CreateMap<ResponsibleEntityCompanyRelationDetailsViewModel, SetResponsibleEntityCompanyRelationDetailsRequest>();
        }
    }
}
