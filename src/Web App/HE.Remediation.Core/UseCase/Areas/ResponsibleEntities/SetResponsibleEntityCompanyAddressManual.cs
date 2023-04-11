using Dapper;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;
using System.Transactions;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities;

public class SetResponsibleEntityCompanyAddressManual : IRequestHandler<SetResponsibleEntityCompanyAddressManualRequest>
{
    private readonly IDbConnectionWrapper _connection;
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IResponsibleEntityRepository _responsibleEntityRepository;

    public SetResponsibleEntityCompanyAddressManual(IDbConnectionWrapper connection,
                                                    IApplicationDataProvider applicationDataProvider,
                                                    IResponsibleEntityRepository responsibleEntityRepository)
    {
        _connection = connection;
        _applicationDataProvider = applicationDataProvider;
        _responsibleEntityRepository = responsibleEntityRepository;
    }

    public async Task<Unit> Handle(SetResponsibleEntityCompanyAddressManualRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var address = await _responsibleEntityRepository.GetCompanyAddress(applicationId);
        if (address is not null)
        {
            await UpdateResponsibleEntityCompanyAddress(applicationId, request);
            return Unit.Value;
        }

        await InsertResponsibleEntityCompanyAddress(applicationId, request);
        return Unit.Value;
    }

    private async Task UpdateResponsibleEntityCompanyAddress(Guid applicationId, SetResponsibleEntityCompanyAddressManualRequest request)
    {
        await _connection.ExecuteAsync("UpdateResponsibleEntityCompanyAddress",
            new
            {
                applicationId,
                request.NameNumber,
                request.AddressLine1,
                request.AddressLine2,
                request.City,
                request.County,
                request.Postcode
            });
    }

    private async Task InsertResponsibleEntityCompanyAddress(Guid applicationId, SetResponsibleEntityCompanyAddressManualRequest request)
    {
        var addressId = Guid.NewGuid();

        using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            await _connection.ExecuteAsync("InsertResponsibleEntityCompanyAddress",
                new
                {
                    addressId,
                    request.NameNumber,
                    request.AddressLine1,
                    request.AddressLine2,
                    request.City,
                    request.County,
                    request.Postcode
                });

            await _connection.ExecuteAsync("UpdateResponsibleEntityCompanyAddressId", new { applicationId, addressId });

            scope.Complete();
        }
    }
}

public class SetResponsibleEntityCompanyAddressManualRequest : IRequest
{
    public string NameNumber { get; set; }
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }
    public string City { get; set; }
    public string County { get; set; }
    public string Postcode { get; set; }
}