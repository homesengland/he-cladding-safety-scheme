using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetChangeProjectDates;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.SetChangeProjectDates;

namespace HE.Remediation.WebApp.ViewModels.PaymentRequest;

public class ChangeProjectDatesViewModelMapper : Profile
{
    public ChangeProjectDatesViewModelMapper()
    {
        CreateMap<GetChangeProjectDatesResponse, ChangeProjectDatesViewModel>();
        CreateMap<ChangeProjectDatesViewModel, SetChangeProjectDatesRequest>();
    }
}
