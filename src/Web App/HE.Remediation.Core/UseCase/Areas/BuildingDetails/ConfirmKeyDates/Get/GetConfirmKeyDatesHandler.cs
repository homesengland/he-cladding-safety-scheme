using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.ConfirmKeyDates.Get;

public class GetConfirmKeyDatesHandler : IRequestHandler<GetConfirmKeyDatesRequest, GetConfirmKeyDatesResponse>
{
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IApplicationDataProvider _applicationDataProvider;

    public GetConfirmKeyDatesHandler(IBuildingDetailsRepository buildingDetailsRepository, IApplicationDataProvider applicationDataProvider)
    {
        _buildingDetailsRepository = buildingDetailsRepository;
        _applicationDataProvider = applicationDataProvider;
    }

    public async Task<GetConfirmKeyDatesResponse> Handle(GetConfirmKeyDatesRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var buildingDetailsKeyDates = await _buildingDetailsRepository.GetBuildingDetailsKeyDates(applicationId);

        return new GetConfirmKeyDatesResponse
        {
            StartDateMonth = buildingDetailsKeyDates?.StartDate?.Month,
            StartDateYear = buildingDetailsKeyDates?.StartDate?.Year,
            UnsafeCladdingRemovalDateMonth = buildingDetailsKeyDates?.UnsafeCladdingRemovalDate?.Month,
            UnsafeCladdingRemovalDateYear = buildingDetailsKeyDates?.UnsafeCladdingRemovalDate?.Year,
            ExpectedDateForCompletionMonth = buildingDetailsKeyDates?.ExpectedDateForCompletion?.Month,
            ExpectedDateForCompletionYear = buildingDetailsKeyDates?.ExpectedDateForCompletion?.Year
        };
    }
}
