using AutoMapper;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.ConstructionCompletionDate.Get;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.ConstructionCompletionDate.Set;

namespace HE.Remediation.WebApp.ViewModels.BuildingDetails;

public class ConstructionCompletionDateViewModelMapper : Profile
{
    public ConstructionCompletionDateViewModelMapper()
    {
        CreateMap<GetConstructionCompletionDateResponse, ConstructionCompletionDateViewModel>();
        CreateMap<ConstructionCompletionDateViewModel, SetConstructionCompletionDateRequest>();
    }
}
