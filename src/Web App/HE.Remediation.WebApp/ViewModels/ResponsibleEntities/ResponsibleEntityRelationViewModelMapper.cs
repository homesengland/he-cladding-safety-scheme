using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.ResponsibleEntityRelation.GetResponsibleEntityRelation;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.ResponsibleEntityRelation.SetResponsibleEntityRelation;

namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities
{
    public class ResponsibleEntityRelationViewModelMapper : Profile
    {
        public ResponsibleEntityRelationViewModelMapper()
        {
            CreateMap<GetResponsibleEntityRelationResponse, ResponsibleEntityRelationViewModel>();
            CreateMap<ResponsibleEntityRelationViewModel, SetResponsibleEntityRelationRequest>();
        }
    }
}
