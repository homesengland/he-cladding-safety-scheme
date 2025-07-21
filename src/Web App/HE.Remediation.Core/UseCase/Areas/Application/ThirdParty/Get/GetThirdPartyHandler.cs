using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.Application.ThirdParty.Get;

public class GetThirdPartyHandler(IApplicationDataProvider applicationDataProvider,
                                       IThirdPartyCollaboratorRepository thirdPartyCollaboratorRepository) : IRequestHandler<GetThirdPartyRequest, GetThirdPartyResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider = applicationDataProvider;
    private readonly IThirdPartyCollaboratorRepository _thirdPartyCollaboratorRepository = thirdPartyCollaboratorRepository;

    public async Task<GetThirdPartyResponse> Handle(GetThirdPartyRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();
        var result = await _thirdPartyCollaboratorRepository.GetTeamMembersForThirdPartyCollaboration(applicationId);

        // do not return removed users
        var filteredResponse = new GetThirdPartyResponse()
        {
            ApplicationReferenceNumber = result.ApplicationReferenceNumber,
            BuildingName = result.BuildingName,
            TeamMembers = result.TeamMembers
        };

        return filteredResponse;
    }
}
