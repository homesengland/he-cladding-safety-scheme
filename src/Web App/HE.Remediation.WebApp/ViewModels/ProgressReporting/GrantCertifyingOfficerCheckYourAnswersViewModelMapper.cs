using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting;

namespace HE.Remediation.WebApp.ViewModels.ProgressReporting;

public class GrantCertifyingOfficerCheckYourAnswersViewModelMapper : Profile
{
    public GrantCertifyingOfficerCheckYourAnswersViewModelMapper()
    {
        CreateMap<GetGrantCertifyingOfficerCheckYourAnswersResponse, GrantCertifyingOfficerCheckYourAnswersViewModel>();
    }
}