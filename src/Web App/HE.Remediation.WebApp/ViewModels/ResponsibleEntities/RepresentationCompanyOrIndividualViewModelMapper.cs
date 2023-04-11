using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.RepresentationCompanyOrIndividual.GetRepresentationCompanyOrIndividual;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.RepresentationCompanyOrIndividual.SetRepresentationCompanyOrIndividual;

namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities;

public class RepresentationCompanyOrIndividualViewModelMapper : Profile
{
    public RepresentationCompanyOrIndividualViewModelMapper()
    {
        CreateMap<RepresentationCompanyOrIndividualViewModel, SetRepresentationCompanyOrIndividualRequest>();

        CreateMap<GetRepresentationCompanyOrIndividualResponse, RepresentationCompanyOrIndividualViewModel>();
    }
}