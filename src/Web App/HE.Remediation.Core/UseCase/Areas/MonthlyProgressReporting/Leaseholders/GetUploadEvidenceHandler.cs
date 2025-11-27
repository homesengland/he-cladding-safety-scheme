using MediatR;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Providers.ApplicationDetailsProvider;
using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.Leaseholders;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.Leaseholders
{
    public partial class GetUploadEvidenceHandler : IRequestHandler<GetUploadEvidenceRequest, GetUploadEvidenceResponse>
    {
        private readonly IProgressReportingLeaseholdersRepository _progressReportingLeaseholdersRepository;
        private readonly IApplicationDetailsProvider _detailsProvider;
        private readonly IApplicationDataProvider _dataProvider;

        public GetUploadEvidenceHandler(
            IApplicationDetailsProvider detailsProvider,
            IApplicationDataProvider dataProvider,
            IProgressReportingLeaseholdersRepository progressReportingLeaseholdersRepository
        )
        {
            _detailsProvider = detailsProvider;
            _dataProvider = dataProvider;
            _progressReportingLeaseholdersRepository = progressReportingLeaseholdersRepository;
        }

        public async Task<GetUploadEvidenceResponse> Handle(GetUploadEvidenceRequest request, CancellationToken cancellationToken)
        {
            var details = await _detailsProvider.GetApplicationDetails();
            var progressReportId = _dataProvider.GetProgressReportId();
            var parameters = new GetProgressReportLeaseholderCommunicationParameters()
            {
                ApplicationId = details.ApplicationId,
                ProgressReportId = progressReportId
            };
            var result = await _progressReportingLeaseholdersRepository.GetProgressReportLeaseholderCommunication(parameters);
            var files = await _progressReportingLeaseholdersRepository.GetProgressReportLeaseholderCommunicationFiles(parameters);

            return new GetUploadEvidenceResponse()
            {
                ApplicationReferenceNumber = details.ApplicationReferenceNumber,
                BuildingName = details.BuildingName,
                AddedFiles = [.. files.Select(f => new GetUploadEvidenceResponse.FileResult(f.Id, f.Name, f.Size))]
            };
        }
    }

    public class GetUploadEvidenceRequest : IRequest<GetUploadEvidenceResponse> {}

    public class GetUploadEvidenceResponse
    {
        public string ApplicationReferenceNumber { get; set; }
        public string BuildingName { get; set; }
        public List<FileResult> AddedFiles { get; set; }

        public record FileResult(Guid Id, string Name, int FileSize);
    }

}
