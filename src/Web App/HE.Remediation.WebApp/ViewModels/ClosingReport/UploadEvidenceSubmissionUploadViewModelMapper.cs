using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.EvidenceOfThirdPartyContribution.EvidenceDetails;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.EvidenceOfThirdPartyContribution.UploadFileEvidence;

namespace HE.Remediation.WebApp.ViewModels.ClosingReport
{
    public class UploadEvidenceSubmissionUploadViewModelMapper : Profile
    {
        public UploadEvidenceSubmissionUploadViewModelMapper()
        {
            CreateMap<GetEvidenceDetailResponse, UploadEvidenceSubmissionUploadViewModel>();
            CreateMap<UploadEvidenceSubmissionUploadViewModel, AddThirdPartyEvidenceFileRequest>();
        }
    }
}
