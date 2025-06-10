using Dapper;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Data.StoredProcedureParameters.PaymentRequest;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Data.StoredProcedureResults.PaymentRequest;
using HE.Remediation.Core.Data.StoredProcedureResults.WorkPackage.KeyDates;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Extensions;
using HE.Remediation.Core.Interface;
using System.Transactions;

namespace HE.Remediation.Core.Data.Repositories;

public class PaymentRequestRepository : IPaymentRequestRepository
{
    private readonly IDbConnectionWrapper _connection;
    private readonly IApplicationDataProvider _applicationDataProvider;

    public PaymentRequestRepository(IDbConnectionWrapper connection,
                                    IApplicationDataProvider applicationDataProvider)
    {
        _connection = connection;
        _applicationDataProvider = applicationDataProvider;
    }

    public void SetPaymentRequestId(Guid? paymentRequestId)
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return;
        }
        _applicationDataProvider.SetPaymentRequestId(paymentRequestId ?? default);
    }

    public async Task<IReadOnlyCollection<PaymentRequestResult>> GetPaymentRequests()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return new List<PaymentRequestResult>();
        }

        return await _connection.QueryAsync<PaymentRequestResult>("GetPaymentRequests", new
        {
            ApplicationId = applicationId
        });
    }

    public async Task UpdatePaymentRequestTeamMemberActiveState(Guid teamMemberId, bool isActive)
    {
        await _connection.ExecuteAsync(nameof(UpdatePaymentRequestTeamMemberActiveState), new
        {
            TeamMemberId = teamMemberId,
            IsActive = isActive
        });
    }

    public async Task<List<GetTeamMembersResult>> GetPaymentRequestTeamMembers()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return new List<GetTeamMembersResult>();
        }

        var results = await _connection.QueryAsync<GetTeamMembersResult>("GetPaymentRequestTeamMembers", new
        {
            ApplicationId = applicationId
        });

        return results.ToList();
    }

    public async Task<List<PaymentCostReportResult>> GetCostReportsForPaymentRequest(Guid paymentRequestId)
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return new List<PaymentCostReportResult>();
        }

        var results = await _connection.QueryAsync<PaymentCostReportResult>("GetPaymentRequestReportFilesForApplication", new
        {
            ApplicationId = applicationId,
            PaymentRequestId = paymentRequestId
        });

        return results.ToList();
    }
    
    public async Task<GetPaymentRequestEndVersionDatesResult> GetPaymentRequestEndVersionDates(Guid applicationId)
    {
        return await _connection.QuerySingleOrDefaultAsync<GetPaymentRequestEndVersionDatesResult>(nameof(GetPaymentRequestEndVersionDates), new
        {
            ApplicationId = applicationId
        });
    }

    public async Task<GetPaymentRequestResult> GetPaymentRequestDetails(Guid applicationId, Guid paymentRequestId)
    {
        return await _connection.QuerySingleOrDefaultAsync<GetPaymentRequestResult>("GetPaymentRequestDetails", new
        {
            ApplicationId = applicationId,
            PaymentRequestId = paymentRequestId
        });
    }
    
    public async Task<GetPaymentRequestProjectDetailsResult> GetPaymentRequestProjectDetails(Guid paymentRequestId)
    {
        return await _connection.QuerySingleOrDefaultAsync<GetPaymentRequestProjectDetailsResult>("GetPaymentRequestProjectDetails", new
        {
            PaymentRequestId = paymentRequestId
        });
    }

    public async Task<GetPaymentRequestDeclarationResult> GetPaymentRequestDeclarationDetails(Guid paymentRequestId)
    {
        return await _connection.QuerySingleOrDefaultAsync<GetPaymentRequestDeclarationResult>("GetPaymentRequestDeclarationDetails", new
        {
            PaymentRequestId = paymentRequestId
        });
    }
    
    public async Task DeletePaymentRequestCostFile(Guid fileId, Guid paymentRequestId)
    {
        await _connection.ExecuteAsync(nameof (DeletePaymentRequestCostFile), new
        {
            FileId = fileId,
            PaymentRequestId = paymentRequestId
        });
    }

    public async Task InsertPaymentRequestCostFile(Guid fileId, Guid paymentRequestId)
    {
        await _connection.ExecuteAsync("InsertPaymentRequestCostFile", new
        {
            FileId = fileId,
            PaymentRequestId = paymentRequestId
        });
    }

    public async Task<KeyDatesResult> GetLatestWorkPackageKeyDates(Guid paymentRequestId, bool includeLinked = false)
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return null;
        }

        return await _connection.QuerySingleOrDefaultAsync<KeyDatesResult>("GetLatestWorkPackageKeyDates", new
        {
            ApplicationId = applicationId,
            PaymentRequestId = paymentRequestId,
            IncludeLinked = includeLinked
        });
    }

    public async Task InsertWorkPackageKeyDetailsVersion(Guid paymentRequestId, KeyDatesParameters keyDates)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        await _connection.ExecuteAsync("InsertWorkPackageKeyDetailsVersion", new
        {
            ApplicationId = applicationId,
            PaymentRequestId = paymentRequestId,
            StartDate = keyDates.StartDate,
            ExpectedDateForCompletion = keyDates.ExpectedDateForCompletion
        });
    }

    public async Task UpdatePaymentRequestDeclaration(Guid paymentRequestId,
                                                      PaymentRequestDeclarationParameters declarationParameters)
    {        
        await _connection.ExecuteAsync("UpdatePaymentRequestDeclaration", new
        {            
            PaymentRequestId = paymentRequestId,
            AwareProcess = declarationParameters?.AwareProcess,
            AwareNoPrecedentForFuture = declarationParameters?.AwareNoPrecedentForFuture,
            PredictionsAccurate = declarationParameters?.PredictionsAccurate
        });
    }

    public async Task UpdatePaymentRequestUserCostChanged(Guid paymentRequestId, 
                                                          PaymentRequestUserCostChangedParameters costParams)
    {
        await _connection.ExecuteAsync(nameof(UpdatePaymentRequestUserCostChanged), new
        {
            PaymentRequestId = paymentRequestId,
            UserEnteredCostsChanged = costParams?.UserEnteredCostsChanged 
        });
    }            

    public async Task UpdatePaymentRequestCostChangedDetails(Guid paymentRequestId, PaymentRequestCostChangedParameters costParams)
    {
        await _connection.ExecuteAsync("UpdatePaymentRequestCostChangedDetails", new
        {
            PaymentRequestId = paymentRequestId,
            CostsChanged = costParams?.CostsChanged            
        });
    }

    public async Task UpdatePaymentRequestThirdPartyContributionsChangedDetails(PaymentRequestThirdPartyContributionsChangedParameters thirdPartyContributionsParams)
    {
        await _connection.ExecuteAsync("UpdatePaymentRequestThirdPartyContributionsChangedDetails", thirdPartyContributionsParams);
    }

    public async Task UpdatePaymentRequestUnsafeCladdingRemoved(Guid paymentRequestId, 
                                                                bool? unsafeCladdingRemoved)
    {        
        await _connection.ExecuteAsync("UpdatePaymentRequestUnsafeCladdingRemoved", new
        {
            PaymentRequestId = paymentRequestId,
            UnsafeCladdingRemoved = unsafeCladdingRemoved
        });
    }
    
    public async Task UpdatePaymentRequestProjectDateChanged(Guid paymentRequestId, 
                                                             bool? projectDatesChanged)
    {        
        await _connection.ExecuteAsync(nameof(UpdatePaymentRequestProjectDateChanged), new
        {
            PaymentRequestId = paymentRequestId,
            ProjectDatesChanged = projectDatesChanged
        });
    }

    public async Task UpdatePaymentRequestUnsafeCladdingRemovedDate(Guid paymentRequestId, 
                                                                    DateTime? unsafeCladdingRemovedDate)
    {        
        await _connection.ExecuteAsync("UpdatePaymentRequestUnsafeCladdingRemovedDate", new
        {
            PaymentRequestId = paymentRequestId,
            UnsafeCladdingRemovedDate = unsafeCladdingRemovedDate
        });
    }

    public async Task UpdatePaymentRequestTaskStatus(Guid paymentRequestId, EPaymentRequestTaskStatus taskStatusId)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        await _connection.ExecuteAsync(nameof(UpdatePaymentRequestTaskStatus), new
        {            
            ApplicationId = applicationId,
            PaymentRequestId = paymentRequestId,
            TaskStatusId = (int)taskStatusId
        });
    }

    public async Task UpdatePaymentRequestReasonForChange(Guid paymentRequestId, string reasonForChange)
    {
        await _connection.ExecuteAsync("UpdatePaymentRequestReasonForChange", new
        {            
            PaymentRequestId = paymentRequestId,
            ReasonForChange = reasonForChange
        });
    }

    public async Task<bool> IsPaymentRequestSubmitted(Guid paymentRequestId)
    {
        return await _connection.QuerySingleOrDefaultAsync<bool>("IsPaymentRequestSubmitted", new
        {
            PaymentRequestId = paymentRequestId
        });
    }

    public async Task<bool> IsPaymentRequestExpired(Guid paymentRequestId)
    {
        return await _connection.QuerySingleOrDefaultAsync<bool>(nameof(IsPaymentRequestExpired), new
        {
            PaymentRequestId = paymentRequestId
        });
    }

    public async Task<bool> HasSubmittedPaymentRequests()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return false;
        }

        return await _connection.QuerySingleOrDefaultAsync<bool>("HasSubmittedPaymentRequests", new
        {
            ApplicationId = applicationId
        });
    }

    public async Task SubmitPaymentRequest(Guid paymentRequestId)
    {
        await _connection.ExecuteAsync("SubmitPaymentRequest", new
        {            
            PaymentRequestId = paymentRequestId
        });
    }

    public async Task<IReadOnlyCollection<GetPaymentCostProfileResult>> GetCostsProfile()
    {
        if (!TryGetApplicationId(out var applicationId) || !TryGetScheduleOfWorksId(out var scheduleOfWorksId, applicationId))
        {
            return null;
        }

        var paymentRequestId = _applicationDataProvider.GetPaymentRequestId();

        return await _connection.QueryAsync<GetPaymentCostProfileResult>(
            "GetPaymentRequestProfile",
            new
            {
                ApplicationId = applicationId,
                ScheduleOfWorksId = scheduleOfWorksId,
                PaymentRequestId = paymentRequestId
            });
    }

    private bool TryGetScheduleOfWorksId(out Guid? scheduleOfWorksId, Guid applicationId)
    {
        scheduleOfWorksId = _connection.QuerySingleOrDefaultAsync<Guid?>("GetScheduleOfWorksIdForApplication", new
        {
            ApplicationId = applicationId
        }).GetAwaiter().GetResult()
        ?? default;

        return scheduleOfWorksId != default;
    }

    private bool TryGetApplicationId(out Guid applicationId)
    {
        applicationId = _applicationDataProvider.GetApplicationId();

        return applicationId != default;
    }

    public async Task UpdatePaymentRequestAmount(Guid paymentRequestId, decimal amount)
    {
        await _connection.ExecuteAsync("UpdatePaymentRequestAmount", new { PaymentRequestId = paymentRequestId, PaymentRequestAmount = amount });
    }

    public async Task InsertScheduleOfWorksProfile(Guid paymentRequestId, List<UpdateCostParameters> costs)
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return;
        }

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        var costsParameters = costs
            .Where(x => x.Id is not null && x.Date is not null)
            .Select(x => new
            {
                Id = x.Id.Value,
                Date = x.Date.Value,
                Amount = x.Amount ?? 0,
                AmountIsNull = x.Amount is null
            })
            .ToDataTable()
            .AsTableValuedParameter("[dbo].[CostListType]");

        await _connection.ExecuteAsync("InsertScheduleOfWorksCostsForPaymentRequest", new { ApplicationId = applicationId, PaymentRequestId = paymentRequestId, Costs = costsParameters });

        scope.Complete();
    }

    public async Task UpdateScheduleOfWorksCosts(Guid scheduleOfWorksCostProfileId, List<UpdateCostParameters> costs)
    {
        var costsParameters = costs
            .Where(x => x.Id is not null && x.Date is not null)
            .Select(x => new
            {
                Id = x.Id.Value,
                Date = x.Date.Value,
                Amount = x.Amount ?? 0,
                AmountIsNull = x.Amount is null
            })
            .ToDataTable()
            .AsTableValuedParameter("[dbo].[CostListType]");

        await _connection.ExecuteAsync("UpdateScheduleOfWorksCostsPaymentRequest", new { ScheduleOfWorksCostProfileId = scheduleOfWorksCostProfileId, Costs = costsParameters });
    }
    
    public async Task<Guid?> GetPaymentRequestFirstPaymentRequest(Guid applicationId)
    {
        return await _connection.QuerySingleOrDefaultAsync<Guid?>(nameof(GetPaymentRequestFirstPaymentRequest), new
        {
            ApplicationId = applicationId
        });
    }
    public async Task<bool> GetUnsafeCladdingAlreadyRemoved(Guid applicationId, Guid paymentRequestId)
    {
        return await _connection.QuerySingleOrDefaultAsync<bool>(nameof(GetUnsafeCladdingAlreadyRemoved), new
        {
            ApplicationId = applicationId,
            PaymentRequestId = paymentRequestId
        });
    }    

    public async Task<OverviewResult> GetOverview(Guid paymentRequestId)
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return null;
        }
        
        var overviewResults = await _connection.QueryAsync<OverviewResult>(
            "GetApplicationPaymentRequestOverview",
            new
            {
                ApplicationId = applicationId,
                PaymentRequestId = paymentRequestId
            });

        return overviewResults?.FirstOrDefault();
    }

    public async Task<OverviewResult> GetOverview()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return null;
        }

        var paymentRequestId = _applicationDataProvider.GetPaymentRequestId();

        var overviewResults = await _connection.QueryAsync<OverviewResult>(
            "GetApplicationPaymentRequestOverview",
            new
            {
                ApplicationId = applicationId,
                PaymentRequestId = paymentRequestId
            });

        return overviewResults?.FirstOrDefault();
    }
    
    public async Task<Guid?> GetSubcontractorSurveyId(Guid paymentRequestId)
    {
        return await _connection.QuerySingleOrDefaultAsync<Guid?>("GetPaymentRequestSubcontractorSurveyId", new
        {
            PaymentRequestId = paymentRequestId
        });
    }

    public async Task UpdateSubcontractorSurveyId(Guid paymentRequestId, Guid subContractorSurveyId)
    {
        await _connection.ExecuteAsync("UpdatePaymentRequestSubcontractorSurveyId", new
        {
            PaymentRequestId = paymentRequestId,
            SubContractorSurveyId = subContractorSurveyId
        });
    }

    public async Task<int?> GetPaymentRequestVersion(Guid paymentRequestId)
    {
        return await _connection.QuerySingleOrDefaultAsync<int?>("GetPaymentRequestVersion", new
        {
            PaymentRequestId = paymentRequestId
        });
    }

    public async Task<int?> GetPaymentRequestProjectDuration(Guid paymentRequestId)
    {
        return await _connection.QuerySingleOrDefaultAsync<int?>(nameof (GetPaymentRequestProjectDuration), new
        {
            PaymentRequestId = paymentRequestId
        });
    }

    public async Task<IReadOnlyCollection<GetPaymentRequestInvoiceFilesResult>> GetPaymentRequestInvoiceFiles(GetPaymentRequestInvoiceFilesParameters parameters)
    {
        var results = await _connection.QueryAsync<GetPaymentRequestInvoiceFilesResult>(nameof(GetPaymentRequestInvoiceFiles), parameters);
        return results;
    }

    public async Task InsertPaymentRequestInvoiceFile(InsertPaymentRequestInvoiceFileParameters parameters)
    {
        await _connection.ExecuteAsync(nameof(InsertPaymentRequestInvoiceFile), parameters);
    }

    public async Task DeletePaymentRequestInvoiceFile(DeletePaymentRequestInvoiceFileParameters parameters)
    {
        await _connection.ExecuteAsync(nameof(DeletePaymentRequestInvoiceFile), parameters);
    }
}
