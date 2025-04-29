using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.UploadFireRiskAppraisalReport.GetFireRiskAppraisalReport
{
    public class GetFireRiskAppraisalReportHandler : IRequestHandler<GetFireRiskReportAppraisalReportRequest, GetFireRiskAppraisalReportResponse>
    {
        private readonly IDbConnectionWrapper _dbConnection;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public GetFireRiskAppraisalReportHandler(IDbConnectionWrapper dbConnection, IApplicationDataProvider applicationDataProvider)
        {
            _dbConnection = dbConnection;
            _applicationDataProvider = applicationDataProvider;
        }

        public async Task<GetFireRiskAppraisalReportResponse> Handle(GetFireRiskReportAppraisalReportRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var result =  new GetFireRiskAppraisalReportResponse();

            var file = await _dbConnection.QueryAsync<FileResult, FileResult, FileResult, GetFireRiskAppraisalReportResponse>("GetFireRiskAppraisalForApplication", 
            (fraewFile, fraewSummary, fraReport) =>
            {
                if(fraewFile is not null && fraewFile.Id != Guid.Empty)
                {
                    result.FraewFile = fraewFile;
                }

                if (fraewSummary is not null && fraewSummary.Id != Guid.Empty)
                {
                    result.SummaryFile = fraewSummary;
                }

                if (fraReport is not null && fraReport.Id != Guid.Empty)
                {
                    result.FraReportFile = fraReport;
                }

                return result;

            }, new { applicationId });

            return result;
        }
    }
}
