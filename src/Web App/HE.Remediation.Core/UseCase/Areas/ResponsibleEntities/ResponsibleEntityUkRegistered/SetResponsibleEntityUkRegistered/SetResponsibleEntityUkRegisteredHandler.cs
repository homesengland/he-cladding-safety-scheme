using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.StatusTransition;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.ResponsibleEntityUkRegistered.SetResponsibleEntityUkRegistered;

public class SetResponsibleEntityUkRegisteredHandler : IRequestHandler<SetResponsibleEntityUkRegisteredRequest, SetResponsibleEntityUkRegisteredResponse>
{
    private readonly IDbConnectionWrapper _connection;
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IResponsibleEntityRepository _responsibleEntityRepository;
    private readonly IStatusTransitionService _statusTransitionService;

    public SetResponsibleEntityUkRegisteredHandler(
        IDbConnectionWrapper connection, 
        IApplicationDataProvider applicationDataProvider, 
        IResponsibleEntityRepository responsibleEntityRepository, 
        IStatusTransitionService statusTransitionService)
    {
        _connection = connection;
        _applicationDataProvider = applicationDataProvider;
        _responsibleEntityRepository = responsibleEntityRepository;
        _statusTransitionService = statusTransitionService;
    }

    public async ValueTask<SetResponsibleEntityUkRegisteredResponse> Handle(SetResponsibleEntityUkRegisteredRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        await SetResponsibleEntityUkRegistered(applicationId, request);

        await _statusTransitionService.TransitionToStatus(EApplicationStatus.ApplicationInProgress, applicationIds: applicationId);

        return await GetRepresentativeUKStatusAndOrganisationTypes(applicationId);
    }

    private async Task SetResponsibleEntityUkRegistered(Guid applicationId, SetResponsibleEntityUkRegisteredRequest request)
    {
        await _connection.ExecuteAsync("SetResponsibleEntityUkRegistered", new
        {
            ApplicationId = applicationId,
            request.UkRegistered
        });
    }

    private async ValueTask<SetResponsibleEntityUkRegisteredResponse> GetRepresentativeUKStatusAndOrganisationTypes(Guid applicationId)
    {
        var representativeType = await _connection.QuerySingleOrDefaultAsync<int>("GetRepresentativeType", new { applicationId });

        var hasRepresentative = representativeType == (int)EResponsibleEntityRepresentationType.Representative
            ? true
            : false;

        var responsibleEntityCompanyType = await _responsibleEntityRepository.GetResponsibleEntityCompanyType(applicationId);

        var hasRepresentativeUkBased = await _connection.QuerySingleOrDefaultAsync<bool?>("GetRepresentativeBasedInUk", new 
        { 
            applicationId 
        });
        
        var hasValidOrganisationTypes =
            responsibleEntityCompanyType.OrganisationType == Enums.EApplicationResponsibleEntityOrganisationType.Other &&
            responsibleEntityCompanyType.OrganisationSubType == Enums.EApplicationResponsibleEntityOrganisationSubType.Individual
            ? true
            : false;

        return new SetResponsibleEntityUkRegisteredResponse
        {
            HasRepresentative = hasRepresentative,
            HasValidOrganisationTypes = hasValidOrganisationTypes,
            OrganisationType = responsibleEntityCompanyType.OrganisationType,
            HasRepresentativeUkBased = hasRepresentativeUkBased
        };
    }
}