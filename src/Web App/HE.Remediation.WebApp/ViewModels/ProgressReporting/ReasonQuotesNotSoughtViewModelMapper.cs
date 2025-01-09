using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.ReasonQuotesNotSought.GetReasonQuotesNotSought;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.ReasonQuotesNotSought.SetReasonQuotesNotSought;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class ReasonQuotesNotSoughtViewModelMapper : Profile
{
    public ReasonQuotesNotSoughtViewModelMapper()
    {
        CreateMap<GetReasonQuotesNotSoughtResponse, ReasonQuotesNotSoughtViewModel>();
        CreateMap<ReasonQuotesNotSoughtViewModel, SetReasonQuotesNotSoughtRequest>();
    }
}
