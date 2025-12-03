using Dapper;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;

namespace HE.Remediation.Core.Data.Repositories;

public class ApplicationRepository : IApplicationRepository
{
    private readonly IDbConnectionWrapper _dbConnection;

    public ApplicationRepository(IDbConnectionWrapper dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<GetApplicationSummaryDetailsResult> GetApplicationSummaryDetails(Guid applicationId)
    {
        var applicationSummaryDetails = await _dbConnection.QuerySingleOrDefaultAsync<GetApplicationSummaryDetailsResult>(
            "GetApplicationSummaryDetails", new
            {
                ApplicationId = applicationId
            });

        return applicationSummaryDetails;
    }

    public async Task<GetApplicationStatusResult> GetApplicationStatus(Guid applicationId)
    {
        var applicationStatus = await _dbConnection.QuerySingleOrDefaultAsync<GetApplicationStatusResult>(
            "GetApplicationStatus", new
            {
                ApplicationId = applicationId
            });

        return applicationStatus;
    }

    public async Task UpdateStatus(Guid applicationId, EApplicationStatus status)
    {
        await _dbConnection.ExecuteAsync("UpdateApplicationDetailsStatus", new
        {
            ApplicationId = applicationId,
            StatusId = status
        });
    }

    public async Task UpdateStatus(Guid applicationId, EApplicationStatus status, string reason)
    {
        await _dbConnection.ExecuteAsync("UpdateApplicationDetailsStatus", new
        {
            ApplicationId = applicationId,
            StatusId = status,
            Reason = reason
        });
    }

    
    public async Task UpdateApplicationStage(Guid applicationId, EApplicationStage stage)
    {
        await _dbConnection.ExecuteAsync(nameof(UpdateApplicationStage), new
        {
            ApplicationId = applicationId,
            StageId = stage
        });
    }

    public async Task UpdateInternalStatus(Guid applicationId, EApplicationInternalStatus status)
    {
        await _dbConnection.ExecuteAsync("UpdateApplicationDetailsInternalStatus", new
        {
            ApplicationId = applicationId,
            InternalStatusId = status
        });
    }

    public async Task UpdateInternalStatus(Guid applicationId, EApplicationInternalStatus status, string reason)
    {
        await _dbConnection.ExecuteAsync("UpdateApplicationDetailsInternalStatus", new
        {
            ApplicationId = applicationId,
            InternalStatusId = status,
            Reason = reason
        });
    }

    public async Task<IEnumerable<FileResult>> GetResponsibleEntityEvidenceFiles(Guid applicationId)
    {
        var result =
            await _dbConnection.QueryAsync<FileResult>(
                nameof(GetResponsibleEntityEvidenceFiles),
                new { applicationId });

        return result;
    }

    public async Task<EBankDetailsRelationship> GetBankDetailsRelationship(Guid applicationId)
    {
        return await _dbConnection.QuerySingleOrDefaultAsync<EBankDetailsRelationship>("GetApplicationBankAccountRelationship",
                                                                                                   new
                                                                                                   {
                                                                                                       applicationId
                                                                                                   });
    }

    public async Task<string> GetApplicationReferenceNumber(Guid applicationId)
    {
        var referenceNumber = await _dbConnection.QuerySingleOrDefaultAsync<string>(nameof(GetApplicationReferenceNumber), new { ApplicationId = applicationId });
        return referenceNumber;
    }

    public async Task<string> GetApplicationReasonForWithdrawalRequest(Guid applicationId)
    {
        var reasonForClosing = await _dbConnection.QuerySingleOrDefaultAsync<string>(nameof(GetApplicationReasonForWithdrawalRequest), new { ApplicationId = applicationId });
        return reasonForClosing;
    }


    public async Task UpdateApplicationReasonForWithdrawalRequest(Guid applicationId, string reasonForWithdrawalRequest)
    {
        await _dbConnection.ExecuteAsync(nameof(UpdateApplicationReasonForWithdrawalRequest), new
        {
            ApplicationId = applicationId,
            ReasonForWithdrawalRequest = reasonForWithdrawalRequest
        });
    }

    public async Task<DateTime?> GetApplicationCreationDate(Guid applicationId)
    {
        var creationDate = await _dbConnection.QuerySingleOrDefaultAsync<DateTime?>(nameof(GetApplicationCreationDate), new
                                                                                    {
                                                                                        ApplicationId = applicationId
                                                                                    });
        return creationDate;
    }

    public async Task<bool> IsExistingApplication(string buildingName, string addressLine1, string postcode)
    {
        var result = await _dbConnection.QuerySingleOrDefaultAsync<bool>(
            "ApplicationExistsWithSameAddress", new
            {
                BuildingName = buildingName,
                AddressLine1 = addressLine1,
                Postcode = postcode
            });

        return result;
    }

    public async Task<bool> IsClosingReportStarted(Guid applicationId)
    {
        var result = await _dbConnection.QuerySingleOrDefaultAsync<bool>(
            "IsClosingReportStarted", new
            {
                ApplicationId = applicationId
            });

        return result;
    }

    public async Task<GetApplicationTaskListSummaryResult> GetApplicationTaskListSummary(Guid applicationId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@ApplicationId", applicationId);

        var result = await _dbConnection.QuerySingleOrDefaultAsync<GetApplicationTaskListSummaryResult>(nameof(GetApplicationTaskListSummary), parameters);
        return result;
    }

    public async Task<bool> IsSocialSector(Guid applicationId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@ApplicationId", applicationId);

        var result = await _dbConnection.QuerySingleOrDefaultAsync<bool>(nameof(IsSocialSector), parameters);
        return result;
    }
}