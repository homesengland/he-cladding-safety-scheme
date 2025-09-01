using HE.Remediation.Core.Enums;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.EvidenceOfThirdPartyContribution.EvidenceDetails
{
    public class GetEvidenceSubmissionUploadRequest : IRequest<GetEvidenceSubmissionUploadResponse>
    {
        public GetEvidenceSubmissionUploadRequest(EClosingReportFileType uploadType)
        {
            UploadType = uploadType;
        }
        public EClosingReportFileType UploadType { get; }
    }
}
