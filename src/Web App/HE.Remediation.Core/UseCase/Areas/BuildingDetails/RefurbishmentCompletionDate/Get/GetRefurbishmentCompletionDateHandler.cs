using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.RefurbishmentCompletionDate.Get;

public class GetRefurbishmentCompletionDateHandler : IRequestHandler<GetRefurbishmentCompletionDateRequest, GetRefurbishmentCompletionDateResponse>
{
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IApplicationDataProvider _applicationDataProvider;

    public GetRefurbishmentCompletionDateHandler(IBuildingDetailsRepository buildingDetailsRepository, IApplicationDataProvider applicationDataProvider)
    {
        _buildingDetailsRepository = buildingDetailsRepository;
        _applicationDataProvider = applicationDataProvider;
    }

    public async ValueTask<GetRefurbishmentCompletionDateResponse> Handle(GetRefurbishmentCompletionDateRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var refurbishmentCompletionDate = await _buildingDetailsRepository.GetRefurbishmentCompletionDate(applicationId);

        return new GetRefurbishmentCompletionDateResponse
        {
            RefurbishmentCompletionDateMonth = refurbishmentCompletionDate?.RefurbishmentCompletionDate?.Month,
            RefurbishmentCompletionDateYear = refurbishmentCompletionDate?.RefurbishmentCompletionDate?.Year
        };
    }
}
