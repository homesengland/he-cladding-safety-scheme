using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.Representative.GetRepresentativeType;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.Representative.SetRepresentativeType;

namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities;

public class RepresentativeTypeViewModelMapper : Profile
{
    public RepresentativeTypeViewModelMapper()
    {
        CreateMap<RepresentativeTypeViewModel, SetRepresentativeTypeRequest>();

        CreateMap<GetRepresentativeTypeResponse, RepresentativeTypeViewModel>();
    }
}