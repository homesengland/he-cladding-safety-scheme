using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.EvidenceOfThirdPartyContribution.ReasonForNoContributions.Get;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.EvidenceOfThirdPartyContribution.ReasonForNoContributions.Set;

namespace HE.Remediation.WebApp.ViewModels.ClosingReport
{
    public class ReasonNoThirdPartyContributionsViewModelMapper : Profile
    {
        public ReasonNoThirdPartyContributionsViewModelMapper()
        {
            CreateMap<GetReasonForNoContributionsResponse, ReasonNoThirdPartyContributionsViewModel>();
            CreateMap<ReasonNoThirdPartyContributionsViewModel, SetReasonForNoContributionsRequest>();
        }
    }
}
