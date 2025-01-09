using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.InterimMeasures.SetInterimMeasures
{
    public class SetInterimMeasuresHandler : IRequestHandler<SetInterimMeasuresRequest>
    {
        private readonly IDbConnectionWrapper _dbConnectionWrapper;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public SetInterimMeasuresHandler(IDbConnectionWrapper dbConnectionWrapper, IApplicationDataProvider applicationDataProvider)
        {
            _dbConnectionWrapper = dbConnectionWrapper;
            _applicationDataProvider = applicationDataProvider;
        }

        public async Task<Unit> Handle(SetInterimMeasuresRequest request, CancellationToken cancellationToken)
        {
            await SetInterimMeasures(request);
            return Unit.Value;
        }

        private async Task SetInterimMeasures(SetInterimMeasuresRequest request)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            await _dbConnectionWrapper.ExecuteAsync("UpsertInterimMeasures", new
            {
                ApplicationId = applicationId,
                request.BuildingInterimMeasures,
                request.BuildingInterimMeasuresText,
                request.EvacuationStrategyType,
                request.EvacuationStrategyText,
                request.NumberOfStairwellsPrompt,
                request.NumberOfStairwells,
                request.ExternalWallAndBalconiesPolicy,
                request.FireAndResueAccessRestrictions,
                request.FireAndResueAccessRestrictionsText
            });

            var interimMeasuresId = await _dbConnectionWrapper.QuerySingleOrDefaultAsync<Guid>("GetFireRiskAppraisalInterimMeasuresId", new { applicationId });

            var buildingInterimMeasureTypes = !request.BuildingInterimMeasuresTypes.Any()
                ? null
                : string.Join(",", request.BuildingInterimMeasuresTypes.Select(x => (int)x));

            var buildingInterimMeasures = request.BuildingInterimMeasures == EYesNoNonBoolean.Yes
                ? true
                : false;

            var otherInterimMeasure = request.BuildingInterimMeasuresTypes.Contains(EInterimMeasuresType.Other);

            if (interimMeasuresId != Guid.Empty)
            {
                await _dbConnectionWrapper.ExecuteAsync("InsertBuildingInterimMeasureTypes", new
                {
                    interimMeasuresId,
                    buildingInterimMeasureTypes,
                    buildingInterimMeasures,
                    otherInterimMeasure,

                }); ;
            }
        }
    }

    public class SetInterimMeasuresRequest : IRequest
    {
        public EYesNoNonBoolean BuildingInterimMeasures { get; set; }
        public string BuildingInterimMeasuresText { get; set; }
        public EEvacuationStrategy EvacuationStrategyType { get; set; }
        public string EvacuationStrategyText { get; set; }
        public ENoYes? NumberOfStairwellsPrompt { get; set; }
        public int? NumberOfStairwells { get; set; }
        public EYesNoNonBoolean ExternalWallAndBalconiesPolicy { get; set; }
        public EYesNoNonBoolean FireAndResueAccessRestrictions { get; set; }
        public string FireAndResueAccessRestrictionsText { get; set; }
        public IEnumerable<EInterimMeasuresType> BuildingInterimMeasuresTypes { get; set; }
    }
}
