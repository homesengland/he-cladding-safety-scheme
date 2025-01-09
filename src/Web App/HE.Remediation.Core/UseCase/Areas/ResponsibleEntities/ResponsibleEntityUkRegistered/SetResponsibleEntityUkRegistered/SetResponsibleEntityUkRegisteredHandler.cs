using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.ResponsibleEntityUkRegistered.SetResponsibleEntityUkRegistered;

public class SetResponsibleEntityUkRegisteredHandler : IRequestHandler<SetResponsibleEntityUkRegisteredRequest, SetResponsibleEntityUkRegisteredResponse>
{
    private readonly IDbConnectionWrapper _connection;
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IResponsibleEntityRepository _responsibleEntityRepository;
    private readonly IApplicationRepository _applicationRepository;

    public SetResponsibleEntityUkRegisteredHandler(IDbConnectionWrapper connection, IApplicationDataProvider applicationDataProvider, IResponsibleEntityRepository responsibleEntityRepository, IApplicationRepository applicationRepository)
    {
        _connection = connection;
        _applicationDataProvider = applicationDataProvider;
        _responsibleEntityRepository = responsibleEntityRepository;
        _applicationRepository = applicationRepository;
    }

    public async Task<SetResponsibleEntityUkRegisteredResponse> Handle(SetResponsibleEntityUkRegisteredRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        await SetResponsibleEntityUkRegistered(applicationId, request);

        await _applicationRepository.UpdateStatus(applicationId, EApplicationStatus.ApplicationInProgress);

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

    private async Task<SetResponsibleEntityUkRegisteredResponse> GetRepresentativeUKStatusAndOrganisationTypes(Guid applicationId)
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