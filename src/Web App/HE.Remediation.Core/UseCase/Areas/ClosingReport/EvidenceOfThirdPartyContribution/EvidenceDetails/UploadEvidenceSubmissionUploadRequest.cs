using HE.Remediation.Core.Enums;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.EvidenceOfThirdPartyContribution.EvidenceDetails
{
    public class UploadEvidenceSubmissionUploadRequest : IRequest<UploadEvidenceSubmissionUploadResponse>
    {
        public UploadEvidenceSubmissionUploadRequest(EClosingReportFileType uploadType)
        {
            UploadType = uploadType;
        }

        public EClosingReportFileType UploadType { get; }
    }
}
