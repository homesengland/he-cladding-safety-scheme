using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.LeadDesignerCompanyDetails.GetLeadDesignerCompanyDetails;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.LeadDesignerCompanyDetails.SetLeadDesignerCompanyDetails;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class LeadDesignerCompDetailsViewModelMapper : Profile
{
    public LeadDesignerCompDetailsViewModelMapper ()
    {
        CreateMap<GetLeadDesignerCompanyDetailsResponse, LeadDesignerCompDetailsViewModel>();
        CreateMap<LeadDesignerCompDetailsViewModel, SetLeadDesignerCompanyDetailsRequest>();
    }
}
