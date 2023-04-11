using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.Administration.CorrespondanceAddress.GetCorrespondanceAddress;
using HE.Remediation.Core.UseCase.Areas.Administration.CorrespondanceAddress.SetCorrespondanceAddress;

namespace HE.Remediation.WebApp.ViewModels.Administration;

public class CorrespondanceAddressViewModelMapper : Profile
{
    public CorrespondanceAddressViewModelMapper()
    {            
        CreateMap<GetCorrespondanceAddressResponse , CorrespondanceAddressViewModel>();            
        CreateMap<CorrespondanceAddressViewModel, SetCorrespondanceAddressRequest>();            
    }
}