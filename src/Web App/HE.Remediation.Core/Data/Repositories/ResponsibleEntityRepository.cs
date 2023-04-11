using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Interface;
using System.Transactions;

namespace HE.Remediation.Core.Data.Repositories;

public class ResponsibleEntityRepository : IResponsibleEntityRepository
{
    private readonly IDbConnectionWrapper _connection;

    public ResponsibleEntityRepository(IDbConnectionWrapper connection)
    {
        _connection = connection;
    }

    public async Task<CompanyAddressManualDetails> GetCompanyAddress(Guid applicationId)
    {
        var result = await _connection.QuerySingleOrDefaultAsync<CompanyAddressManualDetails>(
            "GetResponsibleEntityCompanyAddress", new
            {
                ApplicationId = applicationId
            });

        return result;
    }

    public async Task<FreeholderAddressManualDetails> GetFreeholderAddress(Guid applicationId)
    {
        var result = await _connection.QuerySingleOrDefaultAsync<FreeholderAddressManualDetails>(
            "GetFreeholderAddress", new
            {
                ApplicationId = applicationId
            });

        return result;
    }

    public async Task<GetResponsibleEntityCompanyTypeResult> GetResponsibleEntityCompanyType(Guid applicationId)
    {
        var result = await _connection.QuerySingleOrDefaultAsync<GetResponsibleEntityCompanyTypeResult>(
            "GetResponsibleEntityCompanyType", new
            {
                ApplicationId = applicationId
            });

        return result;
    }
    
    public async Task UpdateFreeholderAddress(Guid applicationId, FreeholderAddressManualDetails addressDetails)
    {
        using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            await _connection.ExecuteAsync("UpdateFreeholderAddress", new
            {
                applicationId,
                addressDetails.NameNumber,
                addressDetails.AddressLine1,
                addressDetails.AddressLine2,
                addressDetails.City,
                addressDetails.County,
                addressDetails.Postcode
            });

            scope.Complete();
        }        
    }

    public async Task InsertFreeholderAddress(Guid applicationId, FreeholderAddressManualDetails addressDetails)
    {
        var addressId = Guid.NewGuid();

        using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            await _connection.ExecuteAsync("InsertFreeholderAddress",
                new
                {
                    addressId,
                    addressDetails.NameNumber,
                    addressDetails.AddressLine1,
                    addressDetails.AddressLine2,
                    addressDetails.City,
                    addressDetails.County,
                    addressDetails.Postcode
                });

            await _connection.ExecuteAsync("UpdateFreeholderAddressId", new { applicationId, addressId });

            scope.Complete();
        }
    }
}