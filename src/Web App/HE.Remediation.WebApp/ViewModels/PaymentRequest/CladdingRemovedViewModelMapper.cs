using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetCladdingRemoved;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.SetCladdingRemoved;

namespace HE.Remediation.WebApp.ViewModels.PaymentRequest;

public class CladdingRemovedViewModelMapper : Profile
{
    public CladdingRemovedViewModelMapper()
    {
        CreateMap<GetCladdingRemovedResponse, CladdingRemovedViewModel>();
        CreateMap<CladdingRemovedViewModel, SetCladdingRemovedRequest>();
    }
}
