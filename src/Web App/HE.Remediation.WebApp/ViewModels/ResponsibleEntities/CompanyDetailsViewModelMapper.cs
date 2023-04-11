using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.CompanyDetails.GetCompanyDetails;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.CompanyDetails.SetCompanyDetails;

namespace HE.Remediation.WebApp.ViewModels.ResponsibleEntities
{
    public class CompanyDetailsViewModelMapper : Profile
    {
        public CompanyDetailsViewModelMapper()
        {
            CreateMap<GetCompanyDetailsResponse, CompanyDetailsViewModel>();
            CreateMap<CompanyDetailsViewModel, SetCompanyDetailsRequest>();
        }
    }
}
