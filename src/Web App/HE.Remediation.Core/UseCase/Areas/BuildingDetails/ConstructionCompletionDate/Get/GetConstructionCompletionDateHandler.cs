using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.ConstructionCompletionDate.Get;

internal class GetConstructionCompletionDateHandler : IRequestHandler<GetConstructionCompletionDateRequest, GetConstructionCompletionDateResponse>
{
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IApplicationDataProvider _applicationDataProvider;

    public GetConstructionCompletionDateHandler(IBuildingDetailsRepository buildingDetailsRepository, IApplicationDataProvider applicationDataProvider)
    {
        _buildingDetailsRepository = buildingDetailsRepository;
        _applicationDataProvider = applicationDataProvider;
    }

    public async ValueTask<GetConstructionCompletionDateResponse> Handle(GetConstructionCompletionDateRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var constructionCompletionDate = await _buildingDetailsRepository.GetConstructionCompletionDate(applicationId);

        return new GetConstructionCompletionDateResponse
        {
            ConstructionCompletionDateMonth = constructionCompletionDate?.Month,
            ConstructionCompletionDateYear = constructionCompletionDate?.Year
        };
    }
}