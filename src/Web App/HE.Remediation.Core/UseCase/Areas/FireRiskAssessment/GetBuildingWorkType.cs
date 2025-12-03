using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAssessment
{
    public class GetBuildingWorkTypeHandler : IRequestHandler<GetBuildingWorkTypeRequest, GetBuildingWorkTypeResponse>
    {
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IFireRiskAssessmentRepository _fireRiskAssessmentRepository;

        public GetBuildingWorkTypeHandler(IApplicationDataProvider applicationDataProvider, IFireRiskAssessmentRepository fireRiskAssessmentRepository)
        {
            _applicationDataProvider = applicationDataProvider;
            _fireRiskAssessmentRepository = fireRiskAssessmentRepository;
        }

        public async Task<GetBuildingWorkTypeResponse> Handle(GetBuildingWorkTypeRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var applicationId = _applicationDataProvider.GetApplicationId();

            var fraBuildingWorkType = await _fireRiskAssessmentRepository.GetFraBuildingWorkType(applicationId);
            var visitedCheckYourAnswers = await _fireRiskAssessmentRepository.GetFraVisitedCheckYourAnswers(applicationId);

            return new GetBuildingWorkTypeResponse
            {
                ApplicationScheme = _applicationDataProvider.GetApplicationScheme(),
                FraBuildingWorkTypeId = fraBuildingWorkType.HasValue ? (EFraBuildingWorkType)fraBuildingWorkType.Value : null,
                VisitedCheckYourAnswers = visitedCheckYourAnswers
            };
        }
    }

    public class GetBuildingWorkTypeRequest : IRequest<GetBuildingWorkTypeResponse>
    {
        private GetBuildingWorkTypeRequest()
        {
        }

        public static readonly GetBuildingWorkTypeRequest Request = new();
    }

    public class GetBuildingWorkTypeResponse
    {
        public EApplicationScheme ApplicationScheme { get; set; }
        public EFraBuildingWorkType? FraBuildingWorkTypeId { get; set; }
        public bool VisitedCheckYourAnswers { get; set; }
    }
}
