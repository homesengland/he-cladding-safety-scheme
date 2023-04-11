using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities;

namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities;

public class RepresentationCompanyOrIndividualAddressDetailsViewModelMapper : Profile
{
    public RepresentationCompanyOrIndividualAddressDetailsViewModelMapper()
    {
        CreateMap<GetRepresentationCompanyOrIndividualAddressDetailsResponse, RepresentationCompanyOrIndividualAddressDetailsViewModel>();
        CreateMap<RepresentationCompanyOrIndividualAddressDetailsViewModel, SetRepresentationCompanyOrIndividualAddressDetailsRequest>();
    }
}