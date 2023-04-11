using AutoMapper;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities;

namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities
{
    public class ResponsibleEntityPrimaryContactDetailsViewModelMapper : Profile
    {
        public ResponsibleEntityPrimaryContactDetailsViewModelMapper()
        {
            CreateMap<GetResponsibleEntityPrimaryContactDetailsResponse, ResponsibleEntityPrimaryContactDetailsViewModel>()
                .ForMember(x => 
                    x.IsIndividuelOrganisationSubType, o =>
                    o.MapFrom(s => 
                        s.OrganisationType == EApplicationResponsibleEntityOrganisationType.Other 
                        && s.OrganisationSubType.HasValue 
                        && s.OrganisationSubType == EApplicationResponsibleEntityOrganisationSubType.Individual));
            CreateMap<ResponsibleEntityPrimaryContactDetailsViewModel, SetResponsibleEntityPrimaryContactDetailsRequest>();
        }
    }
}
