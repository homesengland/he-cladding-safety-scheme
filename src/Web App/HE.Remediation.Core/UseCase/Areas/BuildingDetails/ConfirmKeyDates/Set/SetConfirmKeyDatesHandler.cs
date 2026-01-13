using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters.BuildingDetails;
using HE.Remediation.Core.Interface;
using MediatR;
using System.Transactions;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.ConfirmKeyDates.Set;

public class SetConfirmKeyDatesHandler : IRequestHandler<SetConfirmKeyDatesRequest>
{
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IApplicationDataProvider _applicationDataProvider;

    public SetConfirmKeyDatesHandler(IBuildingDetailsRepository buildingDetailsRepository, IApplicationDataProvider applicationDataProvider)
    {
        _buildingDetailsRepository = buildingDetailsRepository;
        _applicationDataProvider = applicationDataProvider;
    }

    public async Task<Unit> Handle(SetConfirmKeyDatesRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _buildingDetailsRepository.UpdateBuildingDetailsKeyDates(new UpdateBuildingDetailsKeyDatesParameters
        {
            ApplicationId = applicationId,
            StartDate = GetDate(request.StartDateMonth, request.StartDateYear),
            UnsafeCladdingRemovalDate = GetDate(request.UnsafeCladdingRemovalDateMonth, request.UnsafeCladdingRemovalDateYear),
            PracticalCompletionDate = GetDate(request.PracticalCompletionDateMonth, request.PracticalCompletionDateYear)
        });

        scope.Complete();

        return Unit.Value;
    }

    private DateTime? GetDate(int? month, int? year)
    {
        return month is not null && year is not null
            ? new DateTime(year.Value, month.Value, 1).AddMonths(1).AddDays(-1)
            : null;
    }
}