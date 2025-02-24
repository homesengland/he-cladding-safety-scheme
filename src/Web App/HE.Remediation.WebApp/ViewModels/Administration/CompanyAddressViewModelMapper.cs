using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.Administration.CompanyAddress.GetCompanyAddressForCurrentUser;
using HE.Remediation.Core.UseCase.Areas.Administration.CompanyAddress.SetCompanyAddressForCurrentUser;
using HE.Remediation.WebApp.ViewModels.Location;

namespace HE.Remediation.WebApp.ViewModels.Administration;

public class CompanyAddressViewModelMapper : Profile
{
    public CompanyAddressViewModelMapper()
    {
        CreateMap<CompanyAddressViewModel, SetCompanyAddressForCurrentUserRequest>();
        CreateMap<GetCompanyAddressForCurrentUserResponse, CompanyAddressViewModel>();
        CreateMap<GetCompanyAddressForCurrentUserResponse, PostCodeManualViewModel>();
        CreateMap<PostCodeSelectionViewModel, SetCompanyAddressForCurrentUserRequest>();
    }
}