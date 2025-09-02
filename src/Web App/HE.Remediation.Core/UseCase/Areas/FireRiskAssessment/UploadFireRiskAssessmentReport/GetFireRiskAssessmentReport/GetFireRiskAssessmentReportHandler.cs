using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAssessment.UploadFireRiskAssessmentReport.GetFireRiskAssessmentReport
{
    public class GetFireRiskAssessmentReportHandler : IRequestHandler<GetFireRiskReportAssessmentReportRequest, GetFireRiskAssessmentReportResponse>
    {
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IFireRiskAssessmentRepository _fireRiskAssessmentRepository;

        public GetFireRiskAssessmentReportHandler(
            IApplicationDataProvider applicationDataProvider, 
            IFireRiskAssessmentRepository fireRiskAssessmentRepository)
        {
            _applicationDataProvider = applicationDataProvider;
            _fireRiskAssessmentRepository = fireRiskAssessmentRepository;
        }

        public async Task<GetFireRiskAssessmentReportResponse> Handle(GetFireRiskReportAssessmentReportRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var file = await _fireRiskAssessmentRepository.GetFireRiskAssessmentForApplication(applicationId);
            
            var visitedCheckYourAnswers = await _fireRiskAssessmentRepository.GetFraVisitedCheckYourAnswers(applicationId);

            return new GetFireRiskAssessmentReportResponse
            {
                FireRiskAssessmentType = file.FireRiskAssessmentType,
                AddedFra = file.AddedFra is not null
                    ? new FileResult
                    {
                        Id = file.AddedFra.Id,
                        Name = file.AddedFra.Name,
                        Size = file.AddedFra.Size,
                        Extension = file.AddedFra.Extension
                    }
                    : null,
                VisitedCheckYourAnswers = visitedCheckYourAnswers
            };
        }
    }
}
