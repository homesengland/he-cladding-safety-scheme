using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.RepresentativeBasedInUk.GetRepresentativeBasedInUk;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.RepresentativeBasedInUk.SetRepresentativeBasedInUk;

namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities;

public class BasedInUkViewModelMapper : Profile
{
    public BasedInUkViewModelMapper()
    {
        CreateMap<BasedInUkViewModel, SetRepresentativeBasedInUkRequest>();

        CreateMap<GetRepresentativeBasedInUkResponse, BasedInUkViewModel>();
    }
}