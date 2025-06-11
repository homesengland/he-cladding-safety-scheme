using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.UploadFireRiskAssessmentReport.GetFireRiskAssessmentReport
{
    public class GetFireRiskAssessmentReportHandler : IRequestHandler<GetFireRiskReportAssessmentReportRequest, GetFireRiskAssessmentReportResponse>
    {
        private readonly IDbConnectionWrapper _dbConnection;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public GetFireRiskAssessmentReportHandler(IDbConnectionWrapper dbConnection, IApplicationDataProvider applicationDataProvider)
        {
            _dbConnection = dbConnection;
            _applicationDataProvider = applicationDataProvider;
        }

        public async Task<GetFireRiskAssessmentReportResponse> Handle(GetFireRiskReportAssessmentReportRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            GetFireRiskAssessmentReportResponse result = null;

            var file = await _dbConnection.QueryAsync<GetFireRiskAssessmentReportResponse, FileResult, GetFireRiskAssessmentReportResponse>("GetFireRiskAssessmentForApplication",
                (fraType, fraFile) =>
                {
                    result ??= fraType;

                    if (fraFile is not null && fraFile.Id != Guid.Empty)
                    {
                        result.AddedFra = fraFile;
                    }

                    return result;

                }, new { applicationId });

            return result;
        }
    }
}
