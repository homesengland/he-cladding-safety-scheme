using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.KeyDates.Remediation;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.KeyDates;

public class RemediationViewModelMapper : Profile
{
    public RemediationViewModelMapper()
    {
        CreateMap<GetRemediationResponse, RemediationViewModel>();
        CreateMap<RemediationViewModel, SetRemediationRequest>();
    }
}
