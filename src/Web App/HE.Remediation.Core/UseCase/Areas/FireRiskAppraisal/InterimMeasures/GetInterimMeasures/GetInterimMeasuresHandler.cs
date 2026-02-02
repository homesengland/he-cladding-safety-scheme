using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.InterimMeasures.GetInterimMeasures
{
    public class GetInterimMeasuresHandler : IRequestHandler<GetInterimMeasuresRequest, GetInterimMeasuresResponse>
    {
        private readonly IDbConnectionWrapper _dbConnectionWrapper;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public GetInterimMeasuresHandler(IDbConnectionWrapper dbConnectionWrapper, IApplicationDataProvider applicationDataProvider)
        {
            _dbConnectionWrapper = dbConnectionWrapper;
            _applicationDataProvider = applicationDataProvider;
        }

        public async ValueTask<GetInterimMeasuresResponse> Handle(GetInterimMeasuresRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var interimMeasures = await _dbConnectionWrapper.QuerySingleOrDefaultAsync<FRAEWInterimMeasures>("GetFireRiskAssessmentInterimMeasures", new { applicationId });

            if (interimMeasures is not null)
            {
                var buildingInterimMeasuresTypes = await _dbConnectionWrapper.QueryAsync<EInterimMeasuresType>("GetFireRiskAssessmentBuildingInterimMeasuresTypes", new
                {
                    ApplicationId = _applicationDataProvider.GetApplicationId()
                });

                var response = new GetInterimMeasuresResponse
                {
                    BuildingInterimMeasures = (EYesNoNonBoolean)interimMeasures.BuildingInterimMeasuresId,
                    BuildingInterimMeasuresText = interimMeasures.BuildingInterimMeasuresText,
                    EvacuationStrategyType = (EEvacuationStrategy)interimMeasures.EvacuationStrategyTypeId,
                    EvacuationStrategyText = interimMeasures.EvacuationStrategyText,
                    NumberOfStairwellsPrompt = (ENoYes)interimMeasures.NumberOfStairwellsPrompt,
                    NumberOfStairwells = interimMeasures.NumberOfStairwells,
                    ExternalWallAndBalconiesPolicy = (EYesNoNonBoolean)interimMeasures.ExternalWallAndBalconiesPolicyId,
                    FireAndResueAccessRestrictions = (EYesNoNonBoolean)interimMeasures.FireAndResueAccessRestrictionsId,
                    FireAndResueAccessRestrictionsText = interimMeasures.FireAndResueAccessRestrictions,
                    BuildingInterimMeasuresTypes = buildingInterimMeasuresTypes
                };

                return response;
            }

            return new GetInterimMeasuresResponse();
        }
    }

    public class GetInterimMeasuresRequest : IRequest<GetInterimMeasuresResponse>
    {
        public GetInterimMeasuresRequest() { }

        public static readonly GetInterimMeasuresRequest Request = new();
    }

    public class GetInterimMeasuresResponse
    {
        public EYesNoNonBoolean? BuildingInterimMeasures { get; set; }
        public string BuildingInterimMeasuresText { get; set; }
        public EEvacuationStrategy? EvacuationStrategyType { get; set; }
        public string EvacuationStrategyText { get; set; }
        public ENoYes? NumberOfStairwellsPrompt { get; set; }
        public int? NumberOfStairwells { get; set; }
        public EYesNoNonBoolean? ExternalWallAndBalconiesPolicy { get; set; }
        public EYesNoNonBoolean? FireAndResueAccessRestrictions { get; set; }
        public string FireAndResueAccessRestrictionsText { get; set; }
        public IEnumerable<EInterimMeasuresType> BuildingInterimMeasuresTypes { get; set; }
    }
}
