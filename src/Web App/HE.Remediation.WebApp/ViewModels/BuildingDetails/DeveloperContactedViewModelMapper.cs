using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.DeveloperContacted.GetDeveloperContacted;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.DeveloperContacted.SetDeveloperContacted;

namespace HE.Remediation.WebApp.ViewModels.BuildingDetails;

public class DeveloperContactedViewModelMapper : Profile
{
    public DeveloperContactedViewModelMapper()
    {
        CreateMap<DeveloperContactedViewModel, SetDeveloperContactedRequest>();

        CreateMap<GetDeveloperContactedResponse, DeveloperContactedViewModel>();
    }
}