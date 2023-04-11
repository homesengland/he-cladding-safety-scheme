using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities;
using HE.Remediation.WebApp.ViewModels.Location;

namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities;

public class RepresentationCompanyOrIndividualAddressDetailsViewModelMapper : Profile
{
    public RepresentationCompanyOrIndividualAddressDetailsViewModelMapper()
    {
        CreateMap<GetRepresentationCompanyOrIndividualAddressDetailsResponse, RepresentationCompanyOrIndividualAddressDetailsViewModel>();
        CreateMap<RepresentationCompanyOrIndividualAddressDetailsViewModel, SetRepresentationCompanyOrIndividualAddressDetailsRequest>();
        CreateMap<GetRepresentationCompanyOrIndividualAddressDetailsResponse, SetResponsibleEntityCompanyAddressManualRequest>();
        //CreateMap<PostCodeManualViewModel, PostCodeManualViewModel>();
    }
}