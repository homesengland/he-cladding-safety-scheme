using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.UploadFireRiskAppraisalReport.GetFireRiskAppraisalReport
{
    public class GetFireRiskAppraisalReportHandler : IRequestHandler<GetFireRiskReportAppraisalReportRequest, IReadOnlyCollection<GetFireRiskAppraisalReportResponse>>
    {
        private readonly IDbConnectionWrapper _dbConnection;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public GetFireRiskAppraisalReportHandler(IDbConnectionWrapper dbConnection, IApplicationDataProvider applicationDataProvider)
        {
            _dbConnection = dbConnection;
            _applicationDataProvider = applicationDataProvider;
        }

        public Task<IReadOnlyCollection<GetFireRiskAppraisalReportResponse>> Handle(GetFireRiskReportAppraisalReportRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var file = _dbConnection.QueryAsync<GetFireRiskAppraisalReportResponse>("GetFireRiskAppraisalForApplication", new { applicationId });

            return file;
        }
    }
}
