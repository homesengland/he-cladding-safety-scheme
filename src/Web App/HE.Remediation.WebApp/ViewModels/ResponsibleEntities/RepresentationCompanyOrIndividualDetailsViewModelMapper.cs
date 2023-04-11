using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities;

namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities;

public class RepresentationCompanyOrIndividualDetailsViewModelMapper : Profile
{
    public RepresentationCompanyOrIndividualDetailsViewModelMapper()
    {
        CreateMap<GetRepresentationCompanyOrIndividualDetailsResponse, RepresentationCompanyOrIndividualDetailsViewModel>();

        CreateMap<RepresentationCompanyOrIndividualDetailsViewModel, SetRepresentationCompanyOrIndividualDetailsRequest>();
    }
}