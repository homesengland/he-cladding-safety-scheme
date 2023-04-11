using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.Representative.SetRepresentativeType;

public class SetRepresentativeTypeHandler : IRequestHandler<SetRepresentativeTypeRequest>
{
    private readonly IDbConnectionWrapper _connection;
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;

    public SetRepresentativeTypeHandler(IDbConnectionWrapper connection, 
                                        IApplicationDataProvider applicationDataProvider,
                                        IApplicationRepository applicationRepository)
    {
        _connection = connection;
        _applicationDataProvider = applicationDataProvider;
        _applicationRepository = applicationRepository;
    }

    public async Task<Unit> Handle(SetRepresentativeTypeRequest request, CancellationToken cancellationToken)
    {
        await SaveResponse(request);
        return Unit.Value;
    }

    private async Task SaveResponse(SetRepresentativeTypeRequest request)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();
        await _connection.ExecuteAsync("InsertOrUpdateRepresentativeType", new
        {
            ApplicationId = applicationId,
            RepresentationTypeId = (int?)request.RepresentativeType
        });

        await _applicationRepository.UpdateStatusToInProgress(applicationId);
    }
}