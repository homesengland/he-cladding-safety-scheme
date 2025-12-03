using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.RefurbishmentCompletionDate.Get;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.RefurbishmentCompletionDate.Set;

namespace HE.Remediation.WebApp.ViewModels.BuildingDetails;

public class RefurbishmentCompletionDateViewModelMapper : Profile
{
    public RefurbishmentCompletionDateViewModelMapper()
    {
        CreateMap<GetRefurbishmentCompletionDateResponse, RefurbishmentCompletionDateViewModel>();
        CreateMap<RefurbishmentCompletionDateViewModel, SetRefurbishmentCompletionDateRequest>();
    }
}