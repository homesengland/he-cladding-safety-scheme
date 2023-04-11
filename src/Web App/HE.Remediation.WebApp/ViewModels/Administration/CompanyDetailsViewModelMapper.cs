using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.Administration.CompanyDetails.GetCompanyDetailsForCurrentUser;
using HE.Remediation.Core.UseCase.Areas.Administration.CompanyDetails.SetCompanyDetailsForCurrentUser;

namespace HE.Remediation.WebApp.ViewModels.Administration;

public class CompanyDetailsViewModelMapper : Profile
{
    public CompanyDetailsViewModelMapper()
    {
        CreateMap<CompanyDetailsViewModel, SetCompanyDetailsForCurrentUserRequest>();
        CreateMap<GetCompanyDetailsForCurrentUserResponse, CompanyDetailsViewModel>();
    }
}