using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.Administration.CorrespondenceAddress.GetCorrespondenceAddress;
using HE.Remediation.Core.UseCase.Areas.Administration.CorrespondenceAddress.SetCorrespondenceAddress;
using HE.Remediation.WebApp.ViewModels.Location;

namespace HE.Remediation.WebApp.ViewModels.Administration;

public class CorrespondenceAddressViewModelMapper : Profile
{
    public CorrespondenceAddressViewModelMapper()
    {            
        CreateMap<GetCorrespondenceAddressResponse , CorrespondenceAddressViewModel>();            
        CreateMap<CorrespondenceAddressViewModel, SetCorrespondenceAddressRequest>();            
        CreateMap<GetCorrespondenceAddressResponse, PostCodeManualViewModel> ();
    }
}