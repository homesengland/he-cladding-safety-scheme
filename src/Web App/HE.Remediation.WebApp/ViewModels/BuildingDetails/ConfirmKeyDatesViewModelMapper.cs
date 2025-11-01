using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.ConfirmKeyDates.Get;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.ConfirmKeyDates.Set;

namespace HE.Remediation.WebApp.ViewModels.BuildingDetails;

public class ConfirmKeyDatesViewModelMapper : Profile
{
    public ConfirmKeyDatesViewModelMapper()
    {
        CreateMap<GetConfirmKeyDatesResponse, ConfirmKeyDatesViewModel>();
        CreateMap<ConfirmKeyDatesViewModel, SetConfirmKeyDatesRequest>();
    }
}