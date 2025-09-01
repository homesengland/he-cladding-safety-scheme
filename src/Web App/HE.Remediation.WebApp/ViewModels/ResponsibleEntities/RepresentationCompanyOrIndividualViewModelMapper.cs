using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.RepCompanyOrIndividual.GetRepCompanyOrIndividual;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.RepCompanyOrIndividual.SetRepCompanyOrIndividual;

namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities;

public class RepresentationCompanyOrIndividualViewModelMapper : Profile
{
    public RepresentationCompanyOrIndividualViewModelMapper()
    {
        CreateMap<RepresentationCompanyOrIndividualViewModel, SetRepCompanyOrIndividualRequest>();

        CreateMap<GetRepCompanyOrIndividualResponse, RepresentationCompanyOrIndividualViewModel>();
    }
}