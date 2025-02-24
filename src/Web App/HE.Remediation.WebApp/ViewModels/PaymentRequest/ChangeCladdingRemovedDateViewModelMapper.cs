using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetChangeCladdingRemovedDate;
using HE.Remediation.Core.UseCase.Areas.PaymentRequest.SetChangeCladdingRemovedDate;

namespace HE.Remediation.WebApp.ViewModels.PaymentRequest;

public class ChangeCladdingRemovedDateViewModelMapper : Profile
{
    public ChangeCladdingRemovedDateViewModelMapper()
    {
        CreateMap<GetChangeCladdingRemovedDateResponse, ChangeCladdingRemovedDateViewModel>();        
        CreateMap<ChangeCladdingRemovedDateViewModel, SetChangeCladdingRemovedDateRequest>();
    }
}
