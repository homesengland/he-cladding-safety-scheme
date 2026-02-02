using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters.BuildingDetails;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.ConstructionCompletionDate.Set;

internal class SetConstructionCompletionDateHandler : IRequestHandler<SetConstructionCompletionDateRequest>
{
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IApplicationDataProvider _applicationDataProvider;

    public SetConstructionCompletionDateHandler(IBuildingDetailsRepository buildingDetailsRepository, IApplicationDataProvider applicationDataProvider)
    {
        _buildingDetailsRepository = buildingDetailsRepository;
        _applicationDataProvider = applicationDataProvider;
    }

    public async ValueTask<Unit> Handle(SetConstructionCompletionDateRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        if (request.ConstructionCompletionDateMonth != null && request.ConstructionCompletionDateYear != null)
        {
            await _buildingDetailsRepository.UpdateConstructionCompletionDate(new UpdateConstructionCompletionDateParameters
            {
                ApplicationId = applicationId,
                ConstructionCompletionDate = GetDate(request.ConstructionCompletionDateMonth, request.ConstructionCompletionDateYear)
            });
        }
        else
        {
            await _buildingDetailsRepository.UpdateConstructionCompletionDate(new UpdateConstructionCompletionDateParameters
            {
                ApplicationId = applicationId,
                ConstructionCompletionDate = null
            });
        }

        return Unit.Value;
    }

    private DateTime? GetDate(int? month, int? year)
    {
        return month is not null && year is not null
            ? new DateTime(year.Value, month.Value, 1).AddMonths(1).AddDays(-1)
            : null;
    }
}