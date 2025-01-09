using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetSubContractors;
using HE.Remediation.WebApp.ViewModels.ProgressReporting;

namespace HE.Remediation.WebApp.ViewModels.PaymentRequest.SubcontractorSurvey;

public class SubContractorsViewModelMapper : Profile
{
    public SubContractorsViewModelMapper()
    {
        CreateMap<GetSubContractorsResponse, SubContractorsViewModel>();
    }
}